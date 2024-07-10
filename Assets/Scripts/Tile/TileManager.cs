using System;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private int tileSize = 1;

    [SerializeField] private TileObject tileObjectPrefab;
    private TileObject[,] tileObjects;

    [SerializeField] private Soil soilPrefab;
    [SerializeField] private Grass grassPrefab;


    public override void Awake() {
        base.Awake();
        tileObjects = new TileObject[sizeY, sizeX];

        PopulateTileObjectArray();
    }

    public void ReplaceTile(int x, int y, TileObject newTileObj) {
        Destroy(tileObjects[x,y].gameObject);
        tileObjects[x, y] = newTileObj;
    }

    private void PopulateTileObjectArray() {
        for(int y = 0; y < sizeY; y++){
            for(int x = 0; x < sizeX; x++) {
                TileObject tileObject = Instantiate(tileObjectPrefab, new Vector3(x*tileSize, 0, y*tileSize), Quaternion.identity, transform);
                tileObject.transform.localScale = new Vector3(tileSize, .1f, tileSize);
                tileObjects[y, x] = tileObject;
            }
        }
    }

    public Soil GetSoilPrefab() => soilPrefab;
    public Grass GetGrass() => grassPrefab;
}
