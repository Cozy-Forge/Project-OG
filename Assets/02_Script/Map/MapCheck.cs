using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapCheck : MonoBehaviour
{
    [SerializeField] private GameObject Lload;
    [SerializeField] private GameObject LWall;
    [SerializeField] private GameObject Rload;
    [SerializeField] private GameObject RWall;
    [SerializeField] private GameObject Upload;
    [SerializeField] private GameObject UpWall;
    [SerializeField] private GameObject Download;
    [SerializeField] private GameObject DownWall;

    private Tilemap tilemap;

    private void Start()
    {
        // Get the Tilemap component from the current GameObject
        tilemap = GetComponent<Tilemap>();

        CheckRightMap();
    }

    private void CheckRightMap()
    {
        Vector3Int currentPosition = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        Vector3Int rightPosition = currentPosition + new Vector3Int(33, 0, 0); // Check the tile to the right

        TileBase rightTile = tilemap.GetTile(rightPosition);

        // If there's a tile to the right, deactivate Road and activate RWall
        if (rightTile != null)
        {
            Rload.SetActive(false);
            RWall.SetActive(true);
        }
        // If there's no tile to the right, activate Road and deactivate RWall
        else
        {
            Rload.SetActive(true);
            RWall.SetActive(false);
        }
    }
}
