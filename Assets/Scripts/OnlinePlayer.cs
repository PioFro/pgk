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
    public SerializableEncounter team;

    [SyncVar]
    public string name;

    [SyncVar]
    public int id;

    public static event Action<SerializableEncounter, SerializableEncounter> ReceivedEncounter;

    public static event Action<SerializableSkill, int> CastedSkill; //How to define target

    [Command]
    public void SendEncounterData(SerializableEncounter encounterData, SerializableEncounter teamData)
    {
        if(encounterData!=null)
        {
            RecieveTheEncounter(encounterData, teamData);
        }
    }

    [ClientRpc]
    public void RecieveTheEncounter(SerializableEncounter encounterData, SerializableEncounter teamData)
    {
        ReceivedEncounter?.Invoke(encounterData,teamData);
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