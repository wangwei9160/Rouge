using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemTplInfo : BaseTplInfo
{
    public string Name {  get; set; }
    public int Rank { get; set; }
    public string Description { get; set; }
    public int Price { get; set; }
    public int MaxOwnCount { get; set; }
}

[Serializable]
public class ItemTpl : BaseTpl<ItemTplInfo>
{
    
    protected override string TplName => "item";
}
