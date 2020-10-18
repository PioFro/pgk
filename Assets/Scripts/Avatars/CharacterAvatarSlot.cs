using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public abstract class CharacterAvatarSlot : MonoBehaviour
{
    public Image Image;

    protected Character Character;

    protected virtual void Start()
    {
        Image = GetComponent<Image>();
    }

    public virtual void AssignCharacter(Character character)
    {
        this.Character = character;

        if (character != null)
        {
            Image.sprite = character.Avatar;
        }
    }

    public void SetAvatar(Sprite sprite)
    {
        Image.sprite = sprite;
    }
}
