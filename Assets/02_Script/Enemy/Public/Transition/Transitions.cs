using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transitions : MonoBehaviour
{
    /// <summary>
    /// �Ÿ��ȿ� target�� �ִ��� �˻��ϴ� �Լ�.
    /// </summary>
    public static bool CheckDistance(Transform targetTrm, Transform trm, float detectDistance)
    {
        return Vector3.Distance(targetTrm.position, trm.position) < detectDistance;
    }

    /// <summary>
    /// Ư�� ���⿡ ��ֹ��� �ִ��� Ȯ���ϴ� �Լ�.
    /// </summary>
    /// <param name="trm"> �� �ڽ��� Transform </param>
    /// <param name="dir"> ��� ������ �������� </param>
    /// <param name="distance"> ������ �������� </param>
    /// <param name="type"> ��� ��ֹ��� �������� </param>
    public static bool CheckObstacleAtPoint(Transform trm, Vector3 dir, float distance, EObstacleType type)
    {
        return true;
    }

    /// <summary>
    /// ���̿� ��ֹ��� �ִ��� Ȯ���ϴ� �Լ�.
    /// </summary>
    public static bool CheckObstacleBetweenTarget(Transform targetTrm, Transform trm, EObstacleType type)
    {
        return false;
        //if(type == ) 
        //return Physics2D.Raycast(trm.position, (targetTrm.position-trm.position), Mathf.Infinity, LayerMask.type);
    }
}
