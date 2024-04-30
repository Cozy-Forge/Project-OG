using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct ConnectInfo
{
    public Vector2 pos;

    public ConnectInfo(Vector2 pos)
    {
        this.pos = pos;
    }
}

public class ConnectVisible : MonoBehaviour
{
    private WeaponInventory inventory;
    private InventoryActive inventoryActive;
    private InventorySize invenSize;
    Canvas canvas;
    private InvenBrick[] brickList;

    [SerializeField]
    LineRenderer tempObj;

    List<LineRenderer> lendererList = new List<LineRenderer>();
    [SerializeField] Material lineRenderMat;
    Material curMat = null;


    [HideInInspector] public float mulX = 2.0f;
    [HideInInspector] public float mulY = 2.0f;
     public float width = 0.2f;

    private int maxCnt = 0;

    private void Awake()
    {
        invenSize = FindObjectOfType<InventorySize>();
        inventoryActive = FindObjectOfType<InventoryActive>();
        inventory = FindObjectOfType<WeaponInventory>();
        canvas = GetComponentInParent<Canvas>();
        GameManager.Instance.Inventory.camerasetting += SettingOption;
    }
    private void Start()
    {
        GameManager.Instance.Inventory.OnAddItem += VisibleLine;
    }

    public void SettingOption()
    {
        //canvas.renderMode = RenderMode.ScreenSpaceCamera;
        //canvas.worldCamera = Camera.main;
    }

    public void VisibleLine()
    {
        for (int i = lendererList.Count - 1; i >= 0; i--)
        {
            var line = lendererList[i];
            lendererList.Remove(line);
            Destroy(line.gameObject);
        }

        brickList = GetComponentsInChildren<InvenBrick>();
        List<InvenBrick> generatorList = new List<InvenBrick>();
        foreach (var brick in brickList)
        {
            if (brick.Type == ItemType.Generator && !brick.IsDrag)
                generatorList.Add(brick);
        }

        foreach (var generator in generatorList)
        {
            maxCnt = 0;
            curMat = generator.InvenObject.colorMat;

            foreach (var vec in generator.InvenObject.sendPoints)
            {
                Dictionary<Vector2Int, int> weaponData = new();
                InventoryObjectData b = null;
                do
                {
                    Dictionary<ConnectInfo, bool> dic = new Dictionary<ConnectInfo, bool>();
                    LineRenderer line = CreateLine();

                    //line.positionCount += 1;

                    //Vector3 pos = generator.transform.position;// / invenSize.positionRatio;

                    //pos.z = -4;
                    //line.SetPosition(line.positionCount - 1, pos);

                    Vector3 localPos = generator.RectTransform.localPosition;

                    if (GameManager.Instance.Inventory.StartWidth % 2 == 0)
                        localPos.x += 50;
                    if (GameManager.Instance.Inventory.StartHeight % 2 == 0)
                        localPos.y -= 50;
                    Vector2Int invenpoint = Vector2Int.RoundToInt(localPos / 100);
                    AddLineRenderPoint(line, invenpoint);

                    b = null;
                    Connect(ref line, generator.InvenObject.originPos + vec.dir + vec.point, vec.dir, invenpoint, dic, maxCnt, ref weaponData, ref b);
                    AddLineRenderPoint(line, invenpoint);
                } while (b != null);
            }

        }
    }

    bool Connect(ref LineRenderer line, Vector2 pos, Vector2Int dir, Vector2Int originpos, Dictionary<ConnectInfo, bool> isVisited, int cnt, ref Dictionary<Vector2Int, int> weaponData, ref InventoryObjectData findWeapon)
    {
        Vector2Int tempVec = originpos + dir;
        ConnectInfo info = new ConnectInfo(pos);
        #region 스택 오버 플로우 방지 <- 방문한곳 체크
        if (isVisited.ContainsKey(info) && isVisited[info])
            return false;

        isVisited[info] = true;
        #endregion

        Vector2Int fillCheckVec = new Vector2Int((int)pos.x, (int)pos.y);

        if (inventory.CheckFill2(fillCheckVec))
        {
            InventoryObjectData data = inventory.GetObjectData2(fillCheckVec, dir);
            if (data != null)
            {

                BrickPoint b = new BrickPoint();
                bool isConnect = false;
                bool isconnect = false;

                #region 무기 예외처리
                if (data.sendPoints.Count == 0)
                {
                    isConnect = true;
                    Dictionary<ConnectInfo, bool> copiedDict = new Dictionary<ConnectInfo, bool>();
                    foreach (var kvp in isVisited)
                    {
                        copiedDict.Add(kvp.Key, kvp.Value);
                    }
                    isconnect = BrickCircuit(b, tempVec, ref line, data, copiedDict, cnt + 1, ref weaponData, ref findWeapon);
                }
                #endregion

                #region 연결된 블록 있으면 스택에 추가 없으면 리턴

                foreach (var point in data.inputPoints)
                {
                    if (point.dir == dir && point.point == fillCheckVec - data.originPos)
                    {
                        foreach (var point1 in data.bricks)
                        {
                            if (point.point == point1.point)
                            {
                                b.point = point.point;
                                b.dir = point1.dir;

                                Dictionary<ConnectInfo, bool> copiedDict = new Dictionary<ConnectInfo, bool>();
                                foreach (var kvp in isVisited)
                                {
                                    copiedDict.Add(kvp.Key, kvp.Value);
                                }

                                if (BrickCircuit(b, tempVec, ref line, data, copiedDict, cnt + 1, ref weaponData, ref findWeapon))
                                {
                                    isconnect = true;
                                    AddLineRenderPoint(line, tempVec);
                                }
                            }
                        }
                        isConnect = true;
                    }
                }

                if (!isConnect) return false;
                #endregion

                //isconnect = BrickCircuit(b, tempVec, line, data, isVisited, cnt + 1);
                //if (isconnect)
                //{
                //    AddLineRenderPoint(line, tempVec);
                //}

                return isconnect;
                //연결된 블록 순회
            }
        }

        return false;
    }

    private bool BrickCircuit(BrickPoint tmpVec, Vector2Int tempVec, ref LineRenderer line, InventoryObjectData data, Dictionary<ConnectInfo, bool> isVisited, int cnt, ref Dictionary<Vector2Int, int> weaponData, ref InventoryObjectData findWeapon)
    {
        //라인 렌더러에 추가
        AddLineRenderPoint(line, tempVec);

        bool isConnect = false;

        #region 무기면 리턴
        if (tmpVec.dir == null)
        {
            if (findWeapon != null && findWeapon != data)
            {
                DeleteLineRenderPoint(line);
                return false;
            }
            if (weaponData.ContainsKey(data.originPos))
            {
                if (weaponData[data.originPos] < cnt)
                {
                    findWeapon = data;
                    weaponData[data.originPos] = cnt;
                    line.positionCount = 0;
                    line = CreateLine();
                    AddLineRenderPoint(line, tempVec);
                    return true;
                }
                else
                {
                    DeleteLineRenderPoint(line);
                    return false;

                }
            }
            else
            {
                findWeapon = data;
                weaponData.Add(data.originPos, cnt);
                line.positionCount = 0;
                line = CreateLine();
                AddLineRenderPoint(line, tempVec);
                return true;
            }

        }
        #endregion

        //이친구와 이어진 블록 연결
        foreach (BrickPoint point in data.bricks)
        {
            //방향검사
            foreach (var v in tmpVec.dir)
            {
                //전블록과 이어지면 연결
                if (point.point == tmpVec.point + v)// 0,0 1,1
                {

                    Vector2 tempPos = data.originPos + point.point;
                    ConnectInfo info = new ConnectInfo(tempPos);

                    if (isVisited.ContainsKey(info) && isVisited[info])
                        continue;

                    isVisited[info] = true;


                    tempVec += v;
                    BrickPoint b;
                    b.point = point.point;
                    b.dir = point.dir;

                    Dictionary<ConnectInfo, bool> copiedDict = new Dictionary<ConnectInfo, bool>();
                    foreach (var kvp in isVisited)
                    {
                        copiedDict.Add(kvp.Key, kvp.Value);
                    }

                    if (BrickCircuit(b, tempVec, ref line, data, copiedDict, cnt, ref weaponData, ref findWeapon))
                    {
                        AddLineRenderPoint(line, tempVec);
                        isConnect = true;
                    }

                    tempVec -= v;
                }
            }
        }

        #region 연결된 곳 순회
        foreach (SignalPoint point1 in data.sendPoints)
        {
            if (point1.point == tmpVec.point)
            {
                Vector2 tempPos1 = data.originPos + point1.dir + point1.point;

                Dictionary<ConnectInfo, bool> copiedDict = new Dictionary<ConnectInfo, bool>();
                foreach (var kvp in isVisited)
                {
                    copiedDict.Add(kvp.Key, kvp.Value);
                }

                if (Connect(ref line, tempPos1, point1.dir, tempVec, copiedDict, cnt + 1, ref weaponData, ref findWeapon))
                {
                    AddLineRenderPoint(line, tempVec);
                    isConnect = true;
                }

                //라인 렌더러에 추가
            }
        }
        if (isConnect)
            return true;

        DeleteLineRenderPoint(line);
        return false;
        #endregion
    }

    private void AddLineRenderPoint(LineRenderer line, Vector2Int pos, int index = -1)
    {
        line.positionCount += 1;

        Vector3? realPos = GameManager.Instance.Inventory.FindInvenPointPos(pos);

        if (realPos == null)
            return;
        Vector3 drawPos = new Vector3(realPos.Value.x, realPos.Value.y, -4);



        if (index == -1)
            line.SetPosition(line.positionCount - 1, drawPos);
        else
            line.SetPosition(index, drawPos);
    }

    private void DeleteLineRenderPoint(LineRenderer line)
    {
        line.positionCount -= 1;
    }

    private LineRenderer CreateLine()
    {
        LineRenderer line = Instantiate(tempObj, transform).GetComponent<LineRenderer>();
        if (curMat == null)
            line.material = lineRenderMat;
        else
            line.material = curMat;
        line.startWidth = width;
        lendererList.Add(line);
        return line;
    }
}