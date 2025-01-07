using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BaseTplInfo
{
    public int ID { get; set;}
}



public class BaseTpl<T> where T : BaseTplInfo, new()
{
    protected virtual string TplName { get; }

    public string TplPath
    {
        get
        {
            return TplName;
        }
    }

    public List<T> List { get; set; }

    public Dictionary<int, T> Dic = new Dictionary<int, T>();
    
}