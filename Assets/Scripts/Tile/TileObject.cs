using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TileObject : MonoBehaviour
{
    protected Vector2Int localCoordinates; 

    public void SetCoordinates(int x, int y) {
        localCoordinates = new(x, y);
    }

    public void ReplaceTile(TileObject newTilePrefab) {
        TileObject tile = MonoBehaviour.Instantiate(newTilePrefab, transform.position, Quaternion.identity, transform.parent);
        TileManager.Instance.ReplaceTile(localCoordinates.x, localCoordinates.y, tile);
    }

    public Vector2Int GetLocalPosition() => localCoordinates;
}
