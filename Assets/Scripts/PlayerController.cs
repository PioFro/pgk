using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PlayerUIController PlayerUIController;

    private PlayerTeamController PlayerTeamController;

    public GameObject FloorPrefab;

    void Start()
    {
        PlayerTeamController = GetComponent<PlayerTeamController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Finish"))
        {
            Debug.Log("exit");
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());

            FloorPrefab.GetComponent<FloorController>().transform.position = calculateFloorStart(collision.gameObject.GetComponent<ExitProperties>(), collision.gameObject.transform.position);
            Instantiate(FloorPrefab);

        }
        else if (gameObject.CompareTag("Player") && collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Enemy trigger enter");
            //SceneManager.SetActiveScene(SceneManager.GetSceneByName("FightScene"));
        }
    }

    private Vector2 calculateFloorStart(ExitProperties exitProperties, Vector2 position)
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

    // Update is called once per frame
    void Update()
    {
    }
}
