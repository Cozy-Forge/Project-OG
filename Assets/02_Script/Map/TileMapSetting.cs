using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapSetting : MapGenerator
{
    [SerializeField] Tilemap tileMap;
    [SerializeField] Tile roomTile; //방을 구성하는 타일
    [SerializeField] Tile wallTile; //방과 외부를 구분지어줄 벽 타일
    [SerializeField] Tile outTile; //방 외부의 타일


    protected override void Start()
    {
        base.Start();
        FillWall();
    }

    void FillWall() //룸 타일과 바깥 타일이 만나는 부분
    {
        for (int i = 0; i < mapSize.x; i++) //타일 전체를 순회
        {
            for (int j = 0; j < mapSize.y; j++)
            {
                if (tileMap.GetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0)) == outTile)
                {
                    //바깥타일 일 경우
                    for (int x = -1; x <= 1; x++)
                    {
                        for (int y = -1; y <= 1; y++)
                        {
                            if (x == 0 && y == 0) continue;//바깥 타일 기준 8방향을 탐색해서 room tile이 있다면 wall tile로 바꿔준다.
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
    { //room의 rect정보를 받아서 tile을 set해주는 함수
        for (int i = rect.x; i < rect.x + rect.width; i++)
        {
            for (int j = rect.y; j < rect.y + rect.height; j++)
            {
                tileMap.SetTile(new Vector3Int(i - mapSize.x / 2, j - mapSize.y / 2, 0), roomTile);
            }
        }
    }
}
