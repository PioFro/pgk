using ScriptableObjects;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Character Character;

    public void OnHit(int dmg)
    {
        //CurrentHitPoints -= dmg;
        Debug.Log("Character: X" + " received " + dmg + " amount of dmg");
        //if (CurrentHitPoints > 0)
        //    PlayerController.playerUIController.ChangeTextOnIndex(Id, "HP: " + CurrentHitPoints + "/" + HitPoints);
        //else
        //{
        //    PlayerController.playerUIController.ChangeTextOnIndex(Id, "DEAD");
        //    PlayerController.playerUIController.SetDeadOnIndex(Id);
        //}
    }   
}
