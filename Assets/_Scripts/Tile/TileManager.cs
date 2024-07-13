using UnityEngine;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private int tileSize = 1;

    [SerializeField] private Grass grassPrefab;
    private TileObject[,] tileObjects;

    [SerializeField] private GameObject fence;


    public override void Awake() {
        base.Awake();
        tileObjects = new TileObject[sizeY+1, sizeX+1];

        PopulateTileObjectArray();
        CreateFenceSurrounding();
    }

    public void ReplaceTile(int x, int y, TileObject newTileObj) {
        Destroy(tileObjects[y,x].gameObject);
        newTileObj.SetCoordinates(x, y);
        tileObjects[y, x] = newTileObj;
    }

    private void PopulateTileObjectArray() {
        for(int y = 1; y <= sizeY; y++){
            for(int x = 1; x <= sizeX; x++) {
                TileObject tileObject = Instantiate(grassPrefab, new Vector3(x*tileSize, 0, y*tileSize), Quaternion.identity, transform);
                tileObject.transform.localScale = new Vector3(tileSize, .1f, tileSize);
                tileObject.SetCoordinates(x, y);
                tileObjects[y, x] = tileObject;
            }
        }
    }

    private void CreateFenceSurrounding() {
        for(int x = 1; x <= sizeX; x++) {
            Grass grass0 = Instantiate(grassPrefab, new Vector3(x, 0f, 0), Quaternion.identity, transform);
            grass0.SetCoordinates(x, 0);

            Grass grass1 = Instantiate(grassPrefab, new Vector3(x, 0f, GetSizeY()+1), Quaternion.identity, transform);
            grass0.SetCoordinates(x, GetSizeY()+1);

            grass0.PlaceStructure(fence);
            grass1.PlaceStructure(fence);
        }

        
        for(int y = 1; y <= sizeY; y++) {
            Grass grass0 = Instantiate(grassPrefab, new Vector3(0, 0f, y), Quaternion.identity, transform);
            grass0.SetCoordinates(0, y);

            Grass grass1 = Instantiate(grassPrefab, new Vector3(GetSizeX()+1, 0f, y), Quaternion.identity, transform);
            grass0.SetCoordinates(GetSizeX()+1, y);

            grass0.PlaceStructure(fence, null, Quaternion.Euler(0, 90, 0));
            grass1.PlaceStructure(fence, null, Quaternion.Euler(0, 90, 0));
        }
    }



    public TileObject GetTile(int x, int y)  {
        if(x < 0 || x >= sizeX+1 || y < 0 || y >= sizeY+1) return null;
        return tileObjects[y, x];
    }
    public int GetSizeX() => sizeX;
    public int GetSizeY() => sizeY;
}
