using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Astar;
using UnityEngine.Tilemaps;
using System.Linq;

public class AstarTest : MonoBehaviour
{
    [SerializeField] Enemy testEnemy;
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    Transform controllerTrm;
    [SerializeField]
    Transform playerTrm;
    [SerializeField]
    LineRenderer lineRenderer;

    Navigation manager;
    List<Vector3> route;
    int moveIdx;

    private void Start()
    {
        manager = new Navigation(testEnemy);
        //route = manager.GetRoute(testEnemy.TargetTrm.position);
        PrintRoute();
    }

    private void PrintRoute()
    {
        if (route.Count < 2 || route == null) return;
        lineRenderer.positionCount = route.Count;

        lineRenderer.SetPositions(route.ToArray());
    }

    private void Update()
    {
        //route = manager.GetRoute(testEnemy.TargetTrm.position);
        Debug.Log(route);
        PrintRoute();

    }



}
