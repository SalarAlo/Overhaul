using System.Collections.Generic;
using UnityEngine;

public abstract class ReplaceTileItemUsable : ItemModeUsable
{
    public override void DefineDuringModeEnabled() {
        if(!Input.GetMouseButton(0)) return;

        List<TileObject> selectedTiles = SelectedTileIndicator.Instance.GetSelectedTiles();

        foreach(TileObject tile in selectedTiles) {
            if(CanReplaceCondition(tile)) 
                tile.ReplaceTile(GetOutcomeTile(), GetOffset());
        }
    }

    protected virtual Vector3 GetOffset() => Vector3.zero; 

    protected override abstract void DefineOnModeEnabled();
    protected abstract bool CanReplaceCondition(TileObject tileObjectToReplace);
    protected abstract TileObject GetOutcomeTile();
}