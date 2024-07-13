using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeedBagItemUsable : TimedItemUsable
{
    [SerializeField] private float plantTimeForeachSoil;
    [SerializeField] private SeedBagItemSO seedSO;
    private List<TileObject> selectedTiles = new List<TileObject>();

    protected override void DefineOnModeEnabled(UsableInventoryItemSO so){
        seedSO = so as SeedBagItemSO;
    }

    protected override float GetActionDuration(){
        return selectedTiles.Aggregate(0f, (seconds, tile) => (tile is Soil soil && !soil.IsOccupied()) ? seconds+plantTimeForeachSoil : seconds);
    }

    protected override Vector3 GetProgressBarPos()
    {
        return selectedTiles.Aggregate(
            Vector3.zero, 
            (pos, tile) => tile == null ? pos : pos+tile.transform.position 
        ) / selectedTiles.Count + new Vector3(0, 1, 0);
    }

    protected override void OnTimerFinished()
    {
        foreach(TileObject tile in selectedTiles) {
            if(tile is not Soil soilTile) continue;
            if(soilTile.IsOccupied()) continue;

            soilTile.PlantSeed(seedSO);
        }
    }

    protected override bool ShouldCancelCounter()
    {

        List<TileObject> tileObjects = new List<TileObject> (SelectedTileIndicator.Instance.GetSelectedTiles());

        if(!selectedTiles.SequenceEqual(tileObjects)) {
            selectedTiles = tileObjects;
            return true;
        }

        return !tileObjects.Any(tile => tile is Soil soilTile && !soilTile.IsOccupied()); 
    }
}
