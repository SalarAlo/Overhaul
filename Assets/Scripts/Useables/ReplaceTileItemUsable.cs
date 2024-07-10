using System.Collections.Generic;
using UnityEngine;

public abstract class ReplaceTileItemUsable : ItemModeUsable
{
    public override void DefineModeEnabled() {
        if(!Input.GetMouseButtonDown(0)) return;

        List<TileObject> selectedTiles = SelectedTileIndicator.Instance.GetSelectedTiles();

        foreach(TileObject tile in selectedTiles) {
            if(CanReplaceCondition(tile)) 
                tile.ReplaceTile(GetOutcomeTile());
        }
    }

    protected override abstract void DefineOnClickUsable();
    protected abstract bool CanReplaceCondition(TileObject tileObjectToReplace);
    protected abstract TileObject GetOutcomeTile();
}