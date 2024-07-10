using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public abstract class TileObject : MonoBehaviour
{
    protected Vector2Int localCoordinates; 

    public void Initialize(int x, int y) {
        localCoordinates = new (x, y);
    }

    public void ReplaceTile(TileObject newTile) {
        TileManager.Instance.ReplaceTile(localCoordinates.x, localCoordinates.y, newTile);
    }
}
