using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : Photon.PunBehaviour {

    public int score = 0;
    private Text text;
    public TeamSide team;

    // 画面遷移後の初期表示
    void Start () {
        text = GetComponent<Text>();
        text.text = "Score of " + team.ToString() + " : " + score;
    }

    void GUI () {
        // 現在のスコア表示を更新
        text.text = "Score of " + team.ToString() + " :" + score;
    }

    // Rightチームがダメージを受けた時の処理
    public void AddPointLeft(int point) {
        var properties  = new ExitGames.Client.Photon.Hashtable();
        properties.Add( team.ToString() + "Score", point);
        PhotonNetwork.room.SetCustomProperties( properties );
    }

    public void OnPhotonCustomRoomPropertiesChanged( ExitGames.Client.Photon.Hashtable i_propertiesThatChanged )
    {
        {//object valueのスコープ
            object value = null;
            if( i_propertiesThatChanged.TryGetValue( team.ToString() + "Score", out value ) )
            {
                this.score += (int)value;
            }
        }
    }
}
