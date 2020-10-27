using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetProvider : MonoBehaviour
{
    public SpriteMapper SpriteStoreProvider;
    public static SpriteMapper SpriteStore;

    private void Awake()
    {
        SpriteStore = SpriteStoreProvider;
    }
}
