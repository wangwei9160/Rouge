using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;


[Serializable]
public class GameData 
{

    public int ShopSlot = 4;                // 商店槽位
    public int WeaponSlot = 6;              // 武器槽位
    public int CurrentWave = 1;             // 当前波次
    public PlayerAttribute playerAttr;      // 角色属性
    public int[] WeaponIDs;                 // 持有武器列表
    public List<int> ItemIDs;               // 持有道具列表
    public List<int> ItemCount;             // 持有道具数量
    public int[] ShopItemID;                // 商店刷新物品id
    public int[] ShopItemType;              // 商店刷新物品type

    private Dictionary<int, int> ItemMap;

    public int curLevel = 1;
    public int curExp = 0;
    public int nextLevelExp = 100;

    public int curHp = 100;

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
        WeaponIDs[0] = 6; // 暂时默认获得一个武器，后续修改为初始自主选择
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