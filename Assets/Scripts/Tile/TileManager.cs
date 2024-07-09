using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    [SerializeField] private int sizeX;
    [SerializeField] private int sizeY;
    [SerializeField] private int tileSize = 1;

    [SerializeField] private TileObject tileObjectPrefab;
    private TileObject[,] tileObjects;


    private void Awake() {
        tileObjects = new TileObject[sizeY, sizeX];

        PopulateTileObjectArray();
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
}
