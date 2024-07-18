using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.Timeline;

public class TileManager : Singleton<TileManager>
{
    // TODO: Seperation of concerns between tileManaging and plotManaging
    [SerializeField] private Plot plotPrefab;
    [SerializeField] private Grass grassPrefab;
    [SerializeField] private GameObject fence;

    private int plotsX = 5;
    private int plotsY = 5;

    private List<Plot> unlockedPlots;
    private List<Plot> allPlots;
    private List<StructureTileObject> currentTilesWithFences = new List<StructureTileObject>();

    public override void Awake() {
        base.Awake();

        unlockedPlots = new List<Plot>();
        allPlots = new List<Plot>();

        CreatePlots();
        OccupyMiddlePlot();
        CreateFenceSurrounding();
    }

    public void UnlockPlot(int x, int y) {
        Plot plotToAdd = allPlots.Find(p => p.GetLocalCoordinates().x == x && p.GetLocalCoordinates().y == y);
        if(!unlockedPlots.Contains(plotToAdd)){
            unlockedPlots.Add(plotToAdd);
            plotToAdd.SetUnlocked();
        }
        CreateFenceSurrounding();
    }
    public void UnlockPlot(Plot plot) => UnlockPlot(plot.GetLocalCoordinates().x, plot.GetLocalCoordinates().y);

    private void CreatePlots(){
        for(int y = 0; y < plotsY; y++){
            for(int x = 0; x < plotsX; x++) {
                allPlots.Add(
                    Instantiate(plotPrefab, new(x*Plot.GetSinglePlotSize(), 0, y*Plot.GetSinglePlotSize()), Quaternion.identity, transform)
                );
                // -1
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

    public List<Plot> GetAllPlots() => allPlots;

    private void OccupyMiddlePlot() {
        unlockedPlots.Add(
            allPlots.Find((p) => p.GetLocalCoordinates().x == plotsX/2 && p.GetLocalCoordinates().y == plotsY/2)
        );
        unlockedPlots[0].SetUnlocked();
    }

    private void CreateFenceSurrounding() {
        if(currentTilesWithFences.Count != 0) {
            currentTilesWithFences.ForEach(t => t.RemovePlacedStructure());
        }
        foreach (var plot in unlockedPlots) {
            foreach(TileObject tile in plot.GetAllTiles()) {
                Vector2Int tileCoordinates = tile.GetCoordinates();
                List<Vector2Int> neighbourCoordinates = tile.GetNeighboorPositions();

                if(tile.AllNeighboursInPlot()) continue;

                List<Vector2Int> positionsFenceRequired = tile.GetAllNeighbourPositionsNotInPlot();

                foreach(Vector2Int positionFenceRequired in positionsFenceRequired) {
                    Vector2Int positionFenceRequiredAdjusted = positionFenceRequired;
                    bool rotationNeeded = positionFenceRequired.x != tileCoordinates.x; 

                    StructureTileObject tileFenceRequired = 
                        GetTile(positionFenceRequired.x, positionFenceRequired.y) as StructureTileObject;

                    tileFenceRequired.PlaceStructure(
                        fence, 
                        false, 
                        null, 
                        !rotationNeeded ? null : Quaternion.Euler(0, 90, 0)
                    );
                    currentTilesWithFences.Add(tileFenceRequired);
                }
            }
        }
    }

    public TileObject GetTileInUnlockedPlots(int x, int y)  {
        int localX = x % Plot.GetSinglePlotSize();
        int localY = y % Plot.GetSinglePlotSize();

        var plot = unlockedPlots.Find(
            plot => plot.ContainsTile(x, y)
        );

        if(plot == null) return null;

        return plot.GetTile(localX, localY);
    }

    public TileObject GetTile(int x, int y) {
        int localX = x % Plot.GetSinglePlotSize();
        int localY = y % Plot.GetSinglePlotSize();

        Plot plot = allPlots.Find(p => p.ContainsTile(x, y)); 

        if(plot != null)
            return plot.GetTile(localX, localY);
        else 
            return null;
    }

    public Plot GetPlot(int x, int y) {
        return allPlots.Find(p => p.GetLocalCoordinates().x == x && p.GetLocalCoordinates().y == y);
    }
}