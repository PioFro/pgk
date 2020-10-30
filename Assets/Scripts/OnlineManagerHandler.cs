using Mirror;
using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineManagerHandler : MonoBehaviour
{
    public OnlineManager manager;
    public OnlinePlayer player;
    public static OnlinePlayer staticPlayerReference;
    public FightController fightController;
    public Encounter enemyEncounter;

    public PlayerUIController uIController;

    public PlayerTeamController team;

    public bool isWaiting = true;
    public bool receivedEncounter = false;
    public bool sendAck = false;


    public bool isHost = true;
    // Start is called before the first frame update
    void Awake()
    {
        manager = GameObject.Find("NetworkManager").GetComponent<OnlineManager>();
        fightController = GameObject.Find("FightContainer(Clone)").GetComponent<FightController>();
        uIController = GameObject.Find("PlayerUI").GetComponent<PlayerUIController>();
        team = GetComponent<PlayerTeamController>();

        uIController.SubscribeFightController(fightController);

        fightController.FightFinished += OnFightEnd;
        enemyEncounter = FightController.Enemy;
        

        if(isHost)
        {
            manager.StartServer();
            OnlinePlayer.ReceivedAck += OnHostReceivedAck;
        }
        else
        {
            manager.JoinServer();
            OnlinePlayer.ReceivedEncounter += OnClientReceivedEncounter;
        }
        OnlinePlayer.CastedSkill += OnCastedSkill;


        //player = NetworkClient.connection.identity.GetComponent<OnlinePlayer>();
        //Debug.Log("Started as " + player.name);
    }
    public void OnHostReceivedAck(bool ack)
    {
        receivedEncounter = ack;
    }

    public void OnClientReceivedEncounter(SerializableEncounter enemy, SerializableEncounter team)
    {
        if (!receivedEncounter)
        {
            Debug.Log("Player 2nd received encounter");
            Encounter enemyEncounter = enemy.ToEncounter();
            Encounter teamEncounter = team.ToEncounter();
            receivedEncounter = true;
            foreach(Character character in teamEncounter.CharactersInEncounter)
            {
                this.team.AssignCharacterToNextFreeSlot(character);
            }

            fightController.SetupFight(teamEncounter.CharactersInEncounter,enemyEncounter.CharactersInEncounter);
        }
    }

    public void OnCastedSkill(SerializableSkill serializableSkill, int target)
    {
        Debug.Log("CAST on "+target+"dmg "+serializableSkill.HitRatio);
        fightController.DoActionOnId(target, (int)serializableSkill.HitRatio);
        fightController.TickFight();
    }

    public void OnFightEnd()
    {
        manager.StopConnection();
        Debug.Log("server disconnected");
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (player == null)
            {
                player = NetworkClient.connection.identity.GetComponent<OnlinePlayer>();
                staticPlayerReference = player;
                Debug.Log("Started as " + player.name);
            }   
            if(manager.CheckNumberOfPlayers()&&player.name.Equals("Under attack")&&isWaiting)
            {
                Debug.Log("Second Player Joined");
                PlayerTeamController team = GameObject.Find("Player").GetComponent<PlayerTeamController>();
                try
                {
                    SerializableEncounter enc = new SerializableEncounter(team.GetMyTeamAsAnEncounter());
                    player.SendEncounterData(new SerializableEncounter(enemyEncounter), enc);
                    isWaiting = false;
                }
                catch
                { }
            }

        }
        catch { }
    }
}
