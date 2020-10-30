using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableSkill
{

    public string Description;

    public int IconId;

    public float HitRatio;

    public float DamageMultiplier;//tmp attack value

    public SerializableSkill() { }
    public SerializableSkill(ScriptableObjects.Skill skill)
    {
        Description = skill.Description;
        HitRatio = skill.HitRatio;
        DamageMultiplier = skill.DamageMultiplier;
        IconId = AssetProvider.SpriteStore.GetSpriteIdByName(skill.Icon.name);
    }
}
