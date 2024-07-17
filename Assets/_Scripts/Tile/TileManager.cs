using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Timeline;

public class TileManager : Singleton<TileManager>
{
    // TODO: Better declaration for world pos var x and y and local pos var x and y
    [SerializeField] private Plot plotPrefab;
    [SerializeField] private Grass grassPrefab;
    [SerializeField] private GameObject fence;

    private int plotsX = 5;
    private int plotsY = 5;

    private List<Plot> unlockedPlots;
    private List<Plot> allPlots;

    public override void Awake() {
        base.Awake();

        unlockedPlots = new List<Plot>();
        allPlots = new List<Plot>();

        CreatePlots();
        OccupyMiddlePlot();
        CreateFenceSurrounding();
    }

    private void CreatePlots(){
        for(int y = 0; y < plotsY; y++){
            for(int x = 0; x < plotsX; x++) {
                allPlots.Add(
                    Instantiate(plotPrefab, new(x*Plot.GetSinglePlotSize(), 0, y*Plot.GetSinglePlotSize()), Quaternion.identity, transform)
                );
                allPlots[^1].Initialize(x, y);
            }
        }
    }

    public void ReplaceTile(int x, int y, TileObject newTileObj) {
        int localX = x % Plot.GetSinglePlotSize();
        int localY = y % Plot.GetSinglePlotSize();

        Plot plot = allPlots.Find(plot => plot.ContainsTile(x, y));

        plot.ReplaceTile(localX, localY, newTileObj);
    }

    public TileObject CreateTileObject(int x, int y, Plot plot) {
        TileObject tileObject = Instantiate(grassPrefab, plot.transform);
        tileObject.transform.localScale = new Vector3(1, .1f, 1);
        tileObject.Initialize(x, y, plot);
        return tileObject;
    }

    private void OccupyMiddlePlot() {
        unlockedPlots.Add(
            allPlots.Find((p) => p.GetLocalCoordinates().x == plotsX/2 && p.GetLocalCoordinates().y == plotsY/2)
        );
    }

    private void CreateFenceSurrounding() {
        foreach (var plot in unlockedPlots) {
            foreach(TileObject tile in plot.GetAllTiles()){
                Vector2Int tileCoordinates = tile.GetCoordinates();
                List<Vector2Int> neighbourCoordinates = new() {
                    tileCoordinates+Vector2Int.up,
                    tileCoordinates+Vector2Int.down,
                    tileCoordinates+Vector2Int.left,
                    tileCoordinates+Vector2Int.right
                };

                List<Vector2Int> positionsFenceRequired = new();

                foreach(Vector2Int neighbourCoordinate in neighbourCoordinates) 
                    if(GetTileInUnlockedPlots(neighbourCoordinate.x, neighbourCoordinate.y) == null)
                        positionsFenceRequired.Add(neighbourCoordinate);
                
                if(positionsFenceRequired.Count == 0) continue;

                foreach(Vector2Int positionFenceRequired in positionsFenceRequired) {
                    StructureTileObject tileFenceRequired = GetTile(positionFenceRequired.x, positionFenceRequired.y) as StructureTileObject;
                    tileFenceRequired.PlaceStructure(
                        fence, 
                        false, 
                        null, 
                        positionFenceRequired.x == tileCoordinates.x ? null : Quaternion.Euler(0, 90, 0)
                    );
                }
            }
        }
    }

    public TileObject GetTileInUnlockedPlots(int x, int y)  {
        var plot =  unlockedPlots.Find(
            plot => plot.ContainsTile(x, y)
        );

        if(plot == null) return null;

        int localX = x % Plot.GetSinglePlotSize();
        int localY = y % Plot.GetSinglePlotSize();
        return plot.GetTile(localX, localY);
    }

    public TileObject GetTile(int x, int y) {
        int localX = x % Plot.GetSinglePlotSize();
        int localY = y % Plot.GetSinglePlotSize();
        return allPlots.Find(p => p.ContainsTile(x, y)).GetTile(localX, localY);
    }
}