using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
//using UnityEngine.SceneManagement;

public class AutoMatchingScript : MonoBehaviour {

    //オートマッチング
    private int playerNum = 2;//テスト時：１,本番：２
    private string createRoomName = "No_Name";
    private int matchCounter = 100;
    private bool isHost = false;

    //チーム振り分け
    public static string playerTeamPrefKey = "PlayerTeam";

    private string teamCount = "TeamCount";

    //オンライン化に必要なコンポーネントを設定
    public PhotonView myPV;

    void Start () {
        //プレイヤーがチーム情報を保持している場合は削除する
        if (PlayerPrefs.HasKey(playerTeamPrefKey)) {
            PlayerPrefs.DeleteKey(playerTeamPrefKey);
        }
    }

    // Update is called once per frame
    void Update () {
        //約３秒ごとにマッチング
        if (matchCounter++ % 150 == 0) {
            //Debug.Log("matchCounter: " + matchCounter);
            AutoMatching();
        }
        if (isHost && PhotonNetwork.inRoom) { //ホストの時
            if (PhotonNetwork.room.playerCount == playerNum) { //入室可能人数いっぱいになったら
                //チーム分け
                chooseUpTeams();

                //ゲームスタート
                myPV.RPC("LoadBattleScene", PhotonTargets.AllViaServer);
            }
        }
    }

    private void chooseUpTeams ()
    {
        int teamSideNum = Enum.GetNames(typeof(TeamSide)).Length - 1;//Noneの分減らす
        int i = 0;
        foreach ( var player in PhotonNetwork.playerList ) {
            string teamName = Enum.GetName(typeof(TeamSide), i % teamSideNum + 1);//Noneの分飛ばす
            var properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add(playerTeamPrefKey, teamName);
            player.SetCustomProperties(properties);
            Debug.Log("teamName: " + teamName);
            i++;
        }
    }


    //自動でマッチングを行い、ゲームを開始する
    public void AutoMatching ()
    {
        //部屋に居ない場合
        if (!PhotonNetwork.inRoom) {
            string othersRoomName = GetRoom();
            Debug.Log(othersRoomName);

            //部屋が他になかったら
            if (othersRoomName == "") {
                //作成する部屋の設定
                RoomOptions roomOptions = new RoomOptions();
                roomOptions.IsVisible = true;   //ロビーで見える部屋にする
                roomOptions.IsOpen = true;      //他のプレイヤーの入室を許可する
                roomOptions.MaxPlayers = (byte)playerNum;    //入室可能人数を設定
                //ルームカスタムプロパティで部屋作成者を表示させるため、作成者の名前を格納
                roomOptions.CustomRoomProperties = new ExitGames.Client.Photon.Hashtable()
                {
                    { "RoomCreator",PhotonNetwork.playerName }
                };
                roomOptions.CustomRoomProperties.Add(teamCount, "0");
                //ロビーにカスタムプロパティの情報を表示させる
                roomOptions.CustomRoomPropertiesForLobby = new string[] {
                    "RoomCreator",
                };

                if (PlayerPrefs.HasKey(NameInputFieldScript.playerNamePrefKey))
                {
                    createRoomName = PlayerPrefs.GetString(NameInputFieldScript.playerNamePrefKey);
                }

                Debug.Log("createRoomName: " + createRoomName);
                //部屋作成
                PhotonNetwork.CreateRoom(createRoomName/* + DateTime.Now*/,roomOptions,null);
                isHost = true;

            } else {
                //roomnameの部屋に入室
                PhotonNetwork.JoinRoom(othersRoomName);
            }
        } else { //部屋にいる場合
            // ルームの状態を取得
            Room room = PhotonNetwork.room;
            if (room == null) {
                return;
            }
        }
    }

    public string GetRoom()
    {
        //roomInfoに現在存在するルーム情報を格納・更新
        RoomInfo[] roomInfo = PhotonNetwork.GetRoomList();

        //ルームが無ければreturn
        if (roomInfo == null || roomInfo.Length == 0) return "";

        //ルームがあればRoomElementでそれぞれのルーム情報を表示
        for (int i = 0; i < roomInfo.Length; i++)
        {
            Debug.Log(roomInfo[i].Name + " : " + roomInfo[i].Name + "–" + roomInfo[i].PlayerCount + " / " + roomInfo[i].MaxPlayers /*+ roomInfo[i].CustomProperties["roomCreator"].ToString()*/);
            
            if (roomInfo[i].PlayerCount < roomInfo[i].MaxPlayers) {
                return roomInfo[i].Name;
            }
        }
        return "";
    }

    //バトルシーン遷移同期用RPC
    [PunRPC]
    private void LoadBattleScene()
    {
        PhotonNetwork.LoadLevel("battle");
    }

    public void OnPhotonPlayerPropertiesChanged( object[] playerAndUpdatedProps )
    {
        var player      = playerAndUpdatedProps[ 0 ] as PhotonPlayer;
        var properties  = playerAndUpdatedProps[ 1 ] as ExitGames.Client.Photon.Hashtable;

        if (player.ID == PhotonNetwork.player.ID) { //自分なら
            object myTeamSide = null;
            properties.TryGetValue(playerTeamPrefKey, out myTeamSide);
            PlayerPrefs.SetString(playerTeamPrefKey, myTeamSide.ToString());
        }
    }
}