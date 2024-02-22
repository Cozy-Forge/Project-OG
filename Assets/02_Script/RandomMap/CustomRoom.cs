using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CustomRoom
{
    public Tilemap wallTilemap;
    public Tilemap tilemap;

    public GameObject obstacleParent;

    public int width; // 벽포함 가로
    public int height; // 벽포함 세로

    public Vector3 centerPos;
    public Vector3 neighborPos;
}
