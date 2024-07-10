using System;
using UnityEngine;

public class HoeItemUsable : ItemModeUsable
{
    public override void ModeEnabled(){
        if(!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit)) return;

        if(!hit.transform.TryGetComponent(out TileObject tileObject)) return;

        tileObject.SetTile(TileObject.TileType.Soil);
    }

    protected override void DefineOnClickUsable(){
        Debug.Log("Know we're hoing");
    }
}
