using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public Sprite CharacterDeadAvatar;

    public CharacterAvatarTeamSlot[] CharacterAvatarTeamSlots = new CharacterAvatarTeamSlot[4];
    public CharacterAvatarQueueSlot[] CharacterAvatarQueueSlots = new CharacterAvatarQueueSlot[8];

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
