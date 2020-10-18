using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/Create Character")]
    public class Character : ScriptableObject
    {
        public Sprite Avatar;

        public CharacterStats Stats;

        public bool IsDead { get { return Stats.CurrentHitPoints == 0; } }

        public delegate void CharacterKilledDelegate();

        public event CharacterKilledDelegate CharacterKilled;

        public delegate void CharacterHipPointsChangedDelegate(int hitPointsChange);

        public event CharacterHipPointsChangedDelegate HitPointsChanged;

        public void OnHitPointsChanged(int hitPointsChange)
        {
            Stats.CurrentHitPoints = Mathf.Max(0, Mathf.Min(Stats.CurrentHitPoints + hitPointsChange, Stats.HitPoints));

            Debug.Log($"Character: {Stats.Id}" + " received " + hitPointsChange + " amount of dmg");

            HitPointsChanged.Invoke(hitPointsChange);

            if (Stats.CurrentHitPoints == 0)
            {
                CharacterKilled.Invoke();
            }
        }
    }
}
