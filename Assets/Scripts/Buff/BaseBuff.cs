using System;
using UnityEngine;

[Serializable]
public class BaseBuff
{
    public virtual int ID { get; set; }

    public BuffTplInfo Info { get { return TplUtil.GetBuffTplDic()[ID]; } }

    // ��ʱ��
    public float clkDuration;
    public int clkTime;

    public int Cnt; // ��������

    public BaseBuff()
    {
        clkDuration = Info.Duration;
        Cnt = 0;
        clkTime = 0;
    }

    // �������
    public virtual void OnAdd()
    {
        Debug.Log("OnAdd " + Info.Name);
        Cnt++;
    }

    public virtual void OnRemove()
    {
        Debug.Log("OnRemove " + Info.Name);
    }

    public virtual void OnBeforeWaveStart()
    {
        Debug.Log("OnBeforeWaveStart " + Info.Name);
    }

    public virtual void OnWaveEnd()
    {
        Debug.Log("OnWaveEnd " + Info.Name);
    }

    public virtual void OnBeforeDamage(ref DamageInfo damage)
    {
        Debug.Log("OnBeforeDamage " + Info.Name);
    }

    public virtual void OnBeforeHurt(ref DamageInfo damage)
    {
        Debug.Log("OnBeforeHurt " + Info.Name);
    }

    public virtual void OnHurt()
    {
        Debug.Log("OnHurt " + Info.Name);
    }
}