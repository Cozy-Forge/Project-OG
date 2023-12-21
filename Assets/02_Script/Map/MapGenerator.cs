using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private GameObject MainTile;
    [SerializeField] protected Vector3 MainMapSize;
    [SerializeField] private int MapCount;

    private List<Vector3> mapPositions = new List<Vector3>();
    private Vector3 currentMapPosition;

    private void Start()
    {
        Instantiate(MainTile, new Vector2(0,0), Quaternion.identity, transform);
        currentMapPosition = Vector3.zero;
        GenerateMapChain(MapCount);
    }

    private void GenerateMapChain(int numberOfMaps)
    {
        mapPositions.Add(currentMapPosition);
        for (int i = 0; i < numberOfMaps; i++)
        {
            Vector3 nextPosition = GetNextValidPosition();
            Instantiate(MainTile, nextPosition, Quaternion.identity, transform);
            mapPositions.Add(nextPosition);
            currentMapPosition = nextPosition;
        }
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
