using ScriptableObjects;
using UnityEngine;

public class PlayerTeamController : MonoBehaviour
{
    public Character[] Team = new Character[MaxTeamSize];

    private const int MaxTeamSize = 4;

    public PlayerUIController PlayerUIController;

    public CharacterSpawner CharacterSpawner;

    void Start()
    {
        CreateTeam();
    }

    private void Update()
    {
        //foreach (var character in Team)
        //    while (character.Stats.CurrentHitPoints > 0)
        //        character.OnHitPointsChanged(-1);
    }

    public void AssignCharacterToNextFreeSlot(Character character)
    {
        for (int i = 0; i < MaxTeamSize; i++)
        {
            if (Team[i] == null)
            {
                Team[i] = character;

                return;
            }
        }
    }

    private void CreateTeam()
    {
        for (int i = 0; i < MaxTeamSize; i++)
        {
            var character = CharacterSpawner.SpawnCharacter();

            Team[i] = character;

            PlayerUIController.AssignCharacterToTeamSlot(character, i);
        }
    }
    public Encounter GetMyTeamAsEncounter()
    {
        Encounter enc = new Encounter();
        enc.CharactersInEncounter = this.Team;
        
        //tmp
        enc.EncounterSprite = this.Team[0].Avatar;

        return enc;
    }
}
