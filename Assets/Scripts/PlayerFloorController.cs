using UnityEngine;

public class PlayerFloorController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject floorPrefab;

    public GameObject DungeonContainer;

    public EncounterSpawner EncounterSpawner;
    public static EncounterSpawner EncounterSpawnerStatic;

    private void Awake()
    {
        EncounterSpawnerStatic = EncounterSpawner;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            Debug.Log("exit");
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());

            floorPrefab.GetComponent<FloorController>().transform.position = CalculateFloorStart(collision.gameObject.GetComponent<ExitProperties>(), collision.gameObject.transform.position);
            Instantiate(floorPrefab);
            floorPrefab.GetComponent<FloorController>()._upExit = true;
            floorPrefab.GetComponent<FloorController>()._downExit = true;
            floorPrefab.GetComponent<FloorController>()._leftExit = true;
            floorPrefab.GetComponent<FloorController>()._rightExit = true;
        }
    }
    private Vector2 CalculateFloorStart(ExitProperties exitProperties, Vector2 position)
    {
        FloorController tmp = floorPrefab.GetComponent<FloorController>();
        SpriteRenderer _floorSprite = tmp.floortile.GetComponent<SpriteRenderer>();
        float _incrementX = _floorSprite.bounds.size.x;
        float _incrementY = _floorSprite.bounds.size.x;

        if (exitProperties.direction == ExitProperties.Dir.LEFT)
        {
            floorPrefab.GetComponent<FloorController>()._entry = position - new Vector2(_incrementX, 0);
            floorPrefab.GetComponent<FloorController>()._rightExit = false;
            return position - new Vector2(tmp.sizeX * _incrementX, _incrementY);
        }
        if (exitProperties.direction == ExitProperties.Dir.RIGHT)
        {
            floorPrefab.GetComponent<FloorController>()._entry = position + new Vector2(_incrementX, 0);
            floorPrefab.GetComponent<FloorController>()._leftExit = false;
            return position + new Vector2(_incrementX, -_incrementY);
        }
        if (exitProperties.direction == ExitProperties.Dir.UP)
        {
            floorPrefab.GetComponent<FloorController>()._entry = position + new Vector2(0, _incrementY);
            floorPrefab.GetComponent<FloorController>()._downExit = false;
            return position - new Vector2(_incrementX, -_incrementY);
        }
        if (exitProperties.direction == ExitProperties.Dir.DOWN)
        {
            floorPrefab.GetComponent<FloorController>()._entry = position - new Vector2(0, _incrementY);
            floorPrefab.GetComponent<FloorController>()._upExit = false;
            return position - new Vector2(_incrementX, _incrementY * tmp.sizeY);
        }
        return new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
