using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class DamageData
{

    public DamageInfo Damage { get; private set; }

    public Transform pos;

    public DamageData(DamageInfo damage , Transform trans)
    {
        Damage = damage;
        pos = trans;
    }

    public void SetDamageValue(DamageInfo damage)
    {
        Damage = damage;
    }
}

public class ShowDamageAction
{
    public Action<DamageData> action;
    public DamageData data;
}

public class DamageUIManager : Singleton<DamageUIManager> 
{

    public GameObject damgeUI;
    // 用于显示伤害的事件
    public Queue<ShowDamageAction> dataQueue = new Queue<ShowDamageAction>();

    void Update()
    {
        if(dataQueue.Count > 0)
        {
            var dt = dataQueue.Dequeue();
            dt.action.Invoke(dt.data);
        }
    }

    
    private void CreateDamageText(DamageData _data)
    {
        var damUI = GameObject.Instantiate(damgeUI, gameObject.transform);
        damUI.GetComponent<DamgeUI>().SetInfo(_data);
    }

    // 提供给外部用于创建一个伤害显示的接口
    public void ShowDamgeText(Transform pos , DamageInfo info )
    {
        ShowDamageAction tmp = new ShowDamageAction();
        tmp.action = CreateDamageText;
        tmp.data = new DamageData(info, pos);
        dataQueue.Enqueue(tmp);
    }

}