using System;
using UnityEngine;

[Serializable]
public class ÉËº¦½á¹û×£¸£ : BaseBuff
{
    public override int ID => 1;

    public override void OnBeforeDamage(ref int damage)
    {
        base.OnBeforeDamage(ref damage);
        int add = (int)(damage * 0.1f * Cnt);
        Debug.Log(damage);
        damage += add;
        Debug.Log(string.Format("{0} - {0}", damage, add));
    }

}