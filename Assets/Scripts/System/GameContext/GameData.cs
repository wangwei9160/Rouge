using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;


[Serializable]
public class GameData 
{

    public int ShopSlot = 4;                // 商店槽位
    public int WeaponSlot = 5;              // 武器槽位
    public int CurrentWave = 1;             // 当前波次
    public PlayerAttribute playerAttr;      // 角色属性
    public int[] WeaponIDs;                 //持有武器列表
    public int[] ShopItemID;                // 商店刷新物品id
    public int[] ShopItemType;              // 商店刷新物品type

    public int money    // 金币
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

    public int Cost = 0;    // 累计花销

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