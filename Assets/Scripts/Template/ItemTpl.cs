using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class ItemTplInfo : BaseTplInfo
{
    public string Name {  get; set; }           // ����
    public int Rank { get; set; }               // ϡ�ж�
    public string Description { get; set; }     // ����
    public int Price { get; set; }              // �۸�
    public int Index { get; set; }              // ��Ӧ��sprite������
    public int MaxOwnCount { get; set; }        // ����������
}

[Serializable]
public class ItemTpl : BaseTpl<ItemTplInfo>
{
    
    protected override string TplName => "item";
}
