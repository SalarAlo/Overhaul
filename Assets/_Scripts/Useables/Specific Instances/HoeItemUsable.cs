using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HoeItemUsable : TimedItemUsable
{
    List<TileObject> currentTileObjs = new List<TileObject>();
    protected override bool ShouldCancelCounter()
    {
        List<TileObject> tileObjects = new List<TileObject> (SelectedTileIndicator.Instance.GetSelectedTiles());

        if(!currentTileObjs.SequenceEqual(tileObjects)) {
            currentTileObjs = tileObjects;
            return true;
        }

        return !tileObjects.Any(tile => tile is Grass); 
    }

    protected override void DefineOnModeEnabled(UsableInventoryItemSO so) {
    }

    protected override float GetActionDuration() {
        List<TileObject> tileObjects = SelectedTileIndicator.Instance.GetSelectedTiles();
        return tileObjects.Aggregate(0f, (accu, item) => { return accu + (item == null || item is not Grass ? 0 : .3f); });
    }

    protected override Vector3 GetProgressBarPos() {
        return currentTileObjs[0].transform.position + new Vector3(0, 1, 0);
    }

    protected override void OnTimerFinished() {
        foreach(var tile in currentTileObjs) {
            if(tile is not Grass) continue;
            tile.ReplaceTile(TileManager.Instance.GetSoilPrefab());
        }
    }
}
