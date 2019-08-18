using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
 
public class GameManagerScript : Photon.PunBehaviour
{
    const float WAIT_TIME = 0.1f;
    //誰かがログインする度に生成するプレイヤーPrefab
    public GameObject playerPrefab;
        
    void Start()
    {
        if (!PhotonNetwork.connected)   //Photonに接続されていなければ
        {
            SceneManager.LoadScene("Launcher"); //ログイン画面に戻る
            return; 
        }

        //WaitForSecondsを呼び出すためのStartCoroutine
        StartCoroutine(init());
    }

    IEnumerator init()
    {
        //Photonに接続していれば自プレイヤーを生成
        GameObject Player = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 0f, 0f), Quaternion.identity, 0);

        yield return new WaitForSeconds(WAIT_TIME);

        movePlayerToRespawnPos(Player);
    }

    private void movePlayerToRespawnPos (GameObject Player)
    {
        TeamManageScript tmpTeamManageScript = Player.GetComponent<TeamManageScript>() as TeamManageScript;
        GameObject respawnPoint = tmpTeamManageScript.team.getRespawnPoint();

        Player.transform.position = respawnPoint.transform.position;
        Player.transform.rotation = respawnPoint.transform.rotation;
    }

    // Update is called once per frame
    void Update () {
    
    }
}