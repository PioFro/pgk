using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineManager : MonoBehaviour
{
    public void StartServer()
    {
        GetComponent<NetworkManager>().StartHost();
    }

    public void JoinServer()
    {
        GetComponent<NetworkManager>().StartClient();
    }
    //Check if there is two players
    public bool CheckNumberOfPlayers()
    {
        return GetComponent<NetworkManager>().numPlayers == 2;
    }
}
