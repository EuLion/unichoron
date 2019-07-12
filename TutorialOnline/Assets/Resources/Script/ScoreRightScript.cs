using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRightScript : Photon.PunBehaviour {

    public int score = 0;
    private Text text;

    // 画面遷移後の初期表示
    void Start () {
        text = GetComponent<Text>();
        text.text = "Score of Right : " + score;
    }

    void Update () {
        // 現在のスコア表示を更新
        text.text = "Score of Right :" + score;
    }

    // Leftチームがダメージを受けた時の処理
    public void AddPointRight(int point) {
        score += point;
        var properties  = new ExitGames.Client.Photon.Hashtable();
        properties.Add( "RightScore", score);
        PhotonNetwork.room.SetCustomProperties( properties );
 
    }

    public void OnPhotonCustomRoomPropertiesChanged( ExitGames.Client.Photon.Hashtable i_propertiesThatChanged )
    {
        {//object valueのスコープ
            object value = null;
            if( i_propertiesThatChanged.TryGetValue( "RightScore", out value ) )
            {
                this.score = (int)value;
            }
        }
    }
}
