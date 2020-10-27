using Mirror;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineManagerHandler : MonoBehaviour
{
    public OnlineManager manager;
    public OnlinePlayer player;
    public FightController fightController;
    public Encounter enemyEncounter;
    public bool isWaiting = true;


    public bool isHost = true;
    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("NetworkManager").GetComponent<OnlineManager>();
        fightController = GameObject.Find("FightContainer(Clone)").GetComponent<FightController>();

        fightController.FightFinished += OnFightEnd;

        fightController.FightStarted += OnOnlineFightStart;

        if(isHost)
        {
            manager.StartServer();
        }
        else
        {
            manager.JoinServer();
        }

        //player = NetworkClient.connection.identity.GetComponent<OnlinePlayer>();
        //Debug.Log("Started as " + player.name);
    }

    public void OnFightEnd()
    {
        manager.StopConnection();
        Debug.Log("server disconnected");
    }

    public void OnOnlineFightStart(Encounter enemy)
    {
        enemyEncounter = enemy;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (player == null)
            {
                player = NetworkClient.connection.identity.GetComponent<OnlinePlayer>();
                Debug.Log("Started as " + player.name);
            }
            if(manager.CheckNumberOfPlayers()&&isWaiting)
            {
                Debug.Log("Second Player Joined");
                isWaiting = false;
                PlayerTeamController team = GameObject.Find("Player").GetComponent<PlayerTeamController>();

                player.SendEncounterData(new SerializableEncounter(enemyEncounter), new SerializableEncounter(team.GetMyTeamAsAnEncounter()));
            }
        }
        catch { }
    }
}
