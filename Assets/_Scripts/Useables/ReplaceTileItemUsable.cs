using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public abstract class ReplaceTileItemUsable : ItemModeUsable
{
    [SerializeField] private TileObject outcomeTile;
    [SerializeField] private List<TypeReference> validGroundTiles;
    public override void DefineDuringModeEnabled() {
        if(!Input.GetMouseButton(0)) return;

        List<TileObject> selectedTiles = SelectedTileIndicator.Instance.GetSelectedTiles();

        foreach(TileObject tile in selectedTiles) { 
            if (validGroundTiles.Any(typeRef => typeRef.IsOfSameType(tile.gameObject))) continue;
            tile.ReplaceTile(outcomeTile);
        }
    }

    protected override abstract void DefineOnModeEnabled(UsableInventoryItemSO so);
}