using ScriptableObjects;
using TMPro;
using UnityEngine.UI;

public class CharacterShopAvatarSlot : CharacterAvatarSlot
{
    public TextMeshProUGUI NameText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI HitPointsText;
    public TextMeshProUGUI InitiativeText;
    public TextMeshProUGUI StrengthText;
    public TextMeshProUGUI DexterityText;
    public TextMeshProUGUI IntelligenceText;

    public Button ActionButton;

    public delegate void ActionButtonPressed(CharacterShopAvatarSlot characterShopAvatarSlot);

    public event ActionButtonPressed OnActionButtonPressed;

    new void Start()
    {
        base.Start();
    }

    new public void AssignCharacter(Character character)
    {
        if (character != null)
        {
            base.AssignCharacter(character);

            NameText.text = character.Stats.CharacterName;
            LevelText.text = $"Level {character.Stats.Level}";
            HitPointsText.text = $"HP {character.Stats.HitPoints}";
            InitiativeText.text = $"Initiative {character.Stats.Initiative}";
            StrengthText.text = $"STR {character.Stats.Attributes.GetAttributeValue("Strength")}";
            DexterityText.text = $"DEX {character.Stats.Attributes.GetAttributeValue("Dexterity")}";
            IntelligenceText.text = $"INT {character.Stats.Attributes.GetAttributeValue("Intelligence")}";

            gameObject.SetActive(true);
        }
    }

    public void UnassignCharacter()
    {
        Character = null;
        Image.sprite = null;
        gameObject.SetActive(false);
    }

    public Character GetAssignedCharacter()
    {
        return Character;
    }

    public void OnUIActionButtonPressed()
    {
        OnActionButtonPressed.Invoke(this);
    }
}