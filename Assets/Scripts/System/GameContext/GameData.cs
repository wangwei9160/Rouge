using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[Serializable]
public class GameData 
{

    public int ShopSlot = 4;                // �̵��λ
    public int WeaponSlot = 5;              // ������λ
    public int CurrentWave = 1;             // ��ǰ����
    public PlayerAttribute playerAttr;      // ��ɫ����
    public int[] WeaponIDs;                 //���������б�
    public int[] ShopItemID;                // �̵�ˢ����Ʒid
    public int[] ShopItemType;              // �̵�ˢ����Ʒtype

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
        ShopItemID = new int[ShopSlot];
        ShopItemType = new int[ShopSlot];
        for(int i = 0;i < ShopSlot; i++)
        {
            ShopItemType[i] = ShopItemID[i] = -1;
        }
    }

}