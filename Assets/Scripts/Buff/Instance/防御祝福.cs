using System;

[Serializable]
public class ����ף�� : BaseBuff
{
    public override int ID => 2;

    public override void OnBeforeHurt(ref int damage)
    {
        base.OnBeforeHurt(ref damage);
        damage = Math.Max(0, damage - Cnt);
    }

}