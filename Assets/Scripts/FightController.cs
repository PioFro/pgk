using ScriptableObjects;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FightController : MonoBehaviour
{
    public GameObject[] TeamSlots = new GameObject[PlayerTeamController.MaxTeamSize];
    public GameObject[] EnemySlots = new GameObject[Encounter.MaxEncounterCharacters];

    private Character[] TeamCharacters;
    private Character[] EnemyCharacters;

    public Queue<Character> RoundQueue = new Queue<Character>();

    public delegate void FightStartedDelegate(Encounter encounter);

    public event FightStartedDelegate FightStarted;

    public delegate void FightFinishedDelegate();

    public event FightFinishedDelegate FightFinished;

    public delegate void CharactersEnqueuedDelegate(Character[] characters);

    public event CharactersEnqueuedDelegate CharactersEnqueued;

    public delegate void CharacterDequeuedDelegate();

    public event CharacterDequeuedDelegate CharacterDequeued;

    public static Encounter Enemy;

    private void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && Input.GetMouseButtonDown(0))
        {
            var gameObject = hit.collider.gameObject;

            if (gameObject.tag == "Enemy"||gameObject.tag=="Player")
            {
                for (int i = 0; i < EnemyCharacters.Length; i++)
                {
                    if (EnemySlots[i] == gameObject)
                    {
                        TakeAction(EnemyCharacters[i]);
                    }
                }
                for (int i = 0; i < TeamCharacters.Length; i++)
                {
                    if (TeamCharacters[i] == gameObject)
                    {
                        TakeAction(TeamCharacters[i]);
                    }
                }
            }
        }
    }

    public void SetupFight(Character[] teamCharacters, Character[] enemyCharacters)
    {
        Encounter enemy = new Encounter { CharactersInEncounter = enemyCharacters, EncounterSprite = AssetProvider.SpriteStore.placeholder };
        Enemy = enemy; 
        FightStarted?.Invoke(enemy);

        SetupTeam(teamCharacters);
        SetupEnemy(enemyCharacters);

        CreateRoundQueue();
    }

    public void SimulateComputerMove()
    {
        var targetCharacter = TeamCharacters.OrderByDescending(x => x.Stats.CurrentInitiative)
            .First(x => x.IsDead == false);

        TakeAction(targetCharacter);
    }

    public void TakeAction(Character targetCharacter)
    {
        if (RoundQueue.Count > 0)
        {
            //if (IsFightOver())
            //{
            //    FightFinished.Invoke();
            //    return;
            //}

            var character = RoundQueue.Dequeue();

            if (character.IsDead == false)
            {
                var rng = new System.Random();

                if (character.IsEnemy)
                {
                    if (targetCharacter.IsEnemy)
                    {
                        // Przeciwnik leczy/buffuje swojego kompana 
                    }
                    else
                    {
                        // Przeciwnik atakuje postac gracza
                        // Casts skill??
                        var dmg = (int)(character.Stats.Strength * 2.5) + rng.Next(1, 3);

                        //targetCharacter.OnHitPointsChanged(-dmg);
                        OnlineManagerHandler.staticPlayerReference.SendSkillData(new SerializableSkill { HitRatio = dmg }, targetCharacter.Stats.Id);
                    }
                }
                else
                {
                    if (targetCharacter.IsEnemy)
                    {
                        // Gracz atakuje postac przeciwnika
                        var dmg = (int)(character.Stats.Strength * 0.5) + rng.Next(1, 3);
                        //targetCharacter.OnHitPointsChanged(-dmg);
                        OnlineManagerHandler.staticPlayerReference.SendSkillData(new SerializableSkill { HitRatio = dmg }, targetCharacter.Stats.Id);
                    }
                    else
                    {
                        // Gracz leczy/buffuje swojego kompana
                    }
                }

            }

            // tutaj cos jest broken
            /*
           
            */
        }
        else
        {
            CreateRoundQueue();
        }
    }
    public void TickFight()
    {
        if (IsFightOver())
        {
            FightFinished.Invoke();
            return;
        }

        CharacterDequeued.Invoke();
    }

    private void CreateRoundQueue()
    {
        if (RoundQueue.Count == 0)
        {
            var allAliveCharacters = Enumerable.Empty<Character>()
                .Concat(TeamCharacters.Where(x => x.IsDead == false))
                .Concat(EnemyCharacters.Where(x => x.IsDead == false));

            allAliveCharacters = allAliveCharacters
                .OrderByDescending(x => x.Stats.CurrentInitiative)
                .ThenByDescending(x => x.Stats.CurrentHitPoints);

            foreach (var character in allAliveCharacters)
            {
                RoundQueue.Enqueue(character);
            }

            CharactersEnqueued?.Invoke(RoundQueue.ToArray());
        }
    }

    private void HandleCharacterKilled()
    {
        // zdupione?
        if (IsFightOver())
        {
            FightFinished.Invoke();
        }
    }

    public bool IsFightOver()
    {
        return TeamCharacters.All(x => x.IsDead == true) || EnemyCharacters.All(x => x.IsDead == true);
    }

    private void SetupEnemy(Character[] enemyCharacters)
    {
        EnemyCharacters = enemyCharacters;

        for (int i = 0; i < EnemyCharacters.Length; i++)
        {
            EnemyCharacters[i].HitPointsChanged += EnemySlots[i].GetComponent<HitPointsInfo>().ChangeHitPointText;
            EnemyCharacters[i].CharacterKilled += HandleCharacterKilled;

            EnemySlots[i].GetComponent<HitPointsInfo>().Character = EnemyCharacters[i];
            EnemySlots[i].GetComponent<HitPointsInfo>().ChangeHitPointText();
            EnemySlots[i].GetComponent<SpriteRenderer>().sprite = EnemyCharacters[i].FightSprite;
            EnemySlots[i].SetActive(true);
        }
    }
    private void SetupTeam(Character[] teamCharacters)
    {
        TeamCharacters = teamCharacters;

        for (int i = 0; i < TeamCharacters.Length; i++)
        {
            TeamSlots[i].GetComponent<SpriteRenderer>().sprite = TeamCharacters[i].FightSprite;
            TeamSlots[i].SetActive(true);
        }
    }
    public void DoActionOnId(int id, int dmg)
    {
        foreach(Character ch in EnemyCharacters)
        {
            if(ch.Stats.Id == id)
            {
                ch.OnHitPointsChanged(-dmg);
                return;
            }
        }
        foreach (Character ch in TeamCharacters)
        {
            if (ch.Stats.Id == id)
            {
                ch.OnHitPointsChanged(-dmg);
                return;
            }
        }

    }
}
