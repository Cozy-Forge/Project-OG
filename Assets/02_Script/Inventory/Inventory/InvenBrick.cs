using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvenBrick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [field: SerializeField] public InventoryObjectData InvenObject { get; private set; }
    public Vector2 InvenPoint { get; set; }

    public Vector3 prevPos;

    private WeaponInventory inventory;

    private bool isDrag;


    protected virtual void Awake()
    {

        InvenObject = Instantiate(InvenObject);
        InvenObject.Init(transform);
        inventory = FindObjectOfType<WeaponInventory>();

    }

    public virtual void Settings()
    {

    }

    public void Setting()
    {
        bool isControllerSetActive = GameManager.Instance.InventoryActive.inven.activeSelf == false;

        if (isControllerSetActive)
        {
            GameManager.Instance.InventoryActive.inven.SetActive(true);
        }

        Settings();

        if (isControllerSetActive)
            GameManager.Instance.InventoryActive.inven.SetActive(false);
    }

    private void Update()
    {

        if (isDrag)
        {

            transform.position = Input.mousePosition;

        }

    }

    public virtual void OnPointerUp(PointerEventData eventData)
    {

        isDrag = false;

        Vector3Int p = Vector3Int.FloorToInt(transform.position / 100);
        var point = inventory.FindInvenPoint(Vector2Int.FloorToInt(transform.position / 100));

        if (point == null)
        {

            Destroy(gameObject);
            return;

        }

        // 잘못 설치 후 돌아갈떄
        // 아이템이 추가가 안됌(addItem에서 걸림)
        // 와이라노...
        // 

        if (inventory.CheckFills(InvenObject.bricks, point.Value))
        {

            inventory.AddItem(InvenObject, point.Value);
            InvenPoint = point.Value;

            transform.position = p * 100 + new Vector3Int(60, 40);

            Setting();
        }
        else
        {

            Vector3Int prevP = Vector3Int.FloorToInt(prevPos / 100);
            var prev = inventory.FindInvenPoint(Vector2Int.FloorToInt(prevPos / 100));

            inventory.AddItem(InvenObject, prev.Value);
            InvenPoint = prev.Value;

            transform.position = prevP * 100 + new Vector3Int(60, 40);

            Setting();
        }



    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {

        prevPos = transform.position;

        isDrag = true;
        inventory.RemoveItem(InvenObject, InvenObject.originPos);

    }
}
