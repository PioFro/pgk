using System.Collections;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

public class SerializableEncounter
{
    public SerializableCharacter[] encounter = new SerializableCharacter[Encounter.MaxEncounterCharacters];
    public int EncounterSpriteId;

    public SerializableEncounter() { }
    public SerializableEncounter(Encounter e)
    {
        int i = 0;
        foreach(Character character in e.CharactersInEncounter)
        {
            encounter[i] = new SerializableCharacter(character);
            i++;
        }
    }

    public int GetActualLenght()
    {
        int i = 0;
        for(;i<encounter.Length;i++)
        {
            if (encounter[i] == null)
                return i;
        }
        return encounter.Length;
    }
    public Encounter ToEncounter()
    {
        int i = 0;
        Encounter returnEncounter = new Encounter();
        returnEncounter.CharactersInEncounter = new Character[GetActualLenght()];
        returnEncounter.EncounterSprite = AssetProvider.SpriteStore.GetSpriteById(EncounterSpriteId);
        foreach(SerializableCharacter serializableCharacter in encounter)
        {
            if (serializableCharacter == null)
                break;
            returnEncounter.CharactersInEncounter[i] = serializableCharacter.ToCharacter();
            i++;
        }
        return returnEncounter;
    }

}
