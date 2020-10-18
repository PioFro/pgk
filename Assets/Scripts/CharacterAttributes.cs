using ScriptableObjects;

[System.Serializable]
public class CharacterAttributes
{
    public Attribute Attribute;

    public int Value;

    public CharacterAttributes(Attribute attribute, int value)
    {
        Attribute = attribute;
        Value = value;
    }
}
