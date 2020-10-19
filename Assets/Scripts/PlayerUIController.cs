using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Button GoToDungeonButton;
    public GameObject TeamAvatars;
    public GameObject QueueAvatars;
    public GameObject ShopTeamAvatars;
    public GameObject ShopCandidatesAvatars;

    public Sprite CharacterDeadAvatar;

    public CharacterAvatarTeamSlot[] CharacterAvatarTeamSlots = new CharacterAvatarTeamSlot[4];
    public CharacterAvatarQueueSlot[] CharacterAvatarQueueSlots = new CharacterAvatarQueueSlot[8];

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

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "DungeonScene":
                GoToDungeonButton.gameObject.SetActive(false);
                ShopTeamAvatars.SetActive(false);
                ShopCandidatesAvatars.SetActive(false);

                TeamAvatars.SetActive(true);
                QueueAvatars.SetActive(true);
                break;
            case "CityScene":
                TeamAvatars.SetActive(false);
                QueueAvatars.SetActive(false);

                GoToDungeonButton.gameObject.SetActive(true);
                ShopTeamAvatars.SetActive(true);
                ShopCandidatesAvatars.SetActive(true);
                break;
            default:
                break;
        }
    }

    public void SetImageOnIndex(int index, Sprite img)
    {
        if (CharacterAvatarTeamIndexOutOfRange(index))
            return;

        CharacterAvatarTeamSlots[index].SetAvatar(img); 
    }

    //public void SetImageOnIndexActive(int index, bool active)
    //{
    //    if (CharacterAvatarTeamIndexOutOfRange(index))
    //        return;

    //    CharacterAvatarTeamSlots[index].GetComponent<Image>().gameObject.SetActive(active);
    //}

    public void ChangeTextOnIndex(int index, string newText)
    {
        if (CharacterAvatarTeamIndexOutOfRange(index))
            return;
        CharacterAvatarTeamSlots[index].GetComponentInChildren<Text>().text = newText;
    }

    public void SetDeadOnIndex(int index)
    {
        if (CharacterAvatarTeamIndexOutOfRange(index))
            return;

        CharacterAvatarTeamSlots[index].GetComponent<Image>().sprite = Properties.deadStaticSprite;
    }

    private bool CharacterAvatarTeamIndexOutOfRange(int index)
    {
        return index < 0 || index >= CharacterAvatarTeamSlots.Length; 
    }
}
