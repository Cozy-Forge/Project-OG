using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class FocusChecker : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    FocusTweening _tweeningScript;

    public UnityEvent ClickEvent; // 클릭되었을 때 실행할 이벤트
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        _tweeningScript.FocusOn();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _tweeningScript.FocusOff();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ClickEvent?.Invoke();
    }
}
