using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    private float targetDetectionRange;

    [SerializeField]
    private LayerMask obstaclesLayerMask, playerLayerMask;

    [SerializeField]
    private bool showGizmos = false;

    //gizmo parameters
    private List<Transform> colliders;
    private bool checkObstacle;

    public TargetDetector(Transform ownerTrm, EnemyDataSO dataSO) : base(ownerTrm)
    {
        this.obstaclesLayerMask = dataSO.ObstacleLayer;
        this.playerLayerMask = dataSO.TargetAbleLayer;
        this.checkObstacle = dataSO.CheckObstacle;
        this.targetDetectionRange = dataSO.Range;
        //GizmoDrawer.Instance.Add(OnDrawGizmosSelected);
    }

    public override void Detect(AIData aiData)
    {
        //Find out if player is near
        Collider2D playerCollider =
            Physics2D.OverlapCircle(transform.position, targetDetectionRange, playerLayerMask);

        if (playerCollider != null)
        {
            //Check if you see the player
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            if (checkObstacle)
            {
                RaycastHit2D hit = 
                    Physics2D.Raycast(transform.position, direction, targetDetectionRange, obstaclesLayerMask | playerLayerMask);

                ////Make sure that the collider we see is on the "Player" layer
                //Debug.Log(playerCollider);
                if (hit.collider != null && (playerLayerMask & (1 << hit.collider.gameObject.layer)) != 0)
                {
                    Debug.DrawRay(transform.position, direction * targetDetectionRange, Color.magenta);
                    colliders = new List<Transform>() { playerCollider.transform };
                }
                else
                {
                    //colliders = new List<Transform>() { GameManager.Instance.player.transform };
                    colliders = null;
                }
            }
            else
            {
                colliders = new List<Transform>() { playerCollider.transform };
            }
        }
        else
        {
            colliders = null;
        }
        aiData.targets = colliders;
    }

    private void OnDrawGizmosSelected()
    {
        if (showGizmos == false)
            return;

        Gizmos.DrawWireSphere(transform.position, targetDetectionRange);

        if (colliders == null)
            return;
        Gizmos.color = Color.magenta;
        foreach (var item in colliders)
        {
            Gizmos.DrawSphere(item.position, 0.3f);
        }
    }
}
