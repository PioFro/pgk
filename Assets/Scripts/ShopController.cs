using ScriptableObjects;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    public int MaxTeamSize;

    public int MaxCandidatesAmount;

    public CharacterSpawner CharacterSpawner;

    public PlayerTeamController PlayerTeamController;

    public CharacterShopAvatarSlot[] CharacterShopAvatarTeamSlots = new CharacterShopAvatarSlot[4];
    public CharacterShopAvatarSlot[] CharacterShopAvatarCandidatesSlots = new CharacterShopAvatarSlot[6];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < MaxCandidatesAmount; i++)
        {
            var character = CharacterSpawner.SpawnCharacter();

            CharacterShopAvatarCandidatesSlots[i].AssignCharacter(character);

            CharacterShopAvatarCandidatesSlots[i].OnActionButtonPressed += OnAddToTeam;
        }

        for (int i = 0; i< MaxTeamSize; i++)
        {
            CharacterShopAvatarTeamSlots[i].OnActionButtonPressed += OnRemoveFromTeam;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnRemoveFromTeam(CharacterShopAvatarSlot characterShopAvatarSlot)
    {
        //var 
        if (CharacterShopAvatarCandidatesSlots.Any(x => x.GetAssignedCharacter() == null))
        {
            var hasIndex = false;
            var index = -1;

            Character character = null;

            var activeCandidatesLength = CharacterShopAvatarTeamSlots.Count(x => x.GetAssignedCharacter() != null);

            for (int i = 0; i < activeCandidatesLength; i++)
            {
                if (CharacterShopAvatarTeamSlots[i] == characterShopAvatarSlot)
                {
                    hasIndex = true;
                    index = i;
                    break;
                }

            }

            if (hasIndex)
            {
                character = characterShopAvatarSlot.GetAssignedCharacter();

                for (int j = index + 1; j < activeCandidatesLength; j++, index++)
                {
                    CharacterShopAvatarTeamSlots[index].AssignCharacter(CharacterShopAvatarTeamSlots[j].GetAssignedCharacter());
                    PlayerTeamController.Team[index] = PlayerTeamController.Team[j];
                }

                CharacterShopAvatarTeamSlots[index].UnassignCharacter();
                PlayerTeamController.Team[index] = null;

                AssignToFirstFreeSlotInCandidates(character);
            }
        }
    }

    public void OnAddToTeam(CharacterShopAvatarSlot characterShopAvatarSlot)
    {
        if (PlayerTeamController.Team.Any(x => x == null))
        {
            var hasIndex = false;
            var index = -1;

            Character character = null;

            var activeCandidatesLength = CharacterShopAvatarCandidatesSlots.Count(x => x.GetAssignedCharacter() != null);

            for (int i = 0; i < activeCandidatesLength; i++)
            {
                if (CharacterShopAvatarCandidatesSlots[i] == characterShopAvatarSlot)
                {
                    hasIndex = true;
                    index = i;
                    break;
                }

            }

            if (hasIndex)
            {
                character = characterShopAvatarSlot.GetAssignedCharacter();

                for (int j = index + 1; j < activeCandidatesLength; j++, index++)
                {
                    CharacterShopAvatarCandidatesSlots[index].AssignCharacter(CharacterShopAvatarCandidatesSlots[j].GetAssignedCharacter());
                }

                CharacterShopAvatarCandidatesSlots[index].UnassignCharacter();

                PlayerTeamController.AssignCharacterToNextFreeSlot(character);

                AssignToFirstFreeSlotInTeam(character);
            }
        }
    }

    private void AssignToFirstFreeSlotInTeam(Character character)
    {
        if (character != null)
        {
            for (int i = 0; i < CharacterShopAvatarTeamSlots.Length; i++)
            {
                if (CharacterShopAvatarTeamSlots[i].GetAssignedCharacter() == null)
                {
                    CharacterShopAvatarTeamSlots[i].AssignCharacter(character);
                    break;
                }

            }
        }
    }

    private void AssignToFirstFreeSlotInCandidates(Character character)
    {
        if (character != null)
        {
            for (int i = 0; i < CharacterShopAvatarCandidatesSlots.Length; i++)
            {
                if (CharacterShopAvatarCandidatesSlots[i].GetAssignedCharacter() == null)
                {
                    CharacterShopAvatarCandidatesSlots[i].AssignCharacter(character);
                    break;
                }

            }
        }
    }
}
