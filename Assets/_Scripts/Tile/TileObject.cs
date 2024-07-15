using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TileObject : MonoBehaviour
{
    private static bool debugEnabled = false;
    [SerializeField] protected Vector2Int localCoordinates;
    [SerializeField] private TextMeshPro coordText;

    public void SetCoordinates(int x, int y) {
        localCoordinates = new(x, y);
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
        TileManager.Instance.ReplaceTile(localCoordinates.x, localCoordinates.y, tile);
    }

    public void DebugMark() {
        //TODO: MARK RED
    }

    public Vector2Int GetLocalPosition() => localCoordinates;
}
