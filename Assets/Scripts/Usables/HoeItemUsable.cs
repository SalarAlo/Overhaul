using System;
using Unity.VisualScripting;
using UnityEngine;

public class HoeItemUsable : ItemModeUsable {
    
    public override void ModeEnabled() {
        if(!Input.GetMouseButtonDown(0)) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(!Physics.Raycast(ray, out RaycastHit hit)) return;

        if(!hit.transform.TryGetComponent(out TileObject tileObject)) return;
        if(tileObject is not Grass) return;
        tileObject.ReplaceTile(TileManager.Instance.GetSoilPrefab());
    }

    protected override void DefineOnClickUsable(){

    }
}
