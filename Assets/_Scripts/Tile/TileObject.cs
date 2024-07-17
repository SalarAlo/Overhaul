using System;
using System.Collections.Generic;
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
    private Plot plot;

    public void Initialize(int x, int y, Plot plot) {
        this.plot = plot;
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
        meshRenderer.material = marked;
    }

    public Vector2Int GetCoordinates() => coordinates;

    public bool AllNeighboursInPlot() {
        List<Vector2Int> neighbours = new List<Vector2Int>() {
            GetCoordinates() + Vector2Int.down,
            GetCoordinates() + Vector2Int.up,
            GetCoordinates() + Vector2Int.left,
            GetCoordinates() + Vector2Int.right,
        };

        foreach(Vector2Int neighbourCoord in neighbours) {
            if(!plot.ContainsTile(neighbourCoord.x, neighbourCoord.y))  {
                return false;
            }
        }

        return true;
    }
}
