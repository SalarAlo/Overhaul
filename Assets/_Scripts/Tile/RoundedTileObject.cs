using System.Collections.Generic;
using UnityEngine;

public abstract class RoundedTileObject<T> : TileObject where T : RoundedTileObject<T>
{
    [SerializeField] private GameObject soilRoundedParent;
    [SerializeField] private GameObject noRoundedEdgesPrefab;
    [SerializeField] private GameObject oneRoundedEdgePrefab;
    [SerializeField] private GameObject twoRoundedEdgesPrefab;
    [SerializeField] private GameObject fourRoundedEdgesPrefab;

    private GameObject currentPrefab;
    private Quaternion currentRotation;

    private void Start() {
        AdjustTextureBasedOnNeighbors();
    }

    public void AdjustTextureBasedOnNeighbors() {       
        int x = localCoordinates.x;
        int y = localCoordinates.y;

        bool top = IsOfSameType(x, y + 1);
        bool bottom = IsOfSameType(x, y - 1);
        bool left = IsOfSameType(x - 1, y);
        bool right = IsOfSameType(x + 1, y);

        int neighborCount = (top ? 1 : 0) + (bottom ? 1 : 0) + (left ? 1 : 0) + (right ? 1 : 0);

        GameObject selectedPrefab = noRoundedEdgesPrefab;
        Quaternion rotation = currentRotation;

        switch (neighborCount) {
            case 0:
                selectedPrefab = fourRoundedEdgesPrefab;
                break;
            case 1:
                selectedPrefab = twoRoundedEdgesPrefab;
                float yRot = 0;

                if (bottom) yRot = 180;
                if (left) yRot = 270;
                if (right) yRot = 90;

                rotation = Quaternion.Euler(90, yRot, 0);
                break;
            case 2:
                if (left && right) {
                    selectedPrefab = noRoundedEdgesPrefab;
                } else if (top && bottom) {
                    selectedPrefab = noRoundedEdgesPrefab;
                } else {
                    selectedPrefab = oneRoundedEdgePrefab;

                    if (top && left) rotation = Quaternion.Euler(90, 270, 0);
                    if (top && right) rotation = Quaternion.Euler(90, 0, 0);
                    if (bottom && left) rotation = Quaternion.Euler(90, 180, 0);
                    if (bottom && right) rotation = Quaternion.Euler(90, 90, 0);
                }
                break;
            case 3:
            case 4:
                selectedPrefab = noRoundedEdgesPrefab;
                rotation = Quaternion.Euler(0, 0, 0);
                break;
        }

        if (currentPrefab == selectedPrefab && currentRotation == rotation) {
            Debug.Log("Seems like the tile doesnt need any adjustment");
            return;
        }

        currentRotation = rotation;
        currentPrefab = selectedPrefab;

        Destroy(soilRoundedParent.transform.GetChild(0).gameObject);
        GameObject soilObject = Instantiate(currentPrefab, transform.position, currentRotation, soilRoundedParent.transform);

        AdjustNeighbours();
    }

    private void AdjustNeighbours() {
        List<RoundedTileObject<T>> neighbours = new List<RoundedTileObject<T>>() {
            TileManager.Instance.GetTile(localCoordinates.x + 1, localCoordinates.y) as T,
            TileManager.Instance.GetTile(localCoordinates.x - 1, localCoordinates.y) as T,
            TileManager.Instance.GetTile(localCoordinates.x, localCoordinates.y - 1) as T,
            TileManager.Instance.GetTile(localCoordinates.x, localCoordinates.y + 1) as T,
        };

        neighbours
        .FindAll(tile => {
            return tile != null;
        })
        .ForEach(tile => tile.AdjustTextureBasedOnNeighbors());
    }

    private bool IsOfSameType(int x, int y) {
        TileObject tile = TileManager.Instance.GetTile(x, y);
        return tile != null && tile is T;
    }
}
