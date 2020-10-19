using ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public List<Character> AvailableCharacters = new List<Character>();

    public List<Character> EnemyCharacters = new List<Character>();

    public List<Character> SpawnedCharacters = new List<Character>();

    public Character SpawnCharacter()
    {
        var availableCharactersCount = AvailableCharacters.Count;

        if (availableCharactersCount > 0)
        {
            var index = Random.Range(0, availableCharactersCount - 1);
            var character = Instantiate(AvailableCharacters[index]);

            SpawnedCharacters.Add(character);

            RandomizeCharacterStats(character);
            LogSpawnInfo(character);

            return character;
        }

        return null;
    }

    private void RandomizeCharacterStats(Character character)
    {
        character.Stats.Id = 100 + SpawnedCharacters.Count;
        character.Stats.CurrentHitPoints = character.Stats.HitPoints = Random.Range(10, 21);
        character.Stats.CurrentInitiative = character.Stats.Initiative = Random.Range(5, 13);
        character.Stats.Attributes.ForEach(x => x.Value = Random.Range(1, 11));
    }

    private void LogSpawnInfo(Character character)
    {
        Debug.Log($@" Character spawned:
            Id = {character.Stats.Id},
            Name = {character.Stats.CharacterName},
            Max HP = {character.Stats.HitPoints},
            Initiative = {character.Stats.Initiative},
            STR = {character.Stats.Attributes.GetAttributeValue("Strength")},
            DEX = { character.Stats.Attributes.GetAttributeValue("Dexterity")},
            INT = {character.Stats.Attributes.GetAttributeValue("Intelligence")}
        ");
    }

    void Start()
    {
        //for (int i = 0; i < 10; i++)
        //{
        //    SpawnCharacter();
        //}


        // TODO: Read info about saved characters from serialized data file.
    }
}
