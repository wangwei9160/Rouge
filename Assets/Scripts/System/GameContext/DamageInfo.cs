using System;
using UnityEngine;

[Serializable]
public class DamageInfo
{
    //public GameObject source;
    //public GameObject target;

    public float damage;

    public float attack;

    public float CriticalHitRate;       // ±©»÷¸ÅÂÊ
    public float CriticalDamage;        // ±©»÷ÉËº¦
    
    public bool isCritical = false;     // ÊÇ·ñ±©»÷

    public float bonus = 100f;          // ÉËº¦¼Ó³É
    public float reduction = 0f;    

    public float Value
    {
        get
        {
            //Debug.Log(this.ToString());
            var val = (damage + attack);
            if (isCritical)
            {
                val *= (CriticalDamage / 100.0f);
            }

            val *= (bonus / 100.0f);
            val -= reduction;
            return val;
        }
    }

    public DamageInfo()
    {

    }

    public DamageInfo(float damage , float attack , float rate , float cDamage , float bonus = 100.0f , float reduction = 0f)
    {
        this.damage = damage;
        this.attack = attack;
        this.CriticalHitRate = rate;
        this.CriticalDamage = cDamage;
        this.bonus = bonus;
        this.reduction = reduction;
        Set();
    }

    public void Set()
    {
        isCritical = RandomUtil.IsProbabilityMet(CriticalHitRate / 100.0f);
    }

}