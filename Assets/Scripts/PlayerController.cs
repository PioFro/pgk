using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private PlayerTeamController PlayerTeamController;

    public GameObject FloorPrefab;

    void Start()
    {
        PlayerTeamController = GetComponent<PlayerTeamController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy trigger enter");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("FightScene"));
        }
    }

    
    // Update is called once per frame
    void Update()
    {
    }
}
