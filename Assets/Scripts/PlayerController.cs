using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update

    public List<Character> team = new List<Character>();
    public int teamSize = 4;
    public static PlayerUIController uIController;
    public GameObject floorPrefab;

    private void CreateTeam()
    {
        for(int i=0;i<teamSize;i++)
        {
            team.Add(new Character(i));
            uIController.SetImageOnIndex(i, team[i]._property.avatar);
            uIController.SetImageOnIndexActive(i, true);
        }

    }
    void Start()
    {
        uIController = GetComponentInChildren<PlayerUIController>();
        Debug.Log(uIController.ToString());
        CreateTeam();
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Finish")
        {
            Debug.Log("exit");
            Destroy(collision.gameObject.GetComponent<BoxCollider2D>());

            floorPrefab.GetComponent<FloorController>().transform.position = calculateFloorStart(collision.gameObject.GetComponent<ExitProperties>(),collision.gameObject.transform.position);
            Instantiate(floorPrefab);

        }    
    }
    private Vector2 calculateFloorStart(ExitProperties exitProperties, Vector2 position)
    {
        FloorController tmp = floorPrefab.GetComponent<FloorController>();
        SpriteRenderer _floorSprite = tmp.floortile.GetComponent<SpriteRenderer>();
        float _incrementX = _floorSprite.bounds.size.x;
        float _incrementY = _floorSprite.bounds.size.x;

        if (exitProperties.direction == ExitProperties.Dir.LEFT)
        {
            floorPrefab.GetComponent<FloorController>()._entry = position - new Vector2(_incrementX,_incrementY*2);
            return position- new Vector2(tmp.sizeX * _incrementX, _incrementY);
        }
        if (exitProperties.direction == ExitProperties.Dir.RIGHT)
        {
            floorPrefab.GetComponent<FloorController>()._entry = position + new Vector2(_incrementX, _incrementY * 2);
            return position + new Vector2(0, _incrementY);
        }
        if (exitProperties.direction == ExitProperties.Dir.UP)
        {
            floorPrefab.GetComponent<FloorController>()._entry = position - new Vector2(_incrementX*2, _incrementY);
            return position + new Vector2(_incrementX, 0);
        }
        if (exitProperties.direction == ExitProperties.Dir.DOWN)
        {
            floorPrefab.GetComponent<FloorController>()._entry = position + new Vector2(_incrementX*2, _incrementY);
            return position - new Vector2(_incrementX, _incrementY*tmp.sizeY);
        }
        return new Vector2(0, 0);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
