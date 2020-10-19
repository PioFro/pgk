using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
    public GameObject EncounterPrefab;

    public List<Encounter> Encounters = new List<Encounter>();

    private void Start()
    {
        //AddEncounter(new Vector2(18, 3.01f));
    }

    public void OnEncouter(Character[] characters)
    {
        // TODO: Here create fight scene (with enemies)
        Debug.Log("on encounter");
    }
    public void AddEncounter(Vector2 position)
    {
        Encounter enc = Encounters[Random.Range(0, Encounters.Count - 1)];
        enc.EncounterEvent += OnEncouter;
        EncounterPrefab.GetComponent<EncounterInstance>().EncounterData = enc;
        Instantiate(EncounterPrefab, new Vector3(position.x,position.y,-1), Quaternion.identity);
    }
}
