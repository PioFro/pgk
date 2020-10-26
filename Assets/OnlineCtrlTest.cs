using System.Collections;
using System.Collections.Generic;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class OnlineCtrlTest : MonoBehaviour
{
    public OnlinePlayer player = null;
    public PlayerTeamController ptc;
    public bool trigger = false;
    public Text text;
    // Start is called before the first frame update
    void Start()
    {
        ptc = GetComponent<PlayerTeamController>();
        OnlinePlayer.ReceivedEncounter += OnOnlineEncounter;
    }

    public void OnOnlineEncounter(SerializableEncounter se, SerializableEncounter se2)
    {

        text.text = se.encounter[0].AvatarId.ToString();   
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger)
        {
            player = NetworkClient.connection.identity.GetComponent<OnlinePlayer>();
            player.SendEncounterData(new SerializableEncounter(ptc.GetMyTeamAsEncounter()), new SerializableEncounter(ptc.GetMyTeamAsEncounter()));
            trigger = false;
        }
        if (player == null)
        {
            try
            {
                player = NetworkClient.connection.identity.GetComponent<OnlinePlayer>();
            }
            catch { }
        }
    }
}
