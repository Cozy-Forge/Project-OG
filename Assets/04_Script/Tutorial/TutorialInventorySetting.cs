using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialInventorySetting : MonoBehaviour
{
    [SerializeField] Item[] items;// 1 4 3

    Vector2Int[] points = { 
        new Vector2Int(1, 1), 
        new Vector2Int(3, 1), 
        new Vector2Int(5, 2),
        new Vector2Int(3, 2),
        new Vector2Int(3, 3),
        new Vector2Int(5, 3)};

    WeaponInventory inventory;

    private void Awake()
    {
        inventory = GameManager.Instance.Inventory;
    }

    public void SettingInven()
    {
        for(int index = 0; index < items.Length; index++)
        {
            Item item = Instantiate(items[index]);
            item.OnInteract();

            InventoryObjectData InvenObject = inventory.GetContainer()[inventory.GetContainer().Count - 1];
            WeaponBrick weapon = InvenObject.invenBrick.GetComponent<WeaponBrick>();
            if(weapon != null)
            {
                weapon.weaponController.RemoveWeapon(weapon.weaponGuid);
                Destroy(weapon.Weapon.gameObject);
            }
            inventory.RemoveItem(InvenObject, InvenObject.originPos);
            InvenObject.invenBrick.SettingPosToIndex(points[index].x, points[index].y);
        }
    }
}
