using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : Photon.PunBehaviour {

    public int score = 0;
    private Text text;
    public TeamSide team;
    private string strTeam;

    // 画面遷移後の初期表示
    void Start () {
        text = GetComponent<Text>();
        strTeam = team.ToString();
        text.text = "Score of " + strTeam + " : " + score;
    }

    void OnGUI () {
        // 現在のスコア表示を更新
        text.text = "Score of " + strTeam + " :" + score;
    }

    // Rightチームがダメージを受けた時の処理
    public void AddPoint(int point) {
        Debug.Log("in AddPoint:" + point);
        var properties  = new ExitGames.Client.Photon.Hashtable();
        properties.Add( strTeam + "Score", point);
        PhotonNetwork.room.SetCustomProperties( properties );
    }

    public void OnPhotonCustomRoomPropertiesChanged( ExitGames.Client.Photon.Hashtable i_propertiesThatChanged )
    {
        Debug.Log("Photon AddPoint!!!");
        {//object valueのスコープ
            object value = null;
            if( i_propertiesThatChanged.TryGetValue( strTeam + "Score", out value ) )
            {
                Debug.Log("Photon AddPoint: " + (int)value);
                this.score += (int)value;
                Debug.Log("score: " + score);
            }
        }
    }
}
