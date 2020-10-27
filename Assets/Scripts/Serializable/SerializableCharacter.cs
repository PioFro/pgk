using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

[System.Serializable]
public class SerializableCharacter
{
    public SerializableCharacterStats CharacterStatistics;

    public int AvatarId;

    public SerializableCharacter()
    {

    }
    public SerializableCharacter(Character character)
    {
        CharacterStatistics = new SerializableCharacterStats(character.Stats);

        AvatarId = AssetProvider.SpriteStore.GetSpriteIdByName(character.Avatar.name);
    }
}
