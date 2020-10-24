using ScriptableObjects;

public class CharacterAvatarQueueSlot : CharacterAvatarSlot
{
    new void Start()
    {
        base.Start();
    }

    public bool HasAssignedCharacter()
    {
        return Character != null;
    }

    public Character GetAssignedCharacter()
    {
        return Character;
    }
}
