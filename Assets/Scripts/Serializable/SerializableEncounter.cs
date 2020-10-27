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

}
