using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Astar
{
    public class NodeGenerator
    {
        public static List<Node> MakeNode(Vector3Int worldCenterPos, BoundsInt bounds, out List<Node> roomWallNodes)
        {
            List<Node> nodes = new List<Node>();
            roomWallNodes = new List<Node>();

            foreach (Vector3Int pos in bounds.allPositionsWithin)
            {

                Node node = new Node();
                node.Pos = pos + worldCenterPos;
                node.Type = NodeType.MoveAble;


                int wallLayer = LayerMask.GetMask("Wall");
                int obstacleLayer = LayerMask.GetMask("Obstacle");

                //����ġ�� ��ֹ� �ϼ��ǰ� ���� �Ҵ�.
                Collider2D collider = Physics2D.OverlapCircle(TilemapManager.Instance.GetWorldPos(pos)
                                                                  ,0.5f, wallLayer | obstacleLayer);
                
                if(collider != null)
                {
                    if(collider.gameObject.layer == wallLayer)
                    {
                        roomWallNodes.Add(node);
                        node.Type = NodeType.Locomobile;
                    }
                    else if(collider.gameObject.layer == obstacleLayer)
                    {
                        // Obstacle ����ġ ��������
                        // node.Weight = ??;
                    }
                }
                else
                {
                    nodes.Add(node);
                }
            }

            return nodes;
        }
    }
}
