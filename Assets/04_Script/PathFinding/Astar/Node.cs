using System;
using UnityEngine;

namespace Astar
{
    public enum NodeType
    {
        MoveAble, //�̵��� �� �ִ�
        Locomobile, //�̵��� �� ����
    }

    [Serializable]
    public class Node : System.IComparable<Node>
    {
        public Node Parent;
        public Vector3Int Pos;
        public NodeType Type;
        public int Weight; // ����ġ
        public float F;
        public float G;

        public int CompareTo(Node other)
        {
            if (other.F == this.F) return 0;
            else
                return other.F < this.F ? -1 : 1;
        }

        public override bool Equals(object obj) => this.Equals(obj as Node);
        public override int GetHashCode() => base.GetHashCode();

        public bool Equals(Node node)
        {
            if (node is null)
                return false;
            if (this.GetType() != node.GetType())
                return false;

            return GetHashCode() == node.GetHashCode();
        }

        public static bool operator ==(Node lhs, Node rhs)
        {
            if (lhs is null)
            {
                if (rhs is null)
                    return true;
                else
                    return false;
            }
            return lhs.Equals(rhs);
        }

        public static bool operator !=(Node lhs, Node rhs) => !(lhs == rhs);


        public void Reset()
        {
            Parent = null;
            F = default(int);
            G = default(int);
        }

    }
}

