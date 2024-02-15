using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Astar
{
    public class NodeGenerator
    {
        public static List<Node> MakeNode(Vector3Int worldCenterPos, BoundsInt bounds)
        {
            List<Node> nodes = new List<Node>();

            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {
                //����ġ�� ��ֹ� �ϼ��ǰ� ���� �Ҵ�.
                //int Weight =
                Node node = new Node();
                node.Pos = pos + worldCenterPos;
                node.Weight = 1;
                nodes.Add(node);
            }

            return nodes;
        }
    }
}
