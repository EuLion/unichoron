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
    public static readonly string[] TEAM_NAMES = {"None","Left","Right"};
    private TeamSide teamSide;

    public Team ()
    {
        teamSide = TeamSide.None;
    }

    public Team (string strTeam)
    {
        setTeamSide(strTeam);
    }

    public Team (TeamSide teamSide)
    {
        setTeamSide(teamSide);
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