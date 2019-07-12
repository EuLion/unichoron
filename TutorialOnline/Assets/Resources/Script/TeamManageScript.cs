using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeamManageScript : Photon.PunBehaviour, IPunObservable {

    // チーム
    public String team = "";
    public const float TIMEOUT = 30f;

    void Start () {
        if (photonView.isMine) {
            team = PlayerPrefs.GetString(AutoMatchingScript.playerTeamPrefKey);
            Debug.Log("my team: " + team);

            //自分のチームを送信する
            photonView.RPC("setTeam", PhotonTargets.Others, team);

            /*
			//3.0秒後に実行する
    		StartCoroutine(DelayMethod(3.0f, () =>
    		{
        		team = PlayerPrefs.GetString(AutoMatchingScript.playerTeamPrefKey);
    		}));
    		//2.0秒後に実行する
    		StartCoroutine(DelayMethod(2.0f, () =>
    		{
        		team = PlayerPrefs.GetString(AutoMatchingScript.playerTeamPrefKey);
    		}));
    		//5.0秒後に実行する
    		StartCoroutine(DelayMethod(5.0f, () =>
    		{
        		team = PlayerPrefs.GetString(AutoMatchingScript.playerTeamPrefKey);
    		}));
            */
        } else { //not photonView.isMine
            if (team == "") {
                //他のプレイヤーに聞く
                askTeam();
            }
        }
        Invoke("canStart", TIMEOUT);
    }

    [PunRPC]
    private void setTeam (String team) {
        if (this.team == "") {
            Debug.Log("set team: " + team);
            this.team = team;
        }
    }

    private void askTeam () {
        Debug.Log("ask team");
        photonView.RPC("sendTeam", PhotonTargets.Others);
    }

    [PunRPC]
    private void sendTeam () {
        Debug.Log("send team");
        photonView.RPC("setTeam", PhotonTargets.Others, team);
    }

    private void canStart() {
        if (!isReady()) {
            Debug.Log("エラー：タイムアウトしました。");
            //TODO:エラー　ロビーに戻す
        }
    }
    public bool isReady () {
        return team != ""; 
    }

    #region OnPhotonSerializeView同期
    //プレイヤーのチームを同期
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 
    {
        if (stream.isWriting)
        {
            stream.SendNext(this.team);
        }
        else
        {
            this.team = (string)stream.ReceiveNext();
        }
    }
    #endregion

    /// <summary>
	/// 渡された処理を指定時間後に実行する
	/// </summary>
	/// <param name="waitTime">遅延時間[ミリ秒]</param>
	/// <param name="action">実行したい処理</param>
	/// <returns></returns>
	private IEnumerator DelayMethod(float waitTime, Action action)
	{
    	yield return new WaitForSeconds(waitTime);
    	action();
	}
}
