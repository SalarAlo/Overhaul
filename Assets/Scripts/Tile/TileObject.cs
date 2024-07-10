using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public enum TileType {
        Grass,
        Soil,
        Other,
    }

    [SerializeField] private TileType tileType;

    public void SetTile(TileType tileType){
        this.tileType = tileType;
    }
}
