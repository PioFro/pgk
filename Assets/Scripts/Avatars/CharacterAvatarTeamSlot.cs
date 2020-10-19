using ScriptableObjects;
using UnityEngine.UI;

public class CharacterAvatarTeamSlot : CharacterAvatarSlot
{
    public Text Text;

    new void Start()
    {
        base.Start();

        Text = GetComponentInChildren<Text>();
    }

    new public void AssignCharacter(Character character)
    {
        if (character != null)
        {
            base.AssignCharacter(character);

            ChangeHitPointsText();

            character.CharacterKilled += SetCharacterDeadAvatar;
            character.HitPointsChanged += ChangeHitPointsText;
        }
    }

    protected void SetCharacterDeadAvatar()
    {
        Image.sprite = GetComponentInParent<PlayerUIController>().CharacterDeadAvatar;
        SetText("DEAD");
    }

    protected void ChangeHitPointsText(int hitPointsChange = 0)
    {
        // hitPointsChange can be usefull if we want to display damage digits above characters model
        SetText($"HP: {Character.Stats.CurrentHitPoints}/{Character.Stats.HitPoints}");
    }

    private void SetText(string text)
    {
        this.Text.text = text;
    }
}
