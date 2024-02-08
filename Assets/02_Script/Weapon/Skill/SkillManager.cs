using System;
using System.Collections.Generic;
using UnityEngine;

// ���� Ÿ��
[Flags]
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
public enum WeaponID
{
    None,       // �ƹ��͵� �ƴ�
    Shotgun,    // ����
    Pistol,     // �ǽ���
    Crossbow,   // ����
    Bow,        // Ȱ
    Slongshot,  // ����
    Stone,      // ¯��
    Kunai,      // ����
    Dagger,     // �ܰ�
    Monkey,     // ��Ű���г�
    WaterBang,  // ��ǳ��
    Katana,     // īŸ��
    Branch,     // ��������
    Spear,      // â
    Scratcher,  // ȿ�ڼ�
    Golfclub,   // ����ä
    Sickle,     // ��
    Hammer,     // ��ġ
    Pickaxe,    // ���
    Bat,        // ����
    Brick,      // ����
    Tennis,     // �״Ͻ�����
    Rope,       // ����
    Chain,      // ��罽
}

// �����⺰ ID
public enum GeneratorID
{

}

// Skill 2���� ����Ʈ�� �ν����Ϳ��� �Ⱥ��� �̷��� �ؾ���
[Serializable]
public class Shell
{
    public List<Skill> skillList;
}

public class SkillManager : MonoBehaviour
{

    private static SkillManager instance;
    public static SkillManager Instance => instance;

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

        Debug.Log(2);
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