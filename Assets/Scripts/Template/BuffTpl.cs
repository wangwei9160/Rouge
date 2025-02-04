// BuffTpl
using System;

[Serializable]
public class BuffTplInfo : BaseTplInfo
{
    public string Name { get; set; }
    public string Description { get; set; } 
    public float Duration { get; set; }
}

[Serializable]
public class BuffTpl : BaseTpl<BuffTplInfo>
{

    protected override string TplName => "buff";
}
