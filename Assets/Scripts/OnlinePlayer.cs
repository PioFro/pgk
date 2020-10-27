using System;
using System.Collections;
using System.Collections.Generic;
using Mirror;
using ScriptableObjects;
using UnityEngine;

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
    public void SendSkillData(SerializableSkill skill, int characterId)
    {
        ReceiveSkill(skill, characterId);
    }

    [Command]
    public void SendEncounterData(SerializableEncounter encounterData, SerializableEncounter teamData)
    {
        if (encounterData != null)
        {
            RecieveTheEncounter(encounterData, teamData);
        }
    }

    [ClientRpc]
    public void ReceiveSkill(SerializableSkill skill, int characterId)
    {
        CastedSkill?.Invoke(skill, characterId);
    }

    [ClientRpc]
    public void RecieveTheEncounter(SerializableEncounter encounterData, SerializableEncounter teamData)
    {
        ReceivedEncounter?.Invoke(encounterData, teamData);
        Debug.Log("Received Encounter");
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
