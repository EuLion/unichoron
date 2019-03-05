using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRightScript :  Photon.PunBehaviour, IPunObservable {

	public int score = 0;
	Text text;

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
    }

    #region OnPhotonSerializeView同期
    // 現在のスコア表示を同期
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.score);
        }
        else
        {
            this.score = (int)stream.ReceiveNext();
        }
    }
    #endregion
}
