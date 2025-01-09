using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// ������Ϸ�ڵ���Ʒ�Ĵ����ʹ洢
/// ͨ��ID��̬�����ͻ�ȡ��Ʒʵ��
/// </summary>

public static class ItemFactory
{
    /// <summary>
    /// ItemTypeMap     -> ���ڴ洢ID�Ͷ�Ӧ������
    /// ItemInstanceMap -> ���ڴ洢ID�Ͷ�Ӧ��ʵ��
    /// </summary>
    private static Dictionary<int, Type> ItemTypeMap = new Dictionary<int, Type>();
    private static Dictionary<int , BaseItem> ItemInstanceMap = new Dictionary<int , BaseItem>();

    static ItemFactory()
    {
        AutoRegisterItems();
    }

    // ͨ�������ȡBaseItem�������������Ʒ��ע��
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
        int id = instance.ID; // ��Ʒ��ID
        if (!ItemTypeMap.ContainsKey(id))
        {
            ItemTypeMap[id] = type;
        }
        else
        {
            Debug.LogError(string.Format("����ID = {0} �ظ� , {1} �� {2} ", id, ItemTypeMap[id], type));
        }
        if (!ItemInstanceMap.ContainsKey(id))
        {
            ItemInstanceMap[id] = instance;
            Debug.Log(string.Format("ע�����{0}", id));
        }

    }

    // ͨ��ID��ȡʵ��
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
                Debug.LogError(string.Format("û���ҵ�ID={0}��Ӧ�ĵ���", id));
                return null;
            }
        }
        return ItemInstanceMap[id];
    }

}