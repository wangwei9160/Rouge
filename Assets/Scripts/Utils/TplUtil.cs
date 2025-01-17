using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class TplUtil
{
    private static Dictionary<Type , object> singletons = new Dictionary<Type , object>();

    //public static ItemTpl GetItemTpl()
    //{
    //    if (!singletons.ContainsKey(typeof(ItemTpl)))
    //    {
    //        ItemTpl tpl = new ItemTpl();
    //        TextAsset jsonFile = Resources.Load<TextAsset>(tpl.TplPath);
    //        if (jsonFile == null)
    //        {
    //            return null;
    //        }
    //        string json = jsonFile.text;
    //        tpl.List = JsonUtility.FromJson<InterListWrapper>(json);
    //        foreach (ItemTplInfo item in tpl.List.all)
    //        {
    //            tpl.Dic.Add(item.ID, item);
    //        }
    //        singletons[typeof(ItemTpl)] = tpl;
    //    }

    //    return singletons[typeof(ItemTpl)] as ItemTpl;
    //}

    public static Dictionary<int, V> GetTplDic<T, V>() where T : BaseTpl<V>, new() where V : BaseTplInfo, new()
    {
        if (!singletons.ContainsKey(typeof(T)))
        {
            T tpl = new T();
            TextAsset jsonFile = Resources.Load<TextAsset>(tpl.TplPath);
            if (jsonFile == null)
            {
                return null;
            }
            string json = jsonFile.text;
            tpl.List = JsonConvert.DeserializeObject<List<V>>(json);
            foreach (V item in tpl.List)
            {
                //Debug.Log(string.Format("item-{0}-{1}", item.ID, item.Name));
                tpl.Dic.Add(item.ID, item);
            }
            singletons[typeof(T)] = tpl;
        }
        return (singletons[typeof(T)] as T).Dic;
    }

    //public static T GetTpl<T , V>() where T : BaseTpl<V> , new() where V : BaseTplInfo , new()
    //{

    //}

    public static Dictionary<int, ItemTplInfo> GetItemTplDic()
    {
        return GetTplDic<ItemTpl, ItemTplInfo>();
    }

    public static Dictionary<int, WeaponTplInfo> GetWeaponTplDic()
    {
        return GetTplDic<WeaponTpl, WeaponTplInfo>();
    }

    public static Dictionary<int, EnemyTplInfo> GetEnemyTplDic()
    {
        return GetTplDic<EnemyTpl, EnemyTplInfo>();
    }

    public static Dictionary<int, WaveTplInfo> GetWaveTplDic()
    {
        return GetTplDic<WaveTpl, WaveTplInfo>();
    }

    

    //public static Dictionary<int , ItemTplInfo> GetItemTplDic()
    //{
    //    if (!singletons.ContainsKey(typeof(ItemTpl)))
    //    {
    //        ItemTpl tpl = new ItemTpl();
    //        TextAsset jsonFile = Resources.Load<TextAsset>(tpl.TplPath);
    //        if (jsonFile == null)
    //        {
    //            return null;
    //        }
    //        string json = jsonFile.text;
    //        tpl.List = JsonConvert.DeserializeObject<List<ItemTplInfo>>(json);
    //        foreach (ItemTplInfo item in tpl.List)
    //        {
    //            //Debug.Log(string.Format("item-{0}-{1}", item.ID, item.Name));
    //            tpl.Dic.Add(item.ID, item);
    //        }
    //        singletons[typeof(ItemTpl)] = tpl;
    //    }

    //    return ((ItemTpl)singletons[typeof(ItemTpl)]).Dic;
    //}


    //public static Dictionary<int, WeaponTplInfo> GetWeaponTplDic()
    //{
    //    if (!singletons.ContainsKey(typeof(WeaponTpl)))
    //    {
    //        WeaponTpl tpl = new WeaponTpl();
    //        TextAsset jsonFile = Resources.Load<TextAsset>(tpl.TplPath);
    //        if (jsonFile == null)
    //        {
    //            return null;
    //        }
    //        string json = jsonFile.text;
    //        tpl.List = JsonConvert.DeserializeObject<List<WeaponTplInfo>>(json);
    //        foreach (WeaponTplInfo item in tpl.List)
    //        {
    //            //Debug.Log(string.Format("item-{0}-{1}", item.ID, item.Name));
    //            tpl.Dic.Add(item.ID, item);
    //        }
    //        singletons[typeof(ItemTpl)] = tpl;
    //    }

    //    return ((WeaponTpl)singletons[typeof(WeaponTpl)]).Dic;
    //}

}