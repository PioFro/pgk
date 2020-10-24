using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UnityEngine;

public class EncounterSpawner : MonoBehaviour
{
    public GameObject EncounterPrefab;

    public List<Encounter> AvailableEncounters = new List<Encounter>();

    public List<GameObject> SpawnedEncounters = new List<GameObject>();

    public List<Character> EnemyCharacters = new List<Character>();

    private PlayerController PlayerController;

    private void Start()
    {
        var obj = GameObject.FindGameObjectsWithTag("Player");

        if (obj[0] != null)
        {
            PlayerController = obj[0].GetComponent<PlayerController>();
        }

        SpawnWeakEncounter(new Vector2(18, 3.01f));
        SpawnStrongEncounter(new Vector2(20, 3.01f));
    }

    public void SpawnRandomEncounter(Vector2 position)
    {
        Encounter encounter = AvailableEncounters[Random.Range(0, AvailableEncounters.Count - 1)];

        if (encounter != null)
        {
            SpawnEncounter(position, encounter);
        }
    }

    public void SpawnStrongEncounter(Vector2 position)
    {
        Encounter encounter = AvailableEncounters.FirstOrDefault(e => e.name.Contains("Strong"));

        if (encounter != null)
        {
            SpawnEncounter(position, encounter);
        }
    }

    public void SpawnWeakEncounter(Vector2 position)
    {
        Encounter encounter = AvailableEncounters.FirstOrDefault(e => e.name.Contains("Weak"));

        if (encounter != null)
        {
            SpawnEncounter(position, encounter);
        }
    }

    private void SpawnEncounter(Vector2 position, Encounter encounter)
    {
        var spawnedEncounter = Instantiate(encounter);
        spawnedEncounter.PlayerEncountered += PlayerController.PlayerEncountered;

        for (int i = 0; i < spawnedEncounter.CharactersInEncounter.Length; i++)
        {
            spawnedEncounter.CharactersInEncounter[i] = Instantiate(spawnedEncounter.CharactersInEncounter[i]);
            EnemyCharacters.Add(spawnedEncounter.CharactersInEncounter[i]);
        }

        var encounterPrefabInstance = Instantiate(EncounterPrefab, new Vector3(position.x, position.y, -1), Quaternion.identity, this.transform);
        SpawnedEncounters.Add(encounterPrefabInstance);

        encounterPrefabInstance.GetComponent<EncounterController>().Encounter = spawnedEncounter;
    }
}
