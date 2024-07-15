using System.Collections.Generic;
using System.Linq;
using TreeEditor;
using UnityEngine;

public class SeedBagItemUsable : TimedItemUsable
{
    [SerializeField] private float plantTimeForeachSoil;
    [SerializeField] private SeedBagItemSO seedSO;
    private List<TileObject> selectedTiles = new List<TileObject>();

    protected override void OnModeEnabled(UsableInventoryItemSO so){
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

    protected override void OnTimerFinished() {
        foreach(TileObject tile in selectedTiles) {
            if(tile is not Soil soilTile) continue;
            if(soilTile.IsOccupied()) continue;

            soilTile.PlantSeed(seedSO);
            InventorySystem.Instance.RemoveItem(seedSO);
        }
    }

    protected override bool ShouldCancelCounter()
    {

        List<TileObject> tileObjects = new List<TileObject> (SelectedTileIndicator.Instance.GetSelectedTiles());

        if(!selectedTiles.SequenceEqual(tileObjects)) {
            selectedTiles = tileObjects;
            return true;
        }

        int amountOfSoilToPlant = tileObjects.Aggregate(0, (accu, t) => t is Soil soil && !soil.IsOccupied() ? accu+1 : accu);
        bool notEnoughSeeds = amountOfSoilToPlant > InventorySystem.Instance.GetAmount(seedSO); 

        if (notEnoughSeeds) {
            return true;
        }
        bool anySoilWhichCanBePlanted = !tileObjects.Any(tile => tile is Soil soilTile && !soilTile.IsOccupied()); 

        return anySoilWhichCanBePlanted; 
    }
}
