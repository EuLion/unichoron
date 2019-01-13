using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class AutoMatchingScript : MonoBehaviour {

    //オートマッチング
    private int playerNum = 2;
    private string createRoomName = "No_Name";
    private int matchCounter = 100;

    // Update is called once per frame
    void Update () {
        //約３秒ごとにマッチング
        if (matchCounter++ % 150 == 0) {
            //Debug.Log("matchCounter: " + matchCounter);
            AutoMatching();
        }
    }

    //自動でマッチングを行い、ゲームを開始する
    public void AutoMatching ()
    {
        //部屋に居ない場合
        if (!PhotonNetwork.inRoom) {
            string othersRoomName = GetRooms();
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
            if (room.playerCount == room.maxPlayers) {
                PhotonNetwork.LoadLevel("battle");
            }
        }
    }

    public string GetRooms()
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

}