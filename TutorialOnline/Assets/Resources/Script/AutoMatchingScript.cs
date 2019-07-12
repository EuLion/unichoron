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

    //チーム振り分け完了判定リスト
    //private List<int> teamList = new List<int>();    //チーム振り分けが完了したら0を挿入
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
        int teamSideNum = Enum.GetNames(typeof(TeamSide)).Length;
        int i = 0;
        foreach ( var player in PhotonNetwork.playerList ) {
            var properties = new ExitGames.Client.Photon.Hashtable();
            properties.Add(playerTeamPrefKey, Enum.GetName(typeof(TeamSide), i % teamSideNum));
            player.SetCustomProperties(properties);
            Debug.Log("test: " + i.ToString());
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

            /*
            // ルームのカスタムプロパティを取得
            ExitGames.Client.Photon.Hashtable cp = room.customProperties;
            int teamCnt = Convert.ToInt32(cp[teamCount]);
                Debug.Log(teamCnt);

            //チーム振り分け
            if (!PlayerPrefs.HasKey(playerTeamPrefKey)) {
                Debug.Log("チーム振り分け開始");
                if (teamCnt % 2 == 0) {
                    //チーム振り分け(奇数番目に入室したプレイヤー)
                    PlayerPrefs.SetString(playerTeamPrefKey, team1);
                } else if (teamCnt % 2 == 1) {
                    //チーム振り分け(偶数番目に入室したプレイヤー)
                    PlayerPrefs.SetString(playerTeamPrefKey, team2);
                }

                Debug.Log("playerTeamPrefKey: " + PlayerPrefs.GetString(playerTeamPrefKey));
                
                cp[teamCount] = Convert.ToString(teamCnt + 1);
                room.SetCustomProperties (cp);
            }*/

            /*
            //チーム振り分け
            if (!PlayerPrefs.HasKey(playerTeamPrefKey)) {
                Debug.Log("チーム振り分け開始");
                if (room.playerCount % 2 == 1) {
                    //チーム振り分け(奇数番目に入室したプレイヤー)
                    PlayerPrefs.SetString(playerTeamPrefKey, team1);
                } else if (room.playerCount % 2 == 0) {
                    //チーム振り分け(偶数番目に入室したプレイヤー)
                    PlayerPrefs.SetString(playerTeamPrefKey, team2);
                }

                //チーム振り分け完了リスト更新
                myPV.RPC("TeamListAdd", PhotonTargets.AllViaServer);
                Debug.Log("playerTeamPrefKey: " + PlayerPrefs.GetString(playerTeamPrefKey));
            }
            */
            
            /*
            //ルームが満員でチーム振り分けが完了している場合
            if (Convert.ToInt32(cp[teamCount]) == room.maxPlayers) {
                Debug.Log("バトルシーンに遷移します");
                myPV.RPC("LoadBattleScene", PhotonTargets.AllViaServer);
            }
            */

            /*
            //ルームが満員の場合
            if (room.playerCount == room.maxPlayers) {
                Debug.Log("ルームが満員になりました");
                //チーム振り分けが完了している場合：バトルシーンへ遷移
                if ((room.maxPlayers % 2 == 1 && PlayerPrefs.GetString(playerTeamPrefKey) == team1)
                    || (room.maxPlayers % 2 == 0 && PlayerPrefs.GetString(playerTeamPrefKey) == team2)) {
                    //バトルシーン遷移RPC
                    Debug.Log("バトルシーンに遷移します");
                    myPV.RPC("LoadBattleScene", PhotonTargets.AllViaServer);
                }
            }
            */
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

    /*
    //チーム振り分け完了リスト同期用RPC
    [PunRPC]
    void TeamListAdd()
    {
        teamList.Add(0);
    }
    */

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