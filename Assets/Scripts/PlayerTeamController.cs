using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeamController : MonoBehaviour
{
    public Character[] Team = new Character[MaxTeamSize];

    private const int MaxTeamSize = 4;

    public PlayerUIController playerUIController;

    void Start()
    {
        playerUIController = GetComponentInChildren<PlayerUIController>();
        //CreateTeam();
    }

    //private void CreateTeam()
    //{
    //    for (int i = 0; i < TeamSize; i++)
    //    {
    //        Team.Add(new Character());
    //        uIController.SetImageOnIndex(i, Team[i].Avatar);
    //        uIController.SetImageOnIndexActive(i, true);
    //    }

    //}

}
