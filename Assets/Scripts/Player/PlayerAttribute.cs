using System;
using UnityEngine;

[Serializable]
public class PlayerAttribute
{
    public float maxHp = 100;           // 生命上限
    public float attackPower = 5;      // 攻击力
    public float attackSpeed = 100;     // 攻速
    public float defend = 5;            // 防御
    public float magicDefend = 5;       // 法术抗性
    public float CriticalHitRate = 5;  // 暴击率
    public float CriticalDamage = 150;  // 暴击伤害
    public float luck = 5;              // 幸运值
    public int Revenues = 50;           // 回合收入

    public PlayerAttribute()
    {
        maxHp = 100;
        attackPower = 5;
        attackSpeed = 100;
        defend = 5;
        magicDefend = 5;
        CriticalHitRate= 5;
        CriticalDamage = 150;
        luck = 5;
        Revenues = 50;
    }

    public float 最大生命值
    {
        get { return maxHp; }
        set { maxHp = ClampValue(value, float.Epsilon, float.MaxValue); }
    }

    public float 攻击力
    {
        get { return attackPower; }
        set { attackPower = ClampValue(value , 0 , float.MaxValue); }
    }

    public float 攻速
    {
        get { return attackSpeed; }
        set { attackSpeed = ClampValue(value, float.Epsilon, 250f); }
    }

    public float 防御
    {
        get { return defend; }
        set { defend = ClampValue(value, 0, float.MaxValue); }
    }

    public float 法抗
    {
        get { return magicDefend; }
        set { magicDefend = ClampValue(value, 0, float.MaxValue); }
    }

    public float 暴击率
    {
        get { return CriticalHitRate; }
        set { CriticalHitRate = ClampValue(value, 0f , float.MaxValue); }
    }

    public float 暴击伤害
    {
        get { return CriticalDamage; }
        set { CriticalDamage = ClampValue(value, 100f, float.MaxValue); }
    }

    public float 幸运
    {
        get { return luck; }
        set { luck = value; }
    }

    public int 回合收入
    {
        get { return Revenues; }
        set { Revenues = Mathf.Max(0, value); }
    }

    private float ClampValue(float val , float min ,float max)
    {
        return Mathf.Clamp(val, min, max);
    }

}