using UnityEngine;
namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/Create Encounter")]
    public class Encounter : ScriptableObject
    {
        public static int MaxEncounterCharacters = 4;

        public Character[] CharactersInEncounter = new Character[MaxEncounterCharacters];

        public Sprite EncounterSprite;

        public delegate void OnEncounter(Character[] encounteredCharacters);

        public event OnEncounter EncounterEvent;

        public void EncounterTrigger()
        {
            Debug.Log("TriggerInEncounter");
            EncounterEvent.Invoke(this.CharactersInEncounter);
        }
    }
}