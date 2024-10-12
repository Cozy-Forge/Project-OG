using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public struct FocusImageInfo
{
    public Image Image;
    public RectTransform RectTransform;
    public Vector2 BaseLocalPos;
    public Color BaseColor;
}


public class FocusTweening : MonoBehaviour
{
    [SerializeField]
    float duration;
    [SerializeField]
    Vector2 moveValue;
    [SerializeField]
    Color FocusColor;

    FocusImageInfo focusImageInfo;
    Image backgroundImage;

    Sequence sequence;

    private void Awake()
    {
        RectTransform focusImgTrm = transform.Find("BG/FocusImg").GetComponent<RectTransform>();

        focusImageInfo = new()
        {
            Image = focusImgTrm.GetComponent<Image>(),
            RectTransform = focusImgTrm,
            BaseLocalPos = focusImgTrm.anchoredPosition,
        };
        focusImageInfo.BaseColor = focusImageInfo.Image.color;

        backgroundImage = transform.Find("BG").GetComponent<Image>();
    }

    public void FocusOn()
    {
        //if(sequence != null)
        //{
        //    sequence.Kill();
        //}
        //sequence = DOTween.Sequence();

        focusImageInfo.RectTransform.DOAnchorPos(focusImageInfo.BaseLocalPos + moveValue, duration);
        focusImageInfo.Image.DOColor(FocusColor, duration);
      
        backgroundImage.DOFade(0.16f, duration);
    }

    public void FocusOff()
    {
        //DOTween.KillAll();

        focusImageInfo.RectTransform.DOAnchorPos(focusImageInfo.BaseLocalPos, 0f);
        focusImageInfo.Image.DOColor(focusImageInfo.BaseColor, duration);
       
        backgroundImage.DOFade(1f, duration);
    }
}
