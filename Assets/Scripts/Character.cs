using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
public class Character
{
    public int id { get; set; }
    public int STR { get; set; }
    public int DEX { get; set; }
    public int HP { get; set; }
    public int currentHP { get; set; }
    public int INT { get; set; }
    public Property _property { get; set; }
    public List<Skill> _skills { get; set;}



    public Character(int id)
    {
        STR = Random.Range(1, 10);
        DEX = Random.Range(1, 10);
        INT = Random.Range(1, 10);
        HP = Random.Range(10, 20);
        currentHP = HP;
        _property = new Property { avatar = Properties.getRandomAvatar() };
        this.id = id;
        OnHit(0);
    }
    public Character(int str, int dex, int hp, int intel,int id)
    {
        STR = str;
        DEX = dex;
        HP = hp;
        currentHP = hp;
        INT = intel;
        this.id = id;
        OnHit(0);
    }

    public void OnHit(int dmg)
    {
        currentHP -= dmg;
        Debug.Log("Character: " + this.id + " received " + dmg + " amount of dmg");
        if (currentHP > 0)
            PlayerTeamController.uIController.ChangeTextOnIndex(id, "HP: " + currentHP + "/" + HP);
        else
        {
            PlayerTeamController.uIController.ChangeTextOnIndex(id, "DEAD");
            PlayerTeamController.uIController.SetDeadOnIndex(id);
        }
    }   
}
