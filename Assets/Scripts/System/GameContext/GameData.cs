using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;


[Serializable]
public class GameData 
{

    public int ShopSlot = 4;                // �̵��λ
    public int WeaponSlot = 6;              // ������λ
    public int CurrentWave = 1;             // ��ǰ����
    public PlayerAttribute playerAttr;      // ��ɫ����
    public int[] WeaponIDs;                 // ���������б�
    public List<int> ItemIDs;               // ���е����б�
    public List<int> ItemCount;             // ���е�������
    public int[] ShopItemID;                // �̵�ˢ����Ʒid
    public int[] ShopItemType;              // �̵�ˢ����Ʒtype

    private Dictionary<int, int> ItemMap;

    public int curLevel = 1;
    public int curExp = 0;
    public int nextLevelExp = 100;

    public int curHp = 100;

    public int money    // ���
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
        }
    }

    public int Cost = 0;    // �ۼƻ���

    public GameData()
    {
        playerAttr = new PlayerAttribute();
        WeaponIDs = new int[WeaponSlot];
        for(int i = 0; i < WeaponSlot; i++)
        {
            WeaponIDs[i] = -1;
        }
        WeaponIDs[0] = 6; // ��ʱĬ�ϻ��һ�������������޸�Ϊ��ʼ����ѡ��
        ShopItemID = new int[ShopSlot];
        ShopItemType = new int[ShopSlot];
        for(int i = 0;i < ShopSlot; i++)
        {
            ShopItemType[i] = ShopItemID[i] = -1;
        }
        ItemMap = new Dictionary<int, int>();
        ItemIDs = new List<int>();
        ItemCount = new List<int>();
    }

    public void OnGetItemByID(int id)
    {
        if (!ItemMap.ContainsKey(id))
        {
            ItemIDs.Add(id);
            ItemCount.Add(0);
            ItemMap.Add(id, ItemIDs.Count - 1);
        }
        ItemCount[ItemMap[id]]++;
    }

    public void GetExp(int exp)
    {
        curExp += exp;
        if(curExp >= nextLevelExp)
        {
            curExp -= nextLevelExp;
            nextLevelExp *= 2;
            curLevel++;
        }
        UIManager.Instance.UpdatePlayerInfoShow();
    }

}