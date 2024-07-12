using System.Collections.Generic;
using UnityEngine;

public class SeedBagItemUsable : ItemModeUsable
{
    [SerializeField] SeedBagItemSO seedSO;
    public override void DefineDuringModeEnabled(){
        if(!Input.GetMouseButton(0)) return;

        List<TileObject> selectedTiles = SelectedTileIndicator.Instance.GetSelectedTiles();

        foreach(TileObject tile in selectedTiles) {
            if(tile == null) return;
            if(tile is not Soil soilTile) return;
            if(soilTile.IsOccupied()) return;

            soilTile.PlantSeed(seedSO);
        }
    }

    protected override void DefineOnModeEnabled(UsableInventoryItemSO so){
        seedSO = so as SeedBagItemSO;
    }
}
