using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultGUI : MonoBehaviour
{
    // Use this for initialization
    void Start () {
        //TODO:下の実装
        //・まず、フィールドに存在するTopListとScoreListの中身を空にする
        //・「ResultPlayer」構造体を作成（中身は以下)
        //    ・playerName
        //    ・score
        //・resultPlayeListを作成
        //・GetComponentでフィールドに存在するTopListとScoreListを取得
        //・自分の名前の取得
        //・PlayerPrefs.GetStringからTopとScoreの中身を取得
        //    ・keyを"PlayerName"+i,"Score"+i(iは0~最大人数まで)
        //    ・PlayerPrefs.HasKey(key)でfalseならbreak
        //    ※keyは濱ちゃんと共通のものに設定すること
        //・取得したTopとScoreを「ResultPlayer」構造体に代入
        //・「ResultPlayer」構造をresultPlayeListに追加
        //・foreach resultPlayeList で順に表示（処理は以下）
        //    ・resultPlayerListから取得した一つからTopを取得しTextクラスの変数に代入
        //    ・resultPlayerListから取得した一つからScoreを取得しTextクラスの変数に代入
        //    ・GetComponentで取得したTopListにTopを追加（描画される
        //    ・GetComponentで取得したScoreListにScoreを追加（描画される
        //    ・この時、自分の名前と一致したら強調＋報酬を与える
    }

    // Update is called once per frame
    void Update () {

    }

    //Backボタンをクリックした時の処理
    //TODO:このクラスをBackボタンにアタッチし、クリック処理に登録すること
    public void BtnBack(){
        //TODO:シーン遷移
    }
}
