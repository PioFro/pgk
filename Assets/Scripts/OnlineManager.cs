using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnlineManager : MonoBehaviour
{
    public void StopConnection()
    {
        GetComponent<NetworkManager>().StopHost();
        try
        {
            GetComponent<NetworkManager>().StopServer();
        }
        catch { }
   }
    public void StartServer()
    {
        //tmp -> matchmaking here

        GetComponent<NetworkManager>().networkAddress = "localhost";
        GetComponent<NetworkManager>().StartHost();
    }

    public void JoinServer()
    {
        //tmp -> matchmaking here

        GetComponent<NetworkManager>().networkAddress = "localhost";
        GetComponent<NetworkManager>().StartClient();
    }
    //Check if there is two players
    public bool CheckNumberOfPlayers()
    {
        return GetComponent<NetworkManager>().numPlayers == 2;
    }
}
