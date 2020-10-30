using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

[System.Serializable]
public class SerializableCharacter
{
    public SerializableCharacterStats CharacterStatistics;

    public int AvatarId;

    public int FightSpriteId;

    public SerializableCharacter()
    {

    }
    public SerializableCharacter(Character character)
    {
        CharacterStatistics = new SerializableCharacterStats(character.Stats);

        AvatarId = AssetProvider.SpriteStore.GetSpriteIdByName(character.Avatar.name);

        FightSpriteId = AssetProvider.SpriteStore.GetSpriteIdByName(character.FightSprite.name);
    }

    public Character ToCharacter()
    {
        Character returnCharacter = new Character();

        returnCharacter.Avatar = AssetProvider.SpriteStore.GetSpriteById(AvatarId);

        returnCharacter.FightSprite = AssetProvider.SpriteStore.GetSpriteById(FightSpriteId);

        returnCharacter.Stats = CharacterStatistics.ToCharacterStats();

        return returnCharacter;


    }
}
