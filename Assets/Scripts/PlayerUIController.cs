using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Sprite CharacterDeadAvatar;

    public CharacterAvatarTeamSlot[] CharacterAvatarTeamSlots = new CharacterAvatarTeamSlot[4];
    public CharacterAvatarQueueSlot[] CharacterAvatarQueueSlots = new CharacterAvatarQueueSlot[8];

    public void Awake()
    {
        GameObject avatars = transform.Find("TeamAvatars").gameObject;
        GameObject queue = transform.Find("QueueAvatars").gameObject;
        for (int i = 0; i < CharacterAvatarTeamSlots.Length;i++)
        {
            CharacterAvatarTeamSlots[i] = avatars.transform.Find("Slot" + (1 + i)).GetComponent<CharacterAvatarTeamSlot>();
        }
        for (int i = 0; i < CharacterAvatarQueueSlots.Length; i++)
        {
            CharacterAvatarQueueSlots[i] = queue.transform.Find("QueueSlot" + (1 + i)).GetComponent<CharacterAvatarQueueSlot>();
        }
    }
    public void AssignCharacterToTeamSlot(Character character, int slotIndex)
    {
        CharacterAvatarTeamSlots[slotIndex].AssignCharacter(character);
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

        CharacterAvatarTeamSlots[index].GetComponent<Image>().sprite = AssetProvider.SpriteStore.GetSpriteByName("Dead1");
    }

    private bool CharacterAvatarTeamIndexOutOfRange(int index)
    {
        return index < 0 || index >= CharacterAvatarTeamSlots.Length; 
    }
}
