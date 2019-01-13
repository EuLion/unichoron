using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreRightScript : MonoBehaviour {

	public int score = 0;
	Text text;

	// 画面遷移後の初期表示
	void Start () {
		text = GetComponent<Text>();
		text.text = "Score of Right : " + score;
	}
	
	// Leftチームがダメージを受けた時の処理
    public void AddPointRight(int point) {
    	score += point;
		text.text = "Score of Right :" + score;
    }

}
