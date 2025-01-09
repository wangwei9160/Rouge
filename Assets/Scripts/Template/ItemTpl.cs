using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemTplInfo : BaseTplInfo
{
    public string Name {  get; set; }           // 名称
    public int Rank { get; set; }               // 稀有度
    public string Description { get; set; }     // 描述
    public int Price { get; set; }              // 价格
    public int Index { get; set; }              // 对应到sprite的索引
    public int MaxOwnCount { get; set; }        // 最大持有限制
}

[Serializable]
public class ItemTpl : BaseTpl<ItemTplInfo>
{
    
    protected override string TplName => "item";
}
