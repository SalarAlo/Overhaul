using UnityEngine;

public class StoneItemUsable : ReplaceTileItemUsable
{
    protected override bool CanReplaceCondition(TileObject tileObjectToReplace){
        return tileObjectToReplace is Grass;
    }

    protected override void DefineOnModeEnabled(){
    }

    protected override TileObject GetOutcomeTile(){
        return TileManager.Instance.GetStoneTile();
    }
}
