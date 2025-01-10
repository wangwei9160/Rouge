using System;

[Serializable]
public class WeaponTplInfo : BaseTplInfo
{
    public string Name { get; set; }            // 名称
    public int Rank { get; set; }               // 稀有度
    public string Description { get; set; }     // 描述
    public int Price { get; set; }              // 价格
    public int Index { get; set; }              // 对应到sprite的索引

    public float Attack { get; set; }           // 攻击力

    public float AttackSpeed { get; set; }      // 攻击速度

    public float AttackRange { get; set; }      // 攻击范围

    public int Next {  get; set; }              // 下一阶段的样子

}

[Serializable]
public class WeaponTpl : BaseTpl<WeaponTplInfo>
{

    protected override string TplName => "weapon";
}
