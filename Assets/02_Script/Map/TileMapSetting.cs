using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapSetting : MapGenerator
{
    [SerializeField] Tilemap tileMap;
    [SerializeField] Tile roomTile; //���� �����ϴ� Ÿ��
    [SerializeField] Tile wallTile; //��� �ܺθ� ���������� �� Ÿ��
    [SerializeField] Tile outTile; //�� �ܺ��� Ÿ��


    protected override void Start()
    {
        base.Start();
        FillWall();
    }

    void FillWall() //�� Ÿ�ϰ� �ٱ� Ÿ���� ������ �κ�
    {
        for (int i = 0; i < mapSize.x; i++) //Ÿ�� ��ü�� ��ȸ
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0)) == outTile)
                {
                    //�ٱ�Ÿ�� �� ���
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0) continue;//�ٱ� Ÿ�� ���� 8������ Ž���ؼ� room tile�� �ִٸ� wall tile�� �ٲ��ش�.
                            if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2 + x, j - mapSize.y / 2 + y, 0)) == roomTile)
                            {
                                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), wallTile);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
    private void FillRoom(RectInt rect)
    { //room�� rect������ �޾Ƽ� tile�� set���ִ� �Լ�
        for (int i = rect.x; i < rect.x + rect.width; i++)
        {
            for (int j = rect.y; j < rect.y + rect.height; j++)
            {
                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), roomTile);
            }
        }
    }
}
