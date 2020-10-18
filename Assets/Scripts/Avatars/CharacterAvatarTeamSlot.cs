using ScriptableObjects;
using UnityEngine.UI;

public class CharacterAvatarTeamSlot : CharacterAvatarSlot
{
    private Text Text;

    new void Start()
    {
        base.Start();

        Text = GetComponentInChildren<Text>();
    }

    new protected void AssignCharacter(Character character)
    {
        if (character != null)
        {
            base.AssignCharacter(character);

            character.CharacterKilled += SetCharacterDeadAvatar;
        }
    }

    public void SetText(string text)
    {
        this.Text.text = text;
    }

    protected void SetCharacterDeadAvatar()
    {
        Image.sprite = GetComponentInParent<PlayerUIController>().CharacterDeadAvatar;
        Text.text = "DEAD";
    }
}
