using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/Create SpriteMapper")]
public class SpriteMapper : ScriptableObject
{
    public List<Sprite> map;
    public Sprite placeholder;

    public Sprite GetSpriteById(int id)
    {
        try
        {
            return map[id];
        }
        catch (IndexOutOfRangeException e)
        {
            Debug.LogError(id + " sprite not available.");
        }
        return placeholder;

    }
    public Sprite GetSpriteByName(string name)
    {
        foreach (Sprite sprite in map)
        {
            if (sprite.name.Equals(name))
            {
                return sprite;
            }
        }
        return placeholder;
    }

    public int GetSpriteIdByName(string name)
    {
        for (int i = 0; i < map.Count; i++)
        {
            if (map[i].name.Equals(name))
            {
                return i;
            }
        }
        return -1;
    }
}