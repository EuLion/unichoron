using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class text0 : MonoBehaviour
{
    #region Private変数定義
    static string playerNamePrefKey = "PlayerName";
    #endregion

    #region MonoBehaviourコールバック
    void Start()
    {
        string defaultName = "aaaaaa";
       // InputField _inputField = this.GetComponent<InputField>();


          if (PlayerPrefs.HasKey(playerNamePrefKey))
          {
              defaultName = PlayerPrefs.GetString(playerNamePrefKey);
        //      _inputField.text = defaultName;
          }
          else
          {
          //    _inputField.text = "no name";

        }

    }

}
#endregion