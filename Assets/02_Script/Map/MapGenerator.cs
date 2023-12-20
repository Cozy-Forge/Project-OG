using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] Vector2Int mapSize; //���ϴ� ���� ũ��
    [SerializeField] float minimumDevideRate; //������ �������� �ּ� ����
    [SerializeField] float maximumDevideRate; //������ �������� �ִ� ����
    [SerializeField] private GameObject line; //lineRenderer�� ����Ͽ� ������ �������� ������ �� �����ֱ� ����
    [SerializeField] private GameObject map; // lineRenderer�� ����ؼ� ù ���� ����� �����ֱ� ����
    [SerializeField] private int maximumDepth; //Ʈ���� ���� ���� ���� ���� �� �ڼ��� ������ ��
    [SerializeField] private GameObject roomLine; //lineRenderer�� ����ϼ� ���� ����� �����ֱ� ����

    private void Start()
    {
        MapNode root = new MapNode(new RectInt(0, 0, mapSize.x, mapSize.y));//��ü �� ũ���� ��Ʈ��带 ����
        DrawMap(0, 0);
        Divide(root, 0);
        GenerateRoom(root, 0);
        GenerateLoad(root, 0);
    }

    private void Divide(MapNode tree, int n)
    {
        if (n == maximumDepth) return; //���� ���ϴ� ���̿� ���޽� �� ���� ������ ����

        int maxLength = Mathf.Max(tree.nodeRect.width, tree.nodeRect.height);
        //���ο� ������ �� ����� ����, ���ΰ� ��ٸ� ��, ��� ���ΰ� �� ��ٸ� ��, �Ʒ��� ���� ���̴�.
        int split = Mathf.RoundToInt(Random.Range(maxLength * minimumDevideRate, maxLength * maximumDevideRate));
        //���� �� �ִ� �ִ� ���̿� �ּ� �����߿��� �������� �� ���� ����
        if(tree.nodeRect.width >= tree.nodeRect.height) // ���ΰ� �� ����� ��쿡�� �� ��� ������ �� ��쿡�� ���� ���̴� ��ȭ X
        {
            tree.leftNode = new MapNode(new RectInt(tree.nodeRect.x,tree.nodeRect.y, split,tree.nodeRect.height));
            //���� ��� ����
            //��ġ�� ���� �ϴ� �����̹Ƿ� ������������, ���α��̴� ������ ���� ���� ��
            tree.rightNode = new MapNode(new RectInt(tree.nodeRect.x + split, tree.nodeRect.y, tree.nodeRect.width - split, tree.nodeRect.height));
            //���� ��� ����
            //��ġ�� ���� �ϴܿ��� ������ ���� ���̸�ŭ �̵��� ��ġ�̸�, ���� ���̴� ���� ���α��̿��� ���� ���� ���ΰ��� �� ������ �κ��� �ȴ�.
            DrawLine(new Vector2(tree.nodeRect.x + split, tree.nodeRect.y), new Vector2(tree.nodeRect.x + split, tree.nodeRect.y + tree.nodeRect.height));
            //�� �� �� �ΰ��� ��带 ������ ���� �׸��� �Լ�
        }
        else
        {
            tree.leftNode = new MapNode(new RectInt(tree.nodeRect.x, tree.nodeRect.y, tree.nodeRect.width, split));
            tree.rightNode = new MapNode(new RectInt(tree.nodeRect.x, tree.nodeRect.y + split, tree.nodeRect.width, tree.nodeRect.height - split));
            DrawLine(new Vector2(tree.nodeRect.x, tree.nodeRect.y + split), new Vector2(tree.nodeRect.x + tree.nodeRect.width, tree.nodeRect.y + split));
            //���ΰ� �� ����� ����̴�. �ڼ��� ������ ���ο� ����
        }

        tree.leftNode.parentNode = tree; //�ڽĳ����� �θ��带 �������� ���� ����
        tree.rightNode.parentNode = tree;
        Divide(tree.leftNode, n + 1); //����, ������ �ڽ� ���鵵 �����ش�.
        Divide(tree.rightNode, n + 1);//����, ������ �ڽ� ���鵵 �����ش�.
    }

    private void DrawLine(Vector2 from, Vector2 to)
    {
        LineRenderer lineRenderer = Instantiate(line).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, from - mapSize / 2);
        lineRenderer.SetPosition(1, to - mapSize / 2);
    }

    private void DrawMap(int x, int y)
    {
        LineRenderer lineRenderer = Instantiate(map).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector2(x, y) - mapSize / 2);
        lineRenderer.SetPosition(1, new Vector2(x + mapSize.x, y) - mapSize / 2);
        lineRenderer.SetPosition(2, new Vector2(x + mapSize.x, y + mapSize.y) - mapSize / 2);
        lineRenderer.SetPosition(3, new Vector2(x, y + mapSize.y) - mapSize / 2);
    }

    private RectInt GenerateRoom(MapNode tree, int n)
    {
        RectInt rect;
        if (n == maximumDepth) //�ش� ��尡 ��������� ���� ����� �� ���̴�.
        {
            rect = tree.nodeRect;
            int width = Random.Range(rect.width / 2, rect.width - 1);
            //���� ���� �ּ� ũ��� ����� ���α����� ����, �ִ� ũ��� ���α��̺��� 1 �۰� ������ �� �� ���� ���� ������ ���� �����ش�.
            int height = Random.Range(rect.height / 2, rect.height - 1);
            //���̵� ���� ����.
            int x = rect.x + Random.Range(1, rect.width - width);
            //���� x��ǥ�̴�. ���� 0�� �ȴٸ� �پ� �ִ� ��� �������� ������,�ּڰ��� 1�� ���ְ�, �ִ��� ���� ����� ���ο��� ���� ���α��̸� �� �� ���̴�.
            int y = rect.y + Random.Range(1, rect.height - height);
            //y��ǥ�� ���� ����.
            rect = new RectInt(x, y, width, height);
            DrawRectangle(rect);
        }
        else
        {
            tree.leftNode.roomRect = GenerateRoom(tree.leftNode, n + 1);
            tree.rightNode.roomRect = GenerateRoom(tree.rightNode, n + 1);
            rect = tree.leftNode.roomRect;
        }
        return rect;
    }

    private void DrawRectangle(RectInt rect)
    {
        LineRenderer lineRenderer = Instantiate(roomLine).GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, new Vector2(rect.x, rect.y) - mapSize / 2); //���� �ϴ�
        lineRenderer.SetPosition(1, new Vector2(rect.x + rect.width, rect.y) - mapSize / 2); //���� �ϴ�
        lineRenderer.SetPosition(2, new Vector2(rect.x + rect.width, rect.y + rect.height) - mapSize / 2);//���� ���
        lineRenderer.SetPosition(3, new Vector2(rect.x, rect.y + rect.height) - mapSize / 2); //���� ���
    }

    private void GenerateLoad(MapNode tree, int n)
    {
        if (n == maximumDepth) //���� ����� ���� �ڽ��� ����.
            return;
        Vector2Int leftNodeCenter = tree.leftNode.center;
        Vector2Int rightNodeCenter = tree.rightNode.center;

        DrawLine(new Vector2(leftNodeCenter.x, leftNodeCenter.y), new Vector2(rightNodeCenter.x, leftNodeCenter.y));
        //���� ������ leftnode�� ���缭 ���� ������ ��������.
        DrawLine(new Vector2(rightNodeCenter.x, leftNodeCenter.y), new Vector2(rightNodeCenter.x, rightNodeCenter.y));
        //���� ������ rightnode�� ���缭 ���� ������ ��������.
        GenerateLoad(tree.leftNode, n + 1); //�ڽ� ���鵵 Ž��
        GenerateLoad(tree.rightNode, n + 1);
    }
}
