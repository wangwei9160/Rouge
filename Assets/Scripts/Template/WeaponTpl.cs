using System;

[Serializable]
public class WeaponTplInfo : BaseTplInfo
{
    public string Name { get; set; }            // ����
    public int Rank { get; set; }               // ϡ�ж�
    public string Description { get; set; }     // ����
    public int Price { get; set; }              // �۸�
    public int Index { get; set; }              // ��Ӧ��sprite������

    public float Attack { get; set; }           // ������

    public float AttackSpeed { get; set; }      // �����ٶ�

    public float AttackRange { get; set; }      // ������Χ

    public int Next {  get; set; }              // ��һ�׶ε�����

}

[Serializable]
public class WeaponTpl : BaseTpl<WeaponTplInfo>
{

    protected override string TplName => "weapon";
}
