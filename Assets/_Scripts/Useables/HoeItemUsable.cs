using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HoeItemUsable : ReplaceTileItemUsable
{
    protected override bool CanReplaceCondition(TileObject tileObjectToReplace){
        return tileObjectToReplace is Grass;
    }

    protected override void DefineOnClickUsable() {
        
    }

    protected override TileObject GetOutcomeTile() {
        return TileManager.Instance.GetSoilPrefab();
    }

    
}
