using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemLinkGuideQuest : GuideQuest
{
    public int linkPower;

    public override bool IsQuestComplete()
    {
        return CheckItemLink();
    }

    private bool CheckItemLink()
    {
        // �� �κп��� ���� - ����� - ��ų����
        // ���� - ��ų�� ����� �⺻���� 1�̶�� �� ��
        // linkPower��ŭ ����Ǿ� ������ true ��ȯ
        // ������ �� �Ǿ��ְų�, linkPower��ŭ ����Ǿ����� �ʴٸ� false ��ȯ
        return true; 
    }
}
