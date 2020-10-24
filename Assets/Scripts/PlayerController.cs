using ScriptableObjects;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public PlayerUIController PlayerUIController;

    private PlayerTeamController PlayerTeamController;
    public Camera PlayerCamera;

    public GameObject FloorPrefab;

    public GameObject FightContainerPrefab;
    private GameObject FightContainerInstance;
    private FightController FightController;

    void Start()
    {
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("exit");
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());

            FloorPrefab.GetComponent<FloorController>().transform.position = CalculateFloorStart(collision.gameObject.GetComponent<ExitProperties>(), collision.gameObject.transform.position);
            Instantiate(FloorPrefab);
        }
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

    private Vector2 CalculateFloorStart(ExitProperties exitProperties, Vector2 position)
    {
        FloorController tmp = FloorPrefab.GetComponent<FloorController>();
        SpriteRenderer _floorSprite = tmp.floortile.GetComponent<SpriteRenderer>();
        float _incrementX = _floorSprite.bounds.size.x;
        float _incrementY = _floorSprite.bounds.size.x;

        if (exitProperties.direction == ExitProperties.Dir.LEFT)
        {
            FloorPrefab.GetComponent<FloorController>()._entry = position - new Vector2(_incrementX, _incrementY * 2);
            return position - new Vector2(tmp.sizeX * _incrementX, _incrementY);
        }
        if (exitProperties.direction == ExitProperties.Dir.RIGHT)
        {
            FloorPrefab.GetComponent<FloorController>()._entry = position + new Vector2(_incrementX, _incrementY * 2);
            return position + new Vector2(0, _incrementY);
        }
        if (exitProperties.direction == ExitProperties.Dir.UP)
        {
            FloorPrefab.GetComponent<FloorController>()._entry = position - new Vector2(_incrementX * 2, _incrementY);
            return position + new Vector2(_incrementX, 0);
        }
        if (exitProperties.direction == ExitProperties.Dir.DOWN)
        {
            FloorPrefab.GetComponent<FloorController>()._entry = position + new Vector2(_incrementX * 2, _incrementY);
            return position - new Vector2(_incrementX, _incrementY * tmp.sizeY);
        }
        return new Vector2(0, 0);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        switch (scene.name)
        {
            case "DungeonScene":
                GetComponent<Move>().enabled = true;
                GetComponent<SpriteRenderer>().enabled = true;
                PlayerCamera.enabled = true;

                if (FightContainerInstance != null)
                {
                    Destroy(FightContainerInstance);
                }
                break;
            case "CityScene":
                GetComponent<Move>().enabled = false;
                GetComponent<SpriteRenderer>().enabled = false;
                PlayerCamera.enabled = true;

                if (FightContainerInstance != null)
                {
                    Destroy(FightContainerInstance);
                }
                break;
            case "FightScene":
                GetComponent<Move>().enabled = false;
                PlayerCamera.enabled = false;
                break;
            default:
                break;
        }
    }
}
