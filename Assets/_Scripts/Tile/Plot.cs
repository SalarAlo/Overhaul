using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Plot : MonoBehaviour
{
    // TODO: implement usable overworld object and mkae buyINdicator that!
    private Vector2Int localCoords;
    [SerializeField] private BuyPlotSign buyIndicatorPrefab;
    private TileObject[,] tileObjects = new TileObject[10, 10];
    public static Action<Plot> OnAnyPlotUnlocked; 
    private bool unlocked;

    private void Awake() {
        OnAnyPlotUnlocked += Plot_OnAnyPlotUnlocked;
    }

    private void Plot_OnAnyPlotUnlocked(Plot plot){
        List<Plot> neighbours = GetNeighboursOfPlot(plot);
        if (neighbours.Contains(this) && !unlocked) {
            var structureTileObject = tileObjects[4, 4] as StructureTileObject;
            var buySign = structureTileObject.PlaceStructure(buyIndicatorPrefab.gameObject, true, null, Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0));
            buySign.GetComponent<BuyPlotSign>().AssignPlot(this);
        } else if (plot == this) {
            var structureTileObject = tileObjects[4, 4] as StructureTileObject;
            structureTileObject.RemovePlacedStructure();
        }
    }

    public static List<Plot> GetNeighboursOfPlot(Plot plot) {
        Vector2Int plotCoordinate = plot.GetLocalCoordinates();
        List<Vector2Int> neighbourCoordinates = new() {
            plotCoordinate+Vector2Int.up,
            plotCoordinate+Vector2Int.down,
            plotCoordinate+Vector2Int.left,
            plotCoordinate+Vector2Int.right
        };
        return neighbourCoordinates.Select(coord => TileManager.Instance.GetPlot(coord.x, coord.y)).ToList();
    }

    public static int GetSinglePlotSize() {
        return 10;
    }

    public void Initialize(int localX, int loxalY) {
        localCoords = new(localX, loxalY);
        unlocked = false;
        for(int y = 0; y < GetSinglePlotSize(); y++) {
            for(int x = 0; x < GetSinglePlotSize(); x++) {
                tileObjects[x, y] = TileManager.Instance.CreateTileObject(x + localX*GetSinglePlotSize(), y + loxalY*GetSinglePlotSize(), this);
                tileObjects[x, y].transform.localPosition = new(x, 0, y);
            }
        }
    }
    public TileObject GetTile(int x, int y)  { 
        int localX = x  % GetSinglePlotSize();
        int localY = y % GetSinglePlotSize();

        return tileObjects[localX, localY]; 
    }
    public Vector2Int GetLocalCoordinates() => localCoords;
    public void SetUnlocked() {
        unlocked = true;
        OnAnyPlotUnlocked?.Invoke(this);
    }

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
        newTileObj.Initialize(x, y, this);
        tileObjects[x, y] = newTileObj;
    }

    public TileObject[,] GetAllTiles(){
        return tileObjects;
    }
}
