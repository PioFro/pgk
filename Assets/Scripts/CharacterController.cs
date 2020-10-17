using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public Property Property { get; set; }

    public Character Character;

    public CharacterController(int id)
    {
        //Strength = Random.Range(1, 10);
        //Dexterity = Random.Range(1, 10);
        //Intelligence = Random.Range(1, 10);
        //HitPoints = Random.Range(10, 20);
        //CurrentHitPoints = HitPoints;
        Property = new Property { avatar = Properties.getRandomAvatar() };
        //this.Id = id;
        OnHit(0);
    }

    public CharacterController(int str, int dex, int hp, int intelligence, int id)
    {
        //Strength = str;
        //Dexterity = dex;
        //HitPoints = hp;
        //CurrentHitPoints = hp;
        //Intelligence = intelligence;
        //this.Id = id;
        OnHit(0);
    }

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
