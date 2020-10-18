using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/Create Character")]
    public class Character : ScriptableObject
    {
        public Sprite Avatar;

        public CharacterStats Stats;

        public delegate void CharacterKilledDelegate();

        public CharacterKilledDelegate CharacterKilled;
    }
}
