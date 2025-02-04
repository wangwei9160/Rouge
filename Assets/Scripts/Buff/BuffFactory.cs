using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

/// <summary>
/// 管理游戏内的物品的创建和存储
/// 通过ID动态创建和获取物品实例
/// </summary>

public static class BuffFactory
{
    private static Dictionary<int, Type> TypeMap = new Dictionary<int, Type>();
    private static Dictionary<int, BaseBuff> InstanceMap = new Dictionary<int, BaseBuff>();

    static BuffFactory()
    {
        AutoRegisterBuffs();
    }

    // 通过反射获取BaseBuff的所有子类的物品并注册
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
        int id = instance.ID; // 物品的ID
        if (!TypeMap.ContainsKey(id))
        {
            TypeMap[id] = type;
        }
        else
        {
            Debug.LogError(string.Format("道具Buff = {0} 重复 , {1} 和 {2} ", id, TypeMap[id], type));
        }
        if (!InstanceMap.ContainsKey(id))
        {
            InstanceMap[id] = instance;
            Debug.Log(string.Format("注册Buff {0}", id ));
        }

    }

    // 通过ID获取实例
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
                Debug.LogError(string.Format("没有找到ID={0}对应的Buff", id));
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

//    // 通过反射获取BaseBuff的所有子类的物品并注册
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
//        int id = instance.ID; // 物品的ID
//        if (!TypeMap.ContainsKey(id))
//        {
//            TypeMap[id] = type;
//        }
//        else
//        {
//            Debug.LogError(string.Format("道具ID = {0} 重复 , {1} 和 {2} ", id, TypeMap[id], type));
//        }
//        if (!InstanceMap.ContainsKey(id))
//        {
//            InstanceMap[id] = instance;
//            Debug.Log(string.Format("注册道具{0}", id));
//        }

//    }

//    // 通过ID获取实例
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
//                Debug.LogError(string.Format("没有找到ID={0}对应的道具", id));
//                return null;
//            }
//        }
//        return InstanceMap[id];
//    }
//}
