using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeamSide {
    None = 0,
    Left,
    Right,
};

public class Team
{
    //enumのToStringが遅いため配列用意
    public static readonly string[] TEAM_NAMES = {"None","Left","Right"};
    private static GameObject[] RESPAWN_POINTS = null;
    private static int TEAM_COUNT = 0;

    private TeamSide teamSide;

    public Team ()
    {
        init();
        teamSide = TeamSide.None;
    }

    public Team (string strTeam)
    {
        init();
        setTeamSide(strTeam);
    }

    public Team (TeamSide teamSide)
    {
        init();
        setTeamSide(teamSide);
    }

    private void init()
    {
        setTeamCount();
        setRespawnPoint();
    }
    private void setTeamCount()
    {
        if (TEAM_COUNT == 0)
        {
            TEAM_COUNT = Enum.GetValues(typeof(TeamSide)).Length;
        }
    }
    private void setRespawnPoint()
    {
        if (RESPAWN_POINTS == null)
        {
            RESPAWN_POINTS = new GameObject[TEAM_COUNT];
            for (int i = 0; i < TEAM_COUNT; i++)
            {
                Debug.Log(TEAM_NAMES[i] + "RespawnPoint");
                RESPAWN_POINTS[i] = GameObject.Find(TEAM_NAMES[i] + "RespawnPoint");
            }
        }
    }

    public GameObject getRespawnPoint ()
    {
        return RESPAWN_POINTS[(int)teamSide];
    }

    public bool setTeamSide (string strTeam)
    {   
        int i = 0;
        foreach (string teamName in TEAM_NAMES)
        {
            if (teamName == strTeam)
            {
                teamSide = (TeamSide)TeamSide.ToObject(typeof(TeamSide), i);
                return true;
            }
            i++;
        }
        return false;
    }

    public void setTeamSide (TeamSide teamSide)
    {
        this.teamSide = teamSide;
    }

    public TeamSide getTeamSide ()
    {
        return teamSide;
    }

    public string getStrTeam ()
    {
        return TEAM_NAMES[(int)teamSide];
    }

    public bool equal (TeamSide teamSide)
    {
        if (this.teamSide == teamSide)
        {
            return true;
        }
        return false;
    }

    public bool equal (Team otherSide)
    {
        if (teamSide == otherSide.getTeamSide())
        {
            return true;
        }
        return false;
    }
}
