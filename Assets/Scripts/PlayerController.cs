using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerUIController PlayerUIController;

    private PlayerTeamController PlayerTeamController;
    private PlayerFloorController PlayerFloorController;
    public Camera PlayerCamera;

    public GameObject FightContainerPrefab;
    private GameObject FightContainerInstance;
    private FightController FightController;

    void Start()
    {
        PlayerFloorController = GetComponent<PlayerFloorController>();
        PlayerTeamController = GetComponent<PlayerTeamController>();
        PlayerCamera = GetComponentInChildren<Camera>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    public void PlayerEncountered(Character[] encounteredCharacters)
    {
        Debug.Log("on encounter");

        SceneManager.LoadScene("FightScene", LoadSceneMode.Additive);

        FightContainerInstance = Instantiate(FightContainerPrefab);
        DontDestroyOnLoad(FightContainerInstance);
        FightController = FightContainerInstance.GetComponent<FightController>();

        PlayerUIController.SubscribeFightController(FightController);

        FightController.SetupFight(PlayerTeamController.Team, encounteredCharacters);
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "DungeonScene":
                GetComponent<Move>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
                GetComponent<PlayerFloorController>().enabled = true;
                PlayerCamera.enabled = true;

                if (FightContainerInstance != null)
                {
                    Destroy(FightContainerInstance);
                }
                break;
            case "CityScene":
                GetComponent<Move>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<PlayerFloorController>().enabled = false;
                PlayerCamera.enabled = true;

                if (FightContainerInstance != null)
                {
                    Destroy(FightContainerInstance);
                }
                break;
            case "FightScene":
                GetComponent<Move>().enabled = false;
                GetComponent<PlayerFloorController>().enabled = false;
                PlayerCamera.enabled = false;
                break;
            default:
                break;
        }
    }
}
