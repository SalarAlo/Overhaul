using System.Collections.Generic;
using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private int maxSizeX;
    [SerializeField] private int maxSizeY;
    [SerializeField] private int tileSize = 1;

    [SerializeField] private Grass grassPrefab;
    private Dictionary<Vector2Int, TileObject> unlockedTileObjects;
    private Dictionary<Vector2Int, TileObject> allTileObjects;

    [SerializeField] private GameObject fence;


    public override void Awake() {
        base.Awake();
        unlockedTileObjects = new Dictionary<Vector2Int, TileObject>();
        allTileObjects = new Dictionary<Vector2Int, TileObject>();

        CreateAllTileObjects();
        PopulateUnlockedTileObjects();
        CreateFenceSurrounding();
    }

    public void ReplaceTile(int x, int y, TileObject newTileObj) {
        Destroy(unlockedTileObjects[new (y, x)].gameObject);
        newTileObj.SetCoordinates(x, y);
        unlockedTileObjects[new (y, x)] = newTileObj;
    }

    private void CreateAllTileObjects() {
        for(int y = 0; y <= maxSizeX; y++){
            for(int x = 0; x <= maxSizeY; x++) {
                TileObject tileObject = Instantiate(grassPrefab, new Vector3(x*tileSize, 0, y*tileSize), Quaternion.identity, transform);
                tileObject.transform.localScale = new Vector3(tileSize, .1f, tileSize);
                tileObject.SetCoordinates(x, y);
                allTileObjects[tileObject.GetLocalPosition()] = tileObject;
            }
        }
    }

    private void PopulateUnlockedTileObjects() {
        int yOff = Mathf.FloorToInt((maxSizeY - sizeY) / 2);
        int xOff = Mathf.FloorToInt((maxSizeX - sizeX) / 2);


        for(int y = yOff; y <= sizeY - yOff; y++) {
            for(int x = xOff; x <= sizeX - xOff; x++) {
                unlockedTileObjects[new(y, x)] = allTileObjects[new(y, x)];
                unlockedTileObjects[new(y, x)].DebugMark();
            }
        }
    }

    private void CreateFenceSurrounding() {
        foreach (var keyValuePair in unlockedTileObjects) {
            Vector2Int coord = keyValuePair.Key;

        }
    }



    public TileObject GetTile(int x, int y)  {
        int yOff = Mathf.FloorToInt((maxSizeY - sizeY) / 2);
        int xOff = Mathf.FloorToInt((maxSizeX - sizeX) / 2);
        if (x < xOff || x >= sizeX+1 || y < yOff || y >= sizeY+1) return null;
        return unlockedTileObjects[new(y, x)];
    }
    public int GetSizeX() => sizeX;
    public int GetSizeY() => sizeY;
}
