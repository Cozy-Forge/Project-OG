using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum BlockSize
{
    None,
    OneBlock,
    TwoBlockHorizontal,
    TwoBlockVertical,
    ThreeBlockHorizontal,
    ThreeBlockVertical,
    ThreeBlockL1,
    ThreeBlockL2,
    ThreeBlockL3,
    ThreeBlockL4,
    FourBlockQuadrangle,
}

public class SizeTooltip : MonoBehaviour
{
    private Image[] images;

    private Color defalutColor = Color.black;
    private Color fillColor = Color.yellow;

    private void Awake()
    {
        images = GetComponentsInChildren<Image>();
    }

    public void FillBlock(BlockSize size)
    {
        EraseBlock();
        switch (size)
        {
            case BlockSize.OneBlock:
                FillBlock(5);
                break;
            case BlockSize.TwoBlockHorizontal:
                FillBlock(5);
                FillBlock(6);
                break;
            case BlockSize.TwoBlockVertical:
                FillBlock(2);
                FillBlock(5);
                break;
            case BlockSize.ThreeBlockHorizontal:
                FillBlock(4);
                FillBlock(5);
                FillBlock(6);
                break;
            case BlockSize.ThreeBlockVertical:
                FillBlock(2);
                FillBlock(5);
                FillBlock(8);
                break;
            case BlockSize.ThreeBlockL1:
                FillBlock(2);
                FillBlock(5);
                FillBlock(6);
                break;
            case BlockSize.ThreeBlockL2:
                FillBlock(5);
                FillBlock(6);
                FillBlock(8);
                break;
            case BlockSize.ThreeBlockL3:
                FillBlock(4);
                FillBlock(5);
                FillBlock(8);
                break;
            case BlockSize.ThreeBlockL4:
                FillBlock(2);
                FillBlock(4);
                FillBlock(5);
                break;
            case BlockSize.FourBlockQuadrangle:
                FillBlock(2);
                FillBlock(3);
                FillBlock(5);
                FillBlock(6);
                break;
        }
    }

    private void FillBlock(int idx)
    {
        images[idx-1].color = fillColor;
    }

    private void EraseBlock()
    {
        foreach (var image in images)
            image.color = defalutColor;
    }

    public void ONOFF(bool ison)
    {
        foreach(var image in images)
            image.enabled = ison;
    }
}
