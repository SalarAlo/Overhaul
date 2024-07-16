using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TileObject : MonoBehaviour
{
    private static bool debugEnabled = false;
    [SerializeField] protected Vector2Int coordinates;
    [SerializeField] private TextMeshPro coordText;
    [SerializeField] private Material marked;
    [SerializeField] private MeshRenderer meshRenderer;

    public void SetCoordinates(int x, int y) {
        coordinates = new(x, y);
        if(debugEnabled) {
            coordText.text = $"({x}, {y})";
        } else {
            if(coordText != null)
                coordText.gameObject.SetActive(false);
        }
    }

    public void ReplaceTile(TileObject newTilePrefab, Vector3? offsetNullable = null) {
        if(offsetNullable == null) offsetNullable = Vector3.zero;
        Vector3 offset = (Vector3)offsetNullable;

        TileObject tile = MonoBehaviour.Instantiate(newTilePrefab, transform.position + offset, Quaternion.identity, transform.parent);
        TileManager.Instance.ReplaceTile(coordinates.x, coordinates.y, tile);
    }

    public void DebugMark() {
        //TODO: MARK RED
        meshRenderer.material = marked;
    }

    public Vector2Int GetCoordinates() => coordinates;
}
