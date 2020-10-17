using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject floortile;
    public GameObject fogTile;
    public GameObject walltile;
    public GameObject exitTile;
    public GameObject entryTile;

    List<Vector2> _possiblePositionsTile = new List<Vector2>();
    List<Vector2> _possiblePositionsWall = new List<Vector2>();
    List<Vector2> _exits = new List<Vector2>();
    public Vector2 _entry = new Vector2(0,0);


    public GameObject player;

    public int sizeX = 20;
    public int sizeY = 20;
    /**
     * <2,min(sizeX,sizeY)>
     */
    public int numberOfExits = 2;


    private SpriteRenderer _floorSprite;
    private float _incrementX = 0;
    private float _incrementY = 0;

    void Start()
    {

        if (numberOfExits > sizeX || numberOfExits > sizeY || numberOfExits<2)
            numberOfExits = 2;

        _floorSprite = floortile.GetComponent<SpriteRenderer>();
        _incrementX = _floorSprite.bounds.size.x;
        _incrementY = _floorSprite.bounds.size.y;
        Debug.Log("x, y" + _incrementX + " , " + _incrementY);
        for(int x=0;x<sizeX;x++)
        {
            for(int y=0; y<sizeY;y++)
            {
                
                if(y == 0 || x == 0 || y==sizeY-1 || x==sizeX-1)
                {
                    _possiblePositionsWall.Add((new Vector2(x * _incrementX+transform.position.x, y * _incrementY+transform.position.y)));
                }
                else
                {
                    _possiblePositionsTile.Add(new Vector2(x * _incrementX + transform.position.x, y * _incrementY + transform.position.y));
                }
            }
        }

        for(int i = 0; i<numberOfExits;i++)
        {
            Vector2 vec = _possiblePositionsWall[Random.Range(0, _possiblePositionsWall.Count - 1)];
            _possiblePositionsWall.Remove(vec);
            _exits.Add(vec);
        }

        foreach(Vector2 vec in _exits)
        {
            if (vec.x == transform.position.x)
                exitTile.GetComponent<ExitProperties>().direction = ExitProperties.Dir.LEFT;
            if(vec.x == (sizeX-1) * _incrementX + transform.position.x)
                exitTile.GetComponent<ExitProperties>().direction = ExitProperties.Dir.RIGHT;
            if (vec.y == transform.position.y)
                exitTile.GetComponent<ExitProperties>().direction = ExitProperties.Dir.DOWN;
            if (vec.y == (sizeY - 1) * _incrementY + transform.position.y)
                exitTile.GetComponent<ExitProperties>().direction = ExitProperties.Dir.UP;

            Instantiate(exitTile, vec, Quaternion.identity);
        }

        foreach(Vector2 vec in _possiblePositionsWall)
        {
            if (!vec.Equals(_entry))
                Instantiate(walltile,vec,Quaternion.identity);
        }
        foreach(Vector2 vec in _possiblePositionsTile)
        {
            Instantiate(floortile, vec, Quaternion.identity);
            Instantiate(fogTile, new Vector3(vec.x, vec.y, -2), Quaternion.identity);
        }
        if (!_entry.Equals(new Vector2(0, 0)))
            Instantiate(entryTile, new Vector3(_entry.x, _entry.y, -2), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
