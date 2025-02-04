using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// ������Ϸ�ڵ���Ʒ�Ĵ����ʹ洢
/// ͨ��ID��̬�����ͻ�ȡ��Ʒʵ��
/// </summary>

public static class BuffFactory
{
    private static Dictionary<int, Type> TypeMap = new Dictionary<int, Type>();
    private static Dictionary<int, BaseBuff> InstanceMap = new Dictionary<int, BaseBuff>();

    static BuffFactory()
    {
        AutoRegisterBuffs();
    }

    // ͨ�������ȡBaseBuff�������������Ʒ��ע��
    private static void AutoRegisterBuffs()
    {
        var ItemType = typeof(BaseBuff);
        var assembly = Assembly.GetAssembly(ItemType);
        var ItemTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(ItemType));

        foreach (var itemType in ItemTypes)
        {
            var instance = (BaseBuff)Activator.CreateInstance(itemType);
            RegisterBuff(instance, itemType);
        }
    }

    private static void RegisterBuff(BaseBuff instance, Type type)
    {
        int id = instance.ID; // ��Ʒ��ID
        if (!TypeMap.ContainsKey(id))
        {
            TypeMap[id] = type;
        }
        else
        {
            Debug.LogError(string.Format("����Buff = {0} �ظ� , {1} �� {2} ", id, TypeMap[id], type));
        }
        if (!InstanceMap.ContainsKey(id))
        {
            InstanceMap[id] = instance;
            Debug.Log(string.Format("ע��Buff {0}", id ));
        }

    }

    // ͨ��ID��ȡʵ��
    public static BaseBuff GetBuffByID(int id)
    {
        if (!InstanceMap.ContainsKey(id))
        {
            if (TypeMap.ContainsKey(id))
            {
                Type type = TypeMap[id];
                InstanceMap[id] = (BaseBuff)Activator.CreateInstance(type);
            }
            else
            {
                Debug.LogError(string.Format("û���ҵ�ID={0}��Ӧ��Buff", id));
                return null;
            }
        }
        return InstanceMap[id];
    }

}

//public static class Factory<T>
//{
//    private static Dictionary<int, Type> TypeMap = new Dictionary<int, Type>();
//    private static Dictionary<int, T> InstanceMap = new Dictionary<int, T>();

//    static Factory()
//    {
//        AutoRegisterBuffs();
//    }

//    // ͨ�������ȡBaseBuff�������������Ʒ��ע��
//    private static void AutoRegisterBuffs()
//    {
//        var ItemType = typeof(T);
//        var assembly = Assembly.GetAssembly(ItemType);
//        var ItemTypes = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(ItemType));

//        foreach (var itemType in ItemTypes)
//        {
//            var instance = (T)Activator.CreateInstance(itemType);
//            RegisterItem(instance, itemType);
//        }
//    }

//    private static void RegisterItem(T instance, Type type)
//    {
//        int id = instance.ID; // ��Ʒ��ID
//        if (!TypeMap.ContainsKey(id))
//        {
//            TypeMap[id] = type;
//        }
//        else
//        {
//            Debug.LogError(string.Format("����ID = {0} �ظ� , {1} �� {2} ", id, TypeMap[id], type));
//        }
//        if (!InstanceMap.ContainsKey(id))
//        {
//            InstanceMap[id] = instance;
//            Debug.Log(string.Format("ע�����{0}", id));
//        }

//    }

//    // ͨ��ID��ȡʵ��
//    public static T GetItemByID(int id)
//    {
//        if (!InstanceMap.ContainsKey(id))
//        {
//            if (TypeMap.ContainsKey(id))
//            {
//                Type type = TypeMap[id];
//                InstanceMap[id] = (T)Activator.CreateInstance(type);
//            }
//            else
//            {
//                Debug.LogError(string.Format("û���ҵ�ID={0}��Ӧ�ĵ���", id));
//                return null;
//            }
//        }
//        return InstanceMap[id];
//    }
//}
