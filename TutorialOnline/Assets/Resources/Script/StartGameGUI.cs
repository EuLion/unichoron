using UnityEngine;
using UnityEngine.UI;

public class StartGameGUI : MonoBehaviour {

    public Text playerName;
    public GameObject inputNamePanel;
    public GameObject lancherObject;
    private LauncherScript lancher;

    // Use this for initialization
    void Awake () {
        lancher = lancherObject.GetComponent<LauncherScript>();
    }

    // Update is called once per frame
    void Update () {

    }

    public void StartGame ()
    {
        if (!inputNamePanel.GetActive()) {
            //もし名前が空欄なら設定画面を開く
            if (playerName.text == "") {
                Debug.Log("playerName is empty!");
                inputNamePanel.SetActive (true);
            } else {
                lancher.Connect();
            }
        }
    }

    //名前入力画面のOKクリック
    public void ClickOK ()
    {
        if (playerName.text != "") {
            inputNamePanel.SetActive (false);
        }
    }
}
