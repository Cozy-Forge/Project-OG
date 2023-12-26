using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum OpenClose
{
    Left,
    Right,
    Up,
    Down

}


public class MapCheck : MonoBehaviour
{
    [SerializeField] private GameObject Lload;
    [SerializeField] private GameObject LWall;
    [SerializeField] private GameObject Rload;
    [SerializeField] private GameObject RWall;
    [SerializeField] private GameObject Upload;
    [SerializeField] private GameObject UpWall;
    [SerializeField] private GameObject Download;
    [SerializeField] private GameObject DownWall;

    [field:SerializeField] public Vector3 MapSize { get; private set; }

    public Rect rt { get; private set; }

    private Tilemap tilemap;

    private void Awake()
    {
        rt = new Rect(transform.position.x - (33 / 2), transform.position.y - (26 / 2), 33, 26);
    }

    private void Start()
    {
        tilemap = GetComponent<Tilemap>();
    }

    public void CheckRightMap(OpenClose openClose)
    {

        switch (openClose)
        {
            case OpenClose.Up:
                Upload.SetActive(false);
                UpWall.SetActive(true);
                break;
            case OpenClose.Right:
                Rload.SetActive(false);
                RWall.SetActive(true);
                break;
            case OpenClose.Left:
                Lload.SetActive(false);
                LWall.SetActive(true);
                break;
            case OpenClose.Down:
                Download.SetActive(false);
                DownWall.SetActive(true);
                break;
            default:
                break;
        }
    }

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {

        var old = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(33, 26));
        Gizmos.color=old;
    }

#endif

}