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
    [SerializeField] private TileObject stonePrefab;


    public override void Awake() {
        base.Awake();
        tileObjects = new TileObject[sizeY, sizeX];

        PopulateTileObjectArray();
    }

    public void ReplaceTile(int x, int y, TileObject newTileObj) {
        Destroy(tileObjects[y,x].gameObject);
        newTileObj.SetCoordinates(x, y);
        tileObjects[y, x] = newTileObj;
    }

    private void PopulateTileObjectArray() {
        for(int y = 0; y < sizeY; y++){
            for(int x = 0; x < sizeX; x++) {
                TileObject tileObject = Instantiate(tileObjectPrefab, new Vector3(x*tileSize, 0, y*tileSize), Quaternion.identity, transform);
                tileObject.transform.localScale = new Vector3(tileSize, .1f, tileSize);
                tileObject.SetCoordinates(x, y);
                tileObjects[y, x] = tileObject;
            }
        }
    }



    public TileObject GetTile(int x, int y)  {
        if(x < 0 || x >= sizeX || y < 0 || y >= sizeY) return null;
        return tileObjects[y, x];
    }
    public int GetSizeX() => sizeX;
    public int GetSizeY() => sizeY;
    public Soil GetSoilPrefab() => soilPrefab;
    public Grass GetGrassPrefab() => grassPrefab;

    internal TileObject GetStoneTile() => stonePrefab;
}
