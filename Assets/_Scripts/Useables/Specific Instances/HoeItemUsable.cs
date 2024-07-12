using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HoeItemUsable : TimedItemUsable
{
    List<TileObject> currentTileObjs = new List<TileObject>();
    protected override bool CheckAdditionalCondition()
    {
        List<TileObject> tileObjects = SelectedTileIndicator.Instance.GetSelectedTiles();

        if(!Enumerable.SequenceEqual(tileObjects, currentTileObjs)) {
            currentTileObjs = tileObjects;
            return false;
        }

        return tileObjects.Any(tile => tile is Grass); 
    }

    protected override void DefineOnModeEnabled(UsableInventoryItemSO so) {
    }

    protected override float GetActionDuration() {
        List<TileObject> tileObjects = SelectedTileIndicator.Instance.GetSelectedTiles();
        return tileObjects.Aggregate(0f, (accu, item) => accu + .3f);
    }

    protected override void OnTimerFinished() {
        foreach(var tile in currentTileObjs) {
            if(tile is not Grass) continue;
            tile.ReplaceTile(TileManager.Instance.GetSoilPrefab());
        }
    }
}
