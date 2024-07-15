using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class HoeItemUsable : TimedItemUsable
{
    List<TileObject> currentTileObjs = new List<TileObject>();
    [SerializeField] private Soil soilPrefab;
    protected override bool ShouldCancelCounter()
    {
        List<TileObject> tileObjects = new List<TileObject> (SelectedTileIndicator.Instance.GetSelectedTiles());

        if(!currentTileObjs.SequenceEqual(tileObjects)) {
            currentTileObjs = tileObjects;
            return true;
        }


        return !tileObjects.Any(tile => tile is Grass grassTile && !grassTile.HasBlockStructurePlaced()); 
    }

    protected override void OnModeEnabled(UsableInventoryItemSO so) {
    }

    protected override float GetActionDuration() {
        List<TileObject> tileObjects = SelectedTileIndicator.Instance.GetSelectedTiles();
        return tileObjects.Aggregate(0f, (accu, item) => { return accu + (item is not Grass ? 0 : .3f); });
    }

    protected override Vector3 GetProgressBarPos() {
        return currentTileObjs.Aggregate(
            Vector3.zero, 
            (pos, tile) => tile == null ? pos : pos+tile.transform.position 
        ) / currentTileObjs.Count + new Vector3(0, 1, 0);
    }

    protected override void OnTimerFinished() {
        foreach(var tile in currentTileObjs) {
            if(tile is not Grass) continue;
            tile.ReplaceTile(soilPrefab);
        }
    }
}
