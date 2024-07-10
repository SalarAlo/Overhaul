using System.Collections.Generic;
using UnityEngine;

public abstract class ReplaceTileItemUsable : ItemModeUsable
{
    public override void ModeEnabled() {
        if(!Input.GetMouseButtonDown(0)) return;
        Debug.Log("Click");

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