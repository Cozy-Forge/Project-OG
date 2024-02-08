using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    // normal item probabilities are the remaining probabilities other than other item probabilities
    [Header("percent")]
    [SerializeField] private float _rareProbability;     // rare item probability
    [SerializeField] private float _epicProbability;     // epic item probability
    [SerializeField] private float _legendProbability;   // legend item probability

    [Header("Info")]
    [SerializeField]
    private ItemInfoListSO _itemList;

    private Dictionary<ItemRate, List<ItemInfoSO>> _rateItems = new Dictionary<ItemRate, List<ItemInfoSO>>();

    [Header("Chest Sprite")]
    [SerializeField] private Sprite _openSprite;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        if(_itemList == null)
        {
            Debug.LogError("ItemSOList is null");
            return;
        }

        SetRateItems(_itemList.ItemInfoList.ToArray());
    }

    private void SetRateItems(ItemInfoSO[] itemInfoSOs)
    {
        _rateItems.Clear();
        _rateItems[ItemRate.NORMAL] = new List<ItemInfoSO>();
        _rateItems[ItemRate.RARE] = new List<ItemInfoSO>();
        _rateItems[ItemRate.EPIC] = new List<ItemInfoSO>();
        _rateItems[ItemRate.LEGEND] = new List<ItemInfoSO>();

        foreach(ItemInfoSO item in itemInfoSOs)
        {
            _rateItems[item.Rate].Add(item);
        }
    }

    private void Open()
    {
        // ���� ��������Ʈ ����
        _spriteRenderer.sprite = _openSprite;

        ItemInfoSO item = RandomItem();
        // ������ ��ȯ�ε�.. ������ �������� ���µ�?
    }

    private ItemInfoSO RandomItem()
    {
        // ��� ���
        float percent = Random.Range(0f, 100f); // 0 ~ 100
        ItemRate rate = ItemRate.NORMAL;

        if(percent <= _legendProbability)
        {
            rate = ItemRate.LEGEND;

        }
        else if (percent <= _legendProbability + _epicProbability)
        {
            rate = ItemRate.EPIC;

        }
        else if (percent <= _legendProbability + _epicProbability + _rareProbability)
        {
            rate = ItemRate.RARE;

        }

        ItemInfoSO iteminfo = _rateItems[rate][Random.Range(0, _rateItems[rate].Count)];
        return iteminfo;
    }
}
