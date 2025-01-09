using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 管理游戏内的物品的创建和存储
/// 通过ID动态创建和获取物品实例
/// </summary>

public static class ItemFactory
{
    /// <summary>
    /// ItemTypeMap     -> 用于存储ID和对应的类型
    /// ItemInstanceMap -> 用于存储ID和对应的实例
    /// </summary>
    private static Dictionary<int, Type> ItemTypeMap = new Dictionary<int, Type>();
    private static Dictionary<int , BaseItem> ItemInstanceMap = new Dictionary<int , BaseItem>();

    static ItemFactory()
    {
        AutoRegisterItems();
    }

    // 通过反射获取BaseItem的所有子类的物品并注册
    private static void AutoRegisterItems()
    {
        var ItemType = typeof(BaseItem);
        var assembly = Assembly.GetAssembly(ItemType);
        var ItemTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(ItemType));

        foreach ( var itemType in ItemTypes )
        {
            var instance = (BaseItem)Activator.CreateInstance(itemType);
            RegisterItem(instance, itemType);
        }
    }

    private static void RegisterItem(BaseItem instance , Type type)
    {
        int id = instance.ID; // 物品的ID
        if (!ItemTypeMap.ContainsKey(id))
        {
            ItemTypeMap[id] = type;
        }
        else
        {
            Debug.LogError(string.Format("道具ID = {0} 重复 , {1} 和 {2} ", id, ItemTypeMap[id], type));
        }
        if (!ItemInstanceMap.ContainsKey(id))
        {
            ItemInstanceMap[id] = instance;
            Debug.Log(string.Format("注册道具{0}", id));
        }

    }

    // 通过ID获取实例
    public static BaseItem GetItemByID(int id)
    {
        if (!ItemInstanceMap.ContainsKey(id))
        {
            if (ItemTypeMap.ContainsKey(id))
            {
                Type type = ItemTypeMap[id];
                ItemInstanceMap[id] = (BaseItem)Activator.CreateInstance(type);
            }else
            {
                Debug.LogError(string.Format("没有找到ID={0}对应的道具", id));
                return null;
            }
        }
        return ItemInstanceMap[id];
    }

}