using System;

[Serializable]
public class EnemyTplInfo : BaseTplInfo
{
    public string Name { get; set; }           // ����
    public string Description { get; set; }     // ����

    public int Hp { get; set; }              // ����ֵ

    public int Attack {  get; set; }        // ������

    public int Defend { get; set; }         // ����

    public int Speed { get; set; }          // �ƶ��ٶ�

    public int Exp { get; set; }            // ����

}

[Serializable]
public class EnemyTpl : BaseTpl<EnemyTplInfo>
{

    protected override string TplName => "enemy";
}
