using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectedTileIndicator : Singleton<SelectedTileIndicator>
{
    [SerializeField] private float speed;
    [SerializeField] private int size;
    private List<TileObject> tileObjectsSelected = null;
    private SpriteRenderer spriteRenderer; 
    private Vector3 destinationPos;
    private Vector3 tinyOffset = new(.05f, 0, .05f);

    public override void Awake() {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileObjectsSelected = new List<TileObject>();
        SetSize(size);
    }

    private void SetSize(int size) { 
        this.size = size;
        spriteRenderer.size = new(size, size);
    }

    private void Update() {
        HandleSelection();
        if(tileObjectsSelected.Count == 0) return;
        if(tileObjectsSelected[0] != null) {
            destinationPos = tileObjectsSelected[0].transform.position;
        }

        transform.position = 
            Vector3.Lerp(
                transform.position,
                destinationPos + new Vector3((size-1)*.5f, 0f, (size-1) * .5f) + tinyOffset,
                speed * Time.deltaTime
            );
    
        transform.position = new(transform.position.x, .13f, transform.position.z);
    }

    private void HandleSelection() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit);
        if(!hitSomething) return;

        if(!hit.transform.TryGetComponent(out TileObject tileObject)) return;

        tileObjectsSelected.Clear();

        for(int y = 0; y < size; y++) {
            for(int x = 0; x < size; x++) {
                tileObjectsSelected.Add(TileManager.Instance.GetTile(
                    tileObject.GetLocalPosition().x+x,
                    tileObject.GetLocalPosition().y+y
                ));
            }
        }
    }

    public List<TileObject> GetSelectedTiles(){
        return tileObjectsSelected;
    }
}