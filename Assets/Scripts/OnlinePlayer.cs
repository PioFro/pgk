using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
public class OnlinePlayer : NetworkBehaviour
{
    [Header("Players Team")]
    public Encounter team;

    [SyncVar]
    public string name;

    [SyncVar]
    public int id;

    public static event Action<OnlinePlayer, Encounter, Encounter> ReceivedEncounter;

    public static event Action<OnlinePlayer, Skill, int> CastedSkill; //How to define target

    [Command]
    void SendEncounterData(Encounter encounterData, Encounter teamData)
    {
        if(encounterData!=null)
        {
            RecieveTheEncounter(encounterData, teamData);
        }
    }

    [ClientRpc]
    public void RecieveTheEncounter(Encounter encounterData, Encounter teamData)
    {
        ReceivedEncounter?.Invoke(this, encounterData,teamData);
    }
    public override void OnStartServer()
    {
        base.OnStartServer();

        if (isLocalPlayer)
        {
            name = "Under attack";
        }
        else
            name = "Attacker";

        id = connectionToClient.connectionId;
    }
}