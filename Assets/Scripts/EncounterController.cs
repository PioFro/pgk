using ScriptableObjects;
using System.Linq;
using UnityEngine;

public class EncounterController : MonoBehaviour
{
    public Encounter Encounter;

    private void Start()
    {
        GetComponent<SpriteRenderer>().sprite = Encounter.EncounterSprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Encounter.CharactersInEncounter.All(x => x.IsDead))
            {
                Destroy(Encounter);
            }
            else
            {
                Encounter.EncounterTrigger();
            }
        }
    }
}
