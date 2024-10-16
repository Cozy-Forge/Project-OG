using UnityEngine;

public class ObstacleAvoidanceBehaviour : SteeringBehaviour
{
    [SerializeField]
    private float radius = 2f, agentColliderSize = 0.8f;

    //gizmo parameters
    float[] dangersResultTemp = null;

    public ObstacleAvoidanceBehaviour(Transform ownerTrm) : base(ownerTrm)
    {
         //GizmoDrawer.Instance.Add(OnDrawGizmos);
    }
    ~ObstacleAvoidanceBehaviour()
    {

        //Debug.Log("����");
        //GizmoDrawer.Instance.Remove(OnDrawGizmos);
    }

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AIData aiData)
    {
        foreach (Collider2D obstacleCollider in aiData.obstacles)
        {
            Vector2 directionToObstacle
                = obstacleCollider.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

            //calculate weight based on the distance Enemy<--->Obstacle
            float weight
                = distanceToObstacle <= agentColliderSize
                ? 1
                : (radius - distanceToObstacle) / radius;

            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;

            bool isWall = obstacleCollider.gameObject.layer == LayerMask.NameToLayer("Wall");
            if (isWall) weight += 1;
            //Add obstacle parameters to the danger array
            for (int i = 0; i < Directions.eightDirections.Count; i++)
            {
                float result = Vector2.Dot(directionToObstacleNormalized, Directions.eightDirections[i]);

                float valueToPutIn = result * weight;
           
                //override value only if it is higher than the current one stored in the danger array
                if (valueToPutIn > danger[i])
                {
                    danger[i] = valueToPutIn;
                }
            }
        }
        dangersResultTemp = danger;
        return (danger, interest);
    }

    public override void OnDestroy()
    {
        //throw new System.NotImplementedException();
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying && dangersResultTemp != null)
        {
            if (dangersResultTemp != null)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < dangersResultTemp.Length; i++)
                {
                    Gizmos.DrawRay(
                        transform.position,
                        Directions.eightDirections[i] * dangersResultTemp[i]*2
                        );
                }
            }
        }

    }
}