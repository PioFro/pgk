using ScriptableObjects;

[System.Serializable]
public class CharacterAttributes
{
    public Attribute attribute;
    public int value;

    public CharacterAttributes(Attribute attribute, int value)
    {
        this.attribute = attribute;
        this.value = value;
    }
}
