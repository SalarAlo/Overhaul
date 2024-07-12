using System.Collections.Generic;
using UnityEngine;

public class SeedBagItemUsable : ItemModeUsable
{
    public override void DefineDuringModeEnabled(){
        if(!Input.GetMouseButton(0)) return;

        List<TileObject> selectedTiles = SelectedTileIndicator.Instance.GetSelectedTiles();

        foreach(TileObject tile in selectedTiles) {
            if(tile == null) return;
            if(tile is not Soil soilTile) return;
            if(soilTile.IsOccupied()) return;

            soilTile.PlantSeed(UseSystem.Instance.GetUsableItemSO() as SeedBagItemSO);
        }
    }

    protected override void DefineOnModeEnabled(){

    }
}
