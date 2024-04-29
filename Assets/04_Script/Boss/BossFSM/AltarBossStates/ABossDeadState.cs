using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABossDeadState : BossBaseState
{
    private AltarBoss _altar;
    private bool _make;

    public ABossDeadState(AltarBoss boss, AltarPattern pattern) : base(boss, pattern)
    {
        _altar = boss;
        _make = true;
    }

    public override void OnBossStateExit()
    {
        
    }

    public override void OnBossStateOn()
    {
        _altar.gameObject.layer = LayerMask.NameToLayer("Default");
        _altar.mediumSizeBody.SetActive(false);
        _altar.smallestBody.SetActive(false);
        _altar.ChangeSprite(_altar.bigestBody, _altar.bigTriangleSprite);
        _altar.StopAllCoroutines();
        _altar.ReturnAll();
        _altar.ChainReturnAll();
        NowCoroutine(Dying(3, 2));
    }

    public override void OnBossStateUpdate()
    {
        
    }

    private IEnumerator Dying(float dyingTime, float disappearingTime)
    {
        SoundManager.Instance.SFXPlay("Dead", _altar.deadClip, 1);
        _altar.StartCoroutine(MakeBullets());
        yield return new WaitForSeconds(dyingTime);
        _make = false;
        _altar.StartCoroutine(ActiveFalse(_altar.bigestBody, disappearingTime));
        _altar.StartCoroutine(ActiveFalse(_altar.gameObject, disappearingTime));
    }

    private IEnumerator MakeBullets()
    {
        PolygonCollider2D polygon = _altar.GetComponent<PolygonCollider2D>();
        List<GameObject> objList = new List<GameObject>();
        float maxX = 0;
        float minX = 0;
        float maxY = 0;
        float minY = 0;
        int listCount = 0;
        Vector3 a = Vector2.zero;
        Vector3 b = Vector2.zero;
        Vector3 c = Vector2.zero;

        if (polygon)
        {
            Vector2[] points = polygon.GetPath(0);
            if(points.Length >= 3)
            {
                a = points[0]; // ���� �Ʒ��� �ִ� ��
                b = points[1]; // ���� �����ʿ� �ִ� ��
                c = points[2]; // ���� ���ʿ� �ִ� ��

                maxX = b.x;
                minX = c.x;
                maxY = b.y;
                minY = a.y;
            }
        }
        while(_make)
        {
            float randX = Random.Range(minX, maxX);
            float randY = Random.Range(minY, maxY);

            Vector3 randVec = new Vector2(randX, randY);

            Vector2 ab = b - a;
            Vector2 bc = c - b;
            Vector2 ca = a - c;

            Vector2 ar = randVec - a;
            Vector2 br = randVec - b;
            Vector2 cr = randVec - c;

            if((Vector3.Cross(ab, ar).z > 0 && Vector3.Cross(bc, br).z > 0 && Vector3.Cross(ca, cr).z > 0)
                || (Vector3.Cross(ab, ar).z < 0 && Vector3.Cross(bc, br).z < 0 && Vector3.Cross(ca, cr).z < 0))
            {
                objList.Add(ObjectPool.Instance.GetObject(ObjectPoolType.BossBulletType0, _altar.bulletCollector.transform));
                objList[listCount].transform.position = _altar.transform.position + randVec;
                objList[listCount].transform.rotation = Quaternion.identity;
                listCount++;
            }

            yield return null;
        }

        for(int i = 0; i < objList.Count; i++)
        {
            Rigidbody2D rigid = objList[i].GetComponent<Rigidbody2D>();
            Vector2 dir = new Vector2(Mathf.Cos(Mathf.PI * 2 * i / objList.Count), Mathf.Sin(Mathf.PI * 2 * i / objList.Count)).normalized;
            rigid.velocity = dir * 5;
        }
        yield return null;
    }
}
