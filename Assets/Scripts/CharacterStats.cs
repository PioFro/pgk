using System.Collections.Generic;
using System.Linq;
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

    public int Initiative;

    public int CurrentInitiative;

    [Header("Character Attributes")]
    public List<CharacterAttributes> Attributes = new List<CharacterAttributes>();

    [Header("Character Skills")]
    public List<CharacterSkills> Skills = new List<CharacterSkills>();

    public int Strength
    {
        get
        {
            return Attributes.GetAttributeValue("Strength");
        }
        set
        {
            Attributes.First(x => x.Attribute.name == "Strength").Value = value;
        }
    }

    public int Dexterity
    {
        get
        {
            return Attributes.GetAttributeValue("Dexterity");
        }
        set
        {
            Attributes.First(x => x.Attribute.name == "Dexterity").Value = value;
        }
    }

    public int Intelligence
    {
        get
        {
            return Attributes.GetAttributeValue("Intelligence");
        }
        set
        {
            Attributes.First(x => x.Attribute.name == "Intelligence").Value = value;
        }
    }
}
