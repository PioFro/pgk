using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "SO/Create Attribute")]
    public class Attribute : ScriptableObject
    {
        public string Description;

        public Sprite Icon;
    }
}
