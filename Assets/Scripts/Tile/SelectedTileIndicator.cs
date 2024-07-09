using System;
using UnityEngine;

public class SelectedTileIndicator : MonoBehaviour
{
    public Action<TileObject> OnSelectedTileObjectChanged;
    [SerializeField] private float speed;
    private TileObject tileObjectSelected = null;

    private void Update() {
        HandleSelection();
        if(tileObjectSelected == null) return;
        transform.position = Vector3.Lerp(transform.position, tileObjectSelected.transform.position, speed * Time.deltaTime);
        transform.position = new(transform.position.x, .11f, transform.position.z);
    }

    private void HandleSelection() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        bool hitSomething = Physics.Raycast(ray, out RaycastHit hit);
        if(!hitSomething) return;

        if(!hit.transform.TryGetComponent(out TileObject tileObject)) return;

        if(tileObjectSelected != tileObject) {
            tileObjectSelected = tileObject;
            OnSelectedTileObjectChanged?.Invoke(tileObject);
        }
    }
}