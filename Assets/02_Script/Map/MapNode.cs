using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNode
{
    public MapNode leftNode;
    public MapNode rightNode;
    public MapNode parentNode;
    public RectInt nodeRect;
    public RectInt roomRect;
    public Vector2Int center
    {
        get
        {
            return new Vector2Int(roomRect.x + roomRect.width / 2, roomRect.y + roomRect.height / 2);
        }
        //���� ��� ��. ��� ���� ���� �� ���
    }
    public MapNode(RectInt rect)
    {
        this.nodeRect = rect;
    }
}
