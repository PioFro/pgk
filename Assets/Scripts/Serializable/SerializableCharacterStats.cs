using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableCharacterStats
{
    public int Id;

    public string CharacterName;

    [Header("Main Character Stats")]
    public int Level;

    public int HitPoints;

    public int CurrentHitPoints;

    public int CurrentInitiative;

    [Header("Character Attributes")]
    public List<CharacterAttributes> Attributes = new List<CharacterAttributes>();

    public SerializableCharacterStats() {}
    public SerializableCharacterStats(CharacterStats stats)
    {
        Id = stats.Id;
        CharacterName = stats.CharacterName;
        Level = stats.Level;
        HitPoints = stats.HitPoints;
        CurrentHitPoints = stats.CurrentHitPoints;
        CurrentInitiative = stats.CurrentInitiative;
        Attributes = stats.Attributes;
    }
}
