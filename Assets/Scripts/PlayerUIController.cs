using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
    public GameObject[] avatarObjects = new GameObject[4];

    public void SetImageOnIndex(int id, Sprite img)
    {
        if (id < 0 || id > 3)
            return;

        avatarObjects[id].GetComponent<Image>().sprite = img; 
    }
    public void SetImageOnIndexActive(int id, bool active)
    {
        if (id < 0 || id > 3)
            return;

        avatarObjects[id].GetComponent<Image>().gameObject.SetActive(active);
    }
    public void ChangeTextOnIndex(int id, string newText)
    {
        if (id < 0 || id > 3)
            return;
        avatarObjects[id].GetComponentInChildren<Text>().text = newText;
    }
    public void SetDeadOnIndex(int id)
    {
        if (id < 0 || id > 3)
            return;

        avatarObjects[id].GetComponent<Image>().sprite = Properties.deadStaticSprite;
    }

}
