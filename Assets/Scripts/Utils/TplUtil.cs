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

    public static Dictionary<int , ItemTplInfo> GetItemTplDic()
    {
        if (!singletons.ContainsKey(typeof(ItemTpl)))
        {
            ItemTpl tpl = new ItemTpl();
            TextAsset jsonFile = Resources.Load<TextAsset>(tpl.TplPath);
            if (jsonFile == null)
            {
                return null;
            }
            string json = jsonFile.text;
            tpl.List = JsonConvert.DeserializeObject<List<ItemTplInfo>>(json);
            foreach (ItemTplInfo item in tpl.List)
            {
                //Debug.Log(string.Format("item-{0}-{1}", item.ID, item.Name));
                tpl.Dic.Add(item.ID, item);
            }
            singletons[typeof(ItemTpl)] = tpl;
        }

        return ((ItemTpl)singletons[typeof(ItemTpl)]).Dic;
    }

}