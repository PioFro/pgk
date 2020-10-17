using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTeamController : MonoBehaviour
{
    public List<Character> team = new List<Character>();
    public int teamSize = 4;
    public static PlayerUIController uIController;
    // Start is called before the first frame update
    private void CreateTeam()
    {
        for (int i = 0; i < teamSize; i++)
        {
            team.Add(new Character(i));
            uIController.SetImageOnIndex(i, team[i]._property.avatar);
            uIController.SetImageOnIndexActive(i, true);
        }

    }
    void Start()
    {
        uIController = GetComponentInChildren<PlayerUIController>();
        Debug.Log(uIController.ToString());
        CreateTeam();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
