using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLeftScript : Photon.PunBehaviour {

    public int score = 0;
    private Text text;

    // 画面遷移後の初期表示
    void Start () {
        text = GetComponent<Text>();
        text.text = "Score of Left : " + score;
    }

    void Update () {
        // 現在のスコア表示を更新
        text.text = "Score of Left :" + score;
    }

    // Rightチームがダメージを受けた時の処理
    public void AddPointLeft(int point) {
        score += point;
        var properties  = new ExitGames.Client.Photon.Hashtable();
        properties.Add( "LeftScore", score);
        PhotonNetwork.room.SetCustomProperties( properties );
    }

    public void OnPhotonCustomRoomPropertiesChanged( ExitGames.Client.Photon.Hashtable i_propertiesThatChanged )
    {
        {//object valueのスコープ
            object value = null;
            if( i_propertiesThatChanged.TryGetValue( "LeftScore", out value ) )
            {
                this.score = (int)value;
            }
        }
    }
}
