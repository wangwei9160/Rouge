using System;

[Serializable]
public class 回合收入祝福 : BaseBuff
{
    public override int ID => 3;

    public override void OnWaveEnd()
    {
        base.OnWaveEnd();
        GameManager.Instance.OnMoneyChange(50 * Cnt);
    }

}