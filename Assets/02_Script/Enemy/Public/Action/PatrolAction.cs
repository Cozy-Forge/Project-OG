using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PatrolAction<T> : BaseAction<T> where T : Enum
{
    Vector3 targetPos;
    float patrolRadius;
    bool idle;
    float idleTime;

    Coroutine idleCor;

    Transform debuggingTrm;

    //Find Route
    int moveIdx;
    Vector3 nextPos;
    Vector3 currentPos;
    List<Vector3> route;

    public PatrolAction(BaseFSM_Controller<T> controller, Transform debugTarget) : base(controller)
    {
        debuggingTrm = debugTarget;
        nextPos = controller.transform.position;
        targetPos = currentPos;
        patrolRadius = _data.Range;
        idle = false;
        idleTime = controller.EnemyDataSO.IdleTime;
        idleCor = null;
        route = new();
    }

    public override void OnEnter()
    {
        currentPos = TilemapManager.Instance.GetTilePos(controller.transform.position);
        idle = true;
        moveIdx = 0;
        route.Clear();
        StartIdleCor(idleTime);
  
        controller.ChangeColor(Color.white);
    }

    public override void OnExit()
    {
        idle = false;
        StopCoroutine(idleCor);
    }

    public override void OnUpdate()
    {
        if (!controller.Nav.IsNavActive) return;
        if (idle) return;

        if (route == null || route.Count == 0)
        {
            SetToMove();
            return;
        }

        Vector2 dir = nextPos - controller.transform.position;
        Vector2 position = _rigidbody.position + (dir.normalized * _data.Speed * Time.deltaTime);
        controller.Enemy.Rigidbody.MovePosition(position);
        
        if (dir.magnitude <= 0.05f)
        {
            SetNextTarget();
        }

        controller.PrintRoute(route);
    }

    private void SetNextTarget()
    {
        if (moveIdx >= route.Count)
        {
            //목적지에 도착
            route.Clear();
            idle = true;
            float idleTime = Random.Range(this.idleTime, this.idleTime + 0.5f);

            StartIdleCor(idleTime);

            return;
        }
        currentPos = route[moveIdx];
        nextPos = currentPos;
        moveIdx++;
    }

    private void SetToMove()
    {
        Debug.Log("SetTarget");
        targetPos = FindRandomPoint(controller.transform.position);
        route = controller.Nav.GetRoute(targetPos); //경로 검색
        if (route != null && route.Count != 0)
            nextPos = route[0];

        moveIdx = 0;
        idle = false;
    }
    private void StartIdleCor(float idleTime)
    {
        StopCoroutine(idleCor);
        idleCor = StartCoroutine(
              DelayCor
              (
                  idleTime,
                  afterAct:
                      () => SetToMove())
              );
    }
    private Vector2 FindRandomPoint(Vector2 pos)
    {
        Vector2 randomPos = controller.Nav.GetRandomPos(pos, patrolRadius);
        SetTarget(randomPos);
        return randomPos;
    }
    public void SetTarget(Vector2 target)
    {
        this.debuggingTrm.position = target;
    }

    public override void OnFixedUpdate()
    {
    }
}
