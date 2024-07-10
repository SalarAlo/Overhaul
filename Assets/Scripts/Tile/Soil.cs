using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Soil : TileObject
{
    [SerializeField] private GameObject noRoundedEdgesPrefab;
    [SerializeField] private GameObject oneRoundedEdgePrefab;
    [SerializeField] private GameObject twoRoundedEdgesPrefab;
    [SerializeField] private GameObject fourRoundedEdgesPrefab;

    private void AdjustTextureBasedOnNeighbors() {
        TileManager tileManager = TileManager.Instance;
        int x = localCoordinates.x;
        int y = localCoordinates.y;

        bool top = IsSoil(x, y + 1);
        bool bottom = IsSoil(x, y - 1);
        bool left = IsSoil(x - 1, y);
        bool right = IsSoil(x + 1, y);

        int neighborCount = (top ? 1 : 0) + (bottom ? 1 : 0) + (left ? 1 : 0) + (right ? 1 : 0);

        GameObject selectedPrefab = noRoundedEdgesPrefab;
        Quaternion rotation = Quaternion.identity;

        switch(neighborCount) {
            case 0:
                selectedPrefab = fourRoundedEdgesPrefab;
                break;
            case 1:
                selectedPrefab = twoRoundedEdgesPrefab;
                float yRot = 0;

                if(bottom) yRot = 180;
                if(left) yRot = 270;
                if(right) yRot = 90;

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
                selectedPrefab = noRoundedEdgesPrefab;
                break;
            case 4:
                selectedPrefab = noRoundedEdgesPrefab;
                break;
        }

        GameObject soilObject = Instantiate(selectedPrefab, transform.position, rotation, transform.parent);
        soilObject.GetComponent<TileObject>().SetCoordinates(x, y);
        TileManager.Instance.ReplaceTile(x, y, soilObject.GetComponent<TileObject>());
    }

    private void Update() {
        if(Input.GetKeyDown(KeyCode.T)) {
            AdjustTextureBasedOnNeighbors();
        }
    }

    private bool IsSoil(int x, int y) {
        if (x < 0 || y < 0 || x >= TileManager.Instance.GetSizeX() || y >= TileManager.Instance.GetSizeY())
            return false;

        TileObject tile = TileManager.Instance.GetTile(x, y);
        return tile != null && tile is Soil;
    }
}
