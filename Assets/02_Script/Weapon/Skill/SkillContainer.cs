using System;
using System.Collections.Generic;
using UnityEngine;

// �����Ⱑ ������ �ִ°�
// ǥ�� Ÿ��, ������ ID(��ų ����)

// ���Ⱑ ������ �ִ°�
// �ڱ� Ÿ��, �ڱ� ID

// ���� Ÿ��
[Flags]
[Serializable]
public enum WeaponType
{
    Gun = 1 << 0,       // �ѱ��
    Bow = 1 << 1,       // Ȱ
    Throw = 1 << 2,     // ��ô��
    Sword = 1 << 3,     // ��
    Spear = 1 << 4,     // â
    Sickle = 1 << 5,    // ��
    Blunt = 1 << 6,     // �б��
    Whip = 1 << 7,      // ä���
}

// ���⺰ ID
[Serializable]
public enum WeaponID
{
    None,       // �ƹ��͵� �ƴ�
    Shotgun,    // ����
    Pistol,     // �ǽ���
    Crossbow,   // ����
    Bow,        // Ȱ
    Slingshot,  // ����
    Stone,      // ¯��
    Kunai,      // ������
    Monkey,     // ��Ű���г�
    WaterBang,  // ��ǳ��
    Bomb,       // ��ź
    Katana,     // īŸ��
    Branch,     // ��������
    Tennis,     // �״Ͻ�����
    Spear,      // â
    Cane,       // ������
    Golfclub,   // ����ä
    Sickle,     // ��
    Hammer,     // ��ġ
    Pickaxe,    // ���
    Bat,        // ����
    Rope,       // ����
    Chain,      // ��罽
}

// �����⺰ ID
[Serializable]
public enum GeneratorID
{
    None,       // ����
    Double,     // ����
    Fire,       // ��
    Water,      // ��
}

// Skill 2���� ����Ʈ�� �ν����Ϳ��� �Ⱥ��� �̷��� �ؾ���
[Serializable]
public class Shell
{
    public List<Skill> skillList;
}

public class SkillContainer : MonoBehaviour
{

    private static SkillContainer instance;
    public static SkillContainer Instance => instance;

    [SerializeField] List<Shell> weaponList;


    public Transform player;

    private void Awake()
    {

        if (instance != null)
        {

            Debug.LogError("Multiple SkillManager is running");
            Destroy(this);

        }

        instance = this;

    }

    /// <summary>
    /// ID�� �´� ��ų ��� ��
    /// </summary>
    /// <param name="i">���� ID</param>
    /// <param name="j">������ ID</param>
    /// <returns></returns>
    public Skill GetSKill(int i, int j)
    {

        if (weaponList.Count > i && weaponList[i].skillList.Count > j)
        {

            if (weaponList[i] != null && weaponList[j] != null)
            {

                return weaponList[i].skillList[j];

            }

        }

        Debug.LogError($"Skill Doesn't exist in SkillList : {i}, {j} ");
        return null;

    }
}