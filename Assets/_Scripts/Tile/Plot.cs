using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plot : MonoBehaviour
{
    private Vector2Int localCoords;
    private TileObject[,] tileObjects = new TileObject[10, 10];
    public static int GetSinglePlotSize() {
        return 10;
    }

    public void Initialize(int localX, int loxalY) {
        localCoords = new(localX, loxalY);
        for(int y = 0; y < GetSinglePlotSize(); y++) {
            for(int x = 0; x < GetSinglePlotSize(); x++) {
                tileObjects[x, y] = TileManager.Instance.CreateTileObject(x + localX*GetSinglePlotSize(), y + loxalY*GetSinglePlotSize(), transform);
                tileObjects[x, y].transform.localPosition = new(x, 0, y);
            }
        }
    }
    public TileObject GetTile(int x, int y) => tileObjects[x, y];
    public Vector2Int GetLocalCoordinates() => localCoords;

    public void SetTileObjectsRed()
    {
        foreach(TileObject tileObject in tileObjects) {
            tileObject.DebugMark(); 
        }
    }

    public bool ContainsTile(int x, int y) {
        int localX = x  % GetSinglePlotSize();
        int localY = y % GetSinglePlotSize();
        return tileObjects[localX, localY].GetCoordinates() == new Vector2Int(x, y);
    }

    public void ReplaceTile(int x, int y, TileObject newTileObj) {
        Destroy(tileObjects[x, y].gameObject);
        newTileObj.SetCoordinates(x, y);
        tileObjects[x, y] = newTileObj;
    }
}
