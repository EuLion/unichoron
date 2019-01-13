using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLeftScript : MonoBehaviour {

	public int score = 0;
	Text text;

	// 画面遷移後の初期表示
	void Start () {
		text = GetComponent<Text>();
		text.text = "Score of Left : " + score;
	}
	
	// Rightチームがダメージを受けた時の処理
    public void AddPointLeft(int point) {
    	score += point;
		text.text = "Score of Left :" + score;
    }
		
}
