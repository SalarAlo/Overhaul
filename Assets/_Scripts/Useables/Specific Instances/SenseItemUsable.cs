using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SenseItemUsable : ItemModeUsable
{
    public override void DuringModeEnabled(){
        if(!Input.GetMouseButton(0)) return;

        bool IsHarvestableSoilTile(TileObject tile) {
            return tile is Soil soil && soil.CanHarvest();
        }

        List<TileObject> selectedTiles = SelectedTileIndicator.Instance.GetSelectedTiles();
        List<Soil> harvestableSoilTiles = selectedTiles.
            Where(IsHarvestableSoilTile).
            ToList().
            Select(tile => tile as Soil).
            ToList();

        foreach(Soil harvestableSoilTile in harvestableSoilTiles) {
            harvestableSoilTile.Harvest();
        }
    }

    protected override void OnModeEnabled(UsableInventoryItemSO so){

    }
}
