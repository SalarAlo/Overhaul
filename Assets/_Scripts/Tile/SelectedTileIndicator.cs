using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Rendering;
using UnityEngine;

public class SelectedTileIndicator : Singleton<SelectedTileIndicator>
{
    [SerializeField] private float speed;
    [SerializeField] private int sizeIndicator;
    private List<TileObject> tileObjectsSelected = null;
    private SpriteRenderer spriteRenderer; 
    private Vector3 destinationPos;

    public override void Awake() {
        base.Awake();
        spriteRenderer = GetComponent<SpriteRenderer>();
        tileObjectsSelected = new List<TileObject>();
        SetSize(sizeIndicator);
    }

    private void SetSize(int size) { 
        this.sizeIndicator = size;
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
                destinationPos + new Vector3(
                    (sizeIndicator-1)*.5f, 
                    .25f,
                    (sizeIndicator-1) * .5f
                ),
                speed * Time.deltaTime
            );
    
        transform.position = new(transform.position.x, .13f, transform.position.z);
    }

    private void HandleSelection() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit);
        if(!hitSomething) return;

        if(!hit.transform.TryGetComponent(out TileObject tileObject)) return;
        Vector2Int coord = tileObject.GetLocalPosition();

        tileObjectsSelected.Clear();

        for(int y = 0; y < sizeIndicator; y++) {
            for(int x = 0; x < sizeIndicator; x++) {
                tileObjectsSelected.Add(TileManager.Instance.GetTile(
                    tileObject.GetLocalPosition().x+x,
                    tileObject.GetLocalPosition().y+y
                ));
            }
        }
    }

    public List<TileObject> GetSelectedTiles(){
        return tileObjectsSelected.Where(tile => tile != null).ToList();
    }
}