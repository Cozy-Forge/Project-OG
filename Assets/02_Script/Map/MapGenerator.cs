using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private MapCheck MainTile;
    [SerializeField] protected Vector3 MainMapSize;
    [SerializeField] private int MapCount;

    [SerializeField] private List<MapCheck> Checks = new List<MapCheck>();
    private List<Vector3> mapPositions = new List<Vector3>();
    private Vector3 currentMapPosition;

    private void Start()
    {
        Checks.Add(Instantiate(MainTile, new Vector2(0,0), Quaternion.identity, transform));
        currentMapPosition = Vector3.zero;
        GenerateMapChain(MapCount);
    }

    private void ConnectRoad()
    {

        foreach(var map in Checks)
        {

            Debug.Log(map.transform.right);

            ///
            if(Checks.Find(x => x.rt.Contains(map.transform.position + (Vector3.right * MainMapSize.x / 1.5f)) && x != map) == null)
            {
                map.CheckRightMap(OpenClose.Right);
                Debug.Log(map.gameObject);
            }
            if (Checks.Find(x => x.rt.Contains(map.transform.position + (Vector3.up * MainMapSize.y / 1.5f)) && x != map) == null)
            {
                map.CheckRightMap(OpenClose.Up);
                Debug.Log(map.gameObject);
            }
            if (Checks.Find(x => x.rt.Contains(map.transform.position + (Vector3.down * MainMapSize.y / 1.5f)) && x != map) == null)
            {
                map.CheckRightMap(OpenClose.Down);
                Debug.Log(map.gameObject);
            }
            if (Checks.Find(x => x.rt.Contains(map.transform.position + (Vector3.left * MainMapSize.x / 1.5f)) && x != map) == null)
            {
                map.CheckRightMap(OpenClose.Left);
                Debug.Log(map.gameObject);
            }

        }

    }

    private void GenerateMapChain(int numberOfMaps)
    {
        mapPositions.Add(currentMapPosition);
        for (int i = 0; i < numberOfMaps; i++)
        {
            Vector3 nextPosition = GetNextValidPosition();
            Checks.Add(Instantiate(MainTile, nextPosition, Quaternion.identity, transform));
            mapPositions.Add(nextPosition);
            currentMapPosition = nextPosition;
            Debug.Log(123);
        }


        ConnectRoad();
    }

    private Vector3 GetNextValidPosition()
    {
        List<Vector3> possiblePositions = new List<Vector3>();

        foreach (Vector3 position in mapPositions)
        {
            Vector3 right = position + new Vector3(MainMapSize.x, 0, 0);
            Vector3 left = position - new Vector3(MainMapSize.x, 0, 0);
            Vector3 up = position + new Vector3(0, MainMapSize.y, 0);
            Vector3 down = position - new Vector3(0, MainMapSize.y, 0);

            if (!mapPositions.Contains(right))
                possiblePositions.Add(right);
            if (!mapPositions.Contains(left))
                possiblePositions.Add(left);
            if (!mapPositions.Contains(up))
                possiblePositions.Add(up);
            if (!mapPositions.Contains(down))
                possiblePositions.Add(down);
        }

        if (possiblePositions.Count == 0)
        {
            return currentMapPosition;
        }

        int randomIndex = Random.Range(0, possiblePositions.Count);
        return possiblePositions[randomIndex];
    }
}
