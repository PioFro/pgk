using ScriptableObjects;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerUIController : MonoBehaviour
{
    public GameObject GoToDungeonButton;
    public GameObject TeamAvatars;
    public GameObject QueueAvatars;
    public GameObject ShopTeamAvatars;
    public GameObject ShopCandidatesAvatars;

    public Sprite CharacterDeadAvatar;

    public CharacterAvatarTeamSlot[] CharacterAvatarTeamSlots = new CharacterAvatarTeamSlot[4];
    public CharacterAvatarQueueSlot[] CharacterAvatarQueueSlots = new CharacterAvatarQueueSlot[8];

    private FightController FightController;

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void AssignCharacterToTeamSlot(Character character, int slotIndex)
    {
        CharacterAvatarTeamSlots[slotIndex].AssignCharacter(character);
    }

    public void GoToDungeon()
    {
        SceneManager.LoadScene("DungeonScene");
    }

    public void SubscribeFightController(FightController fightController)
    {
        FightController = fightController;
        FightController.FightStarted += OnFightStarted;
        FightController.FightFinished += OnFightFinished;
    }

    private void OnFightStarted()
    {
        FightController.CharactersEnqueued += PushCharactersToQueueSlots;
        FightController.CharacterDequeued += PopCharacterFromQueueSlot;
    }

    private void OnFightFinished()
    {
        FightController.CharactersEnqueued -= PushCharactersToQueueSlots;
        FightController.CharacterDequeued -= PopCharacterFromQueueSlot;

        SceneManager.LoadScene("DungeonScene");
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "DungeonScene":
                GoToDungeonButton.SetActive(false);
                ShopTeamAvatars.SetActive(false);
                ShopCandidatesAvatars.SetActive(false);

                TeamAvatars.SetActive(true);
                QueueAvatars.SetActive(false);
                break;
            case "CityScene":
                TeamAvatars.SetActive(false);
                QueueAvatars.SetActive(false);

                GoToDungeonButton.SetActive(true);
                ShopTeamAvatars.SetActive(true);
                ShopCandidatesAvatars.SetActive(true);
                break;
            case "FightScene":
                QueueAvatars.SetActive(true);

                break;
            default:
                break;
        }
    }

    private void PopCharacterFromQueueSlot()
    {
        var activeQueueSlotsCount = CharacterAvatarQueueSlots.Count(x => x.HasAssignedCharacter());

        for (int i = 1; i < activeQueueSlotsCount; i++)
        {
            CharacterAvatarQueueSlots[i - 1].AssignCharacter(CharacterAvatarQueueSlots[i].GetAssignedCharacter());
        }

        CharacterAvatarQueueSlots[activeQueueSlotsCount - 1].AssignCharacter(null);
        CharacterAvatarQueueSlots[activeQueueSlotsCount - 1].gameObject.SetActive(false);

        if (activeQueueSlotsCount == 1)
        {
            QueueAvatars.SetActive(false);
        }
    }

    private void PushCharactersToQueueSlots(Character[] characters)
    {
        for (int i = 0; i < characters.Length; i++)
        {
            var queueSlot = CharacterAvatarQueueSlots[i];

            queueSlot.AssignCharacter(characters[i]);

            queueSlot.gameObject.SetActive(true);
        }

        QueueAvatars.SetActive(true);
    }
}
