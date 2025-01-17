using System;

[Serializable]
public class EnemyTplInfo : BaseTplInfo
{
    public string Name { get; set; }           // 名称
    public string Description { get; set; }     // 描述

    public int Hp { get; set; }              // 生命值

    public int Attack {  get; set; }        // 攻击力

    public int Defend { get; set; }         // 防御

    public int Speed { get; set; }          // 移动速度

    public int Exp { get; set; }            // 经验

}

[Serializable]
public class EnemyTpl : BaseTpl<EnemyTplInfo>
{

    protected override string TplName => "enemy";
}
