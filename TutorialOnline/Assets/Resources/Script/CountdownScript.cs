using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownScript : MonoBehaviour {

    private int counter = 1;
    private int interval = 30;

    private Text countdownText;
    private int minSize = 50;
    private int maxSize = 140;
    private float fontSize;
    private float unit = 1f;

    private string message = "GO!";
    private CountEndEvent countEndEvent;

    // Use this for initialization
    void Start () {
        countdownText = this.GetComponent<Text>();
        unit = (float)(maxSize - minSize) / interval;
        Debug.Log("unit: " + unit);
        fontSize = (float)countdownText.fontSize;
        countEndEvent = _DefaultEvent;
    }
    private void _DefaultEvent () {
        Debug.Log("DefaultEvent called!");
    }
    
    // Update is called once per frame
    void Update () {
        if (counter++ % interval == 0) {
            Debug.Log("counter: " + countdownText.text);
            int countdownInt;
            if (int.TryParse(countdownText.text, out countdownInt)) {
                if (countdownInt > 1) {
                    countdownText.text = (countdownInt - 1).ToString();
                } else {
                    countdownText.text = message;
                    countEndEvent();
                }
            } else {
                countdownText.text = "";
                countdownText.fontSize = minSize;
            }
            countdownText.fontSize = minSize;
            fontSize = (float)minSize;
        }
        if (countdownText.text != "" && countdownText.fontSize <= maxSize) {
            fontSize += unit;
            countdownText.fontSize = (int)fontSize;
        }
    }

    public void SetMessage (string msg) {
        this.message = msg;
    }

    public void SetCount (uint num) {
        this.countdownText.text = num.ToString();
    }

    public void SetEvent(CountEndEvent evt) {
        this.countEndEvent = evt;
    }

    public delegate void CountEndEvent ();
}
