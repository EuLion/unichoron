using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Top0 : MonoBehaviour {

    #region Private変数定義
    static string playerNamePrefKey = "PlayerName";
    static string playerteamKey = "red";
    #endregion

    #region MonoBehaviourコールバック
    void Start()
    {
        string defaultName = "player0";
        string Top0 = GetComponent<GUIText>().text;

        //前回プレイ開始時に入力した名前をロードして表示
        if (Top0 != null)
        {
            if (PlayerPrefs.HasKey(playerNamePrefKey))
            {
                defaultName = PlayerPrefs.GetString(playerNamePrefKey);
                Top0 = defaultName;
            }
        }
    }

}
#endregion