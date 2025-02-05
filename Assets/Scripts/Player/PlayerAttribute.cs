using System;
using UnityEngine;

[Serializable]
public class PlayerAttribute
{
    public float maxHp = 100;           // ��������
    public float attackPower = 5;      // ������
    public float attackSpeed = 100;     // ����
    public float defend = 5;            // ����
    public float magicDefend = 5;       // ��������
    public float CriticalHitRate = 5;  // ������
    public float CriticalDamage = 150;  // �����˺�
    public float luck = 5;              // ����ֵ
    public int Revenues = 50;           // �غ�����

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

    public float �������ֵ
    {
        get { return maxHp; }
        set { maxHp = ClampValue(value, float.Epsilon, float.MaxValue); }
    }

    public float ������
    {
        get { return attackPower; }
        set { attackPower = ClampValue(value , 0 , float.MaxValue); }
    }

    public float ����
    {
        get { return attackSpeed; }
        set { attackSpeed = ClampValue(value, float.Epsilon, 250f); }
    }

    public float ����
    {
        get { return defend; }
        set { defend = ClampValue(value, 0, float.MaxValue); }
    }

    public float ����
    {
        get { return magicDefend; }
        set { magicDefend = ClampValue(value, 0, float.MaxValue); }
    }

    public float ������
    {
        get { return CriticalHitRate; }
        set { CriticalHitRate = ClampValue(value, 0f , float.MaxValue); }
    }

    public float �����˺�
    {
        get { return CriticalDamage; }
        set { CriticalDamage = ClampValue(value, 100f, float.MaxValue); }
    }

    public float ����
    {
        get { return luck; }
        set { luck = value; }
    }

    public int �غ�����
    {
        get { return Revenues; }
        set { Revenues = Mathf.Max(0, value); }
    }

    private float ClampValue(float val , float min ,float max)
    {
        return Mathf.Clamp(val, min, max);
    }

}