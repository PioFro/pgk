using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/Create Skill")]
    public class Skill : ScriptableObject
    {
        public string Description;

        public Sprite Icon;

        public float HitRatio;

        public float DamageMultiplier;

    }
}
