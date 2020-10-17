using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Properties:MonoBehaviour
{
    public static List<Sprite> avatarsStatic = new List<Sprite>();
    public List<Sprite> avatars = new List<Sprite>();
    public Sprite deadSprite;
    public static Sprite deadStaticSprite;
    void Awake()
    {
        deadStaticSprite = deadSprite;
        foreach(Sprite sprite in avatars)
        {
            avatarsStatic.Add(sprite);
        }    
    }
    public static Sprite getRandomAvatar()
    {
        if (avatarsStatic.Count == 0)
            return null;
        return avatarsStatic[Random.Range(0, avatarsStatic.Count - 1)];
    }
    
}
