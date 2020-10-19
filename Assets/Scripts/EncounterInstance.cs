using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class EncounterInstance : MonoBehaviour
{
    public Encounter EncounterData;
    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = EncounterData.EncounterSprite;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            EncounterData.EncounterTrigger();
        }
    }



}
