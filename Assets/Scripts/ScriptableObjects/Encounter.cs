using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/Create Encounter")]
    public class Encounter : ScriptableObject
    {
        public static int MaxEncounterCharacters = 4;

        public Character[] CharactersInEncounter = new Character[MaxEncounterCharacters];

        public Sprite EncounterSprite;

        public delegate void PlayerEncounteredDelegate(Character[] encounteredCharacters);

        public event PlayerEncounteredDelegate PlayerEncountered;

        public void EncounterTrigger()
        {
            Debug.Log("TriggerInEncounter");
            PlayerEncountered.Invoke(this.CharactersInEncounter);
        }
    }
}