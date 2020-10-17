using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStats
{
    public int Id;

    public string CharacterName;

    [Header("Main Character Stats")]
    public int Level;

    public int ExperiencePoints;

    public int HitPoints;

    public int CurrentHitPoints;

    [Header("Character Attributes")]
    public List<CharacterAttributes> Attributes = new List<CharacterAttributes>();

    [Header("Character Skills")]
    public List<CharacterSkills> Skills = new List<CharacterSkills>();
}
