using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TeamManageScript : Photon.PunBehaviour {

    // チーム
    public Team team;
    public const float TIMEOUT = 30f;

    void Awake () { //CharacterControlScriptのStart使用するteamを先に初期化するためAwake
        team = new Team();
        if (photonView.isMine) {
            team.setTeamSide(PlayerPrefs.GetString(AutoMatchingScript.playerTeamPrefKey));
            Debug.Log("my team: " + team.getTeamSide());

            //自分のチームを送信する
            photonView.RPC("setTeam", PhotonTargets.Others, team.getStrTeam());
        } else { //not photonView.isMine
            if (team.equal(TeamSide.None)) {
                //他のプレイヤーに聞く
                askTeam();
            }
        }
        Invoke("canStart", TIMEOUT);
    }

    [PunRPC]
    private void setTeam (String strTeam) {
        if (this.team.equal(TeamSide.None)) {
            Debug.Log("set team: " + strTeam);
            Debug.Log("could set: " + this.team.setTeamSide(strTeam));
        }
    }

    private void askTeam () {
        Debug.Log("ask team");
        photonView.RPC("sendTeam", PhotonTargets.Others);
    }

    [PunRPC]
    private void sendTeam () {
        Debug.Log("send team");
        photonView.RPC("setTeam", PhotonTargets.Others, team.getStrTeam());
    }

    private void canStart() {
        if (!isReady()) {
            Debug.Log("エラー：タイムアウトしました。");
            //TODO:エラー　ロビーに戻す
        }
    }
    public bool isReady () {
        return !team.equal(TeamSide.None); 
    }
}
