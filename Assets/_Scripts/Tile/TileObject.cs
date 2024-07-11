using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TileObject : MonoBehaviour
{
    [SerializeField] protected Vector2Int localCoordinates; 
    [SerializeField] private Transform visuals;

    public void SetCoordinates(int x, int y) {
        localCoordinates = new(x, y);
    }

    public void ReplaceTile(TileObject newTilePrefab, Vector3? offsetNullable = null) {
        if(offsetNullable == null) offsetNullable = Vector3.zero;
        Vector3 offset = (Vector3)offsetNullable;

        TileObject tile = MonoBehaviour.Instantiate(newTilePrefab, transform.position + offset, Quaternion.identity, transform.parent);
        TileManager.Instance.ReplaceTile(localCoordinates.x, localCoordinates.y, tile);
    }

    public Vector2Int GetLocalPosition() => localCoordinates;
}
