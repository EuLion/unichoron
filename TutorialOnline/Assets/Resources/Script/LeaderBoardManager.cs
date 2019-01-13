using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LeaderBoardManager : MonoBehaviour
{

    // private LeaderBoard lBoard;
    public Text targetText;
   // public GameObject[] top = new GameObject[5];
   // public GameObject[] nei = new GameObject[5];

    void Start()
    {
 //       lBoard = new LeaderBoard();

        // テキストを表示するゲームオブジェクトを取得
         //for (int i = 0; i < 5; ++i)
         //{
   //          top[i] = GameObject.Find("Top" + i);
     //        nei[i] = GameObject.Find("Neighbor" + i);
       //  }
        for (int i = 0; i < 5; ++i)
        {

            //   top[i].guiText.text = i + 1 + ". " + lBoard.topRankers[i].print();
            this.targetText = this.GetComponent<Text>(); // <---- 追加3
            this.targetText.text = "ChangeText";
        }


    }

}