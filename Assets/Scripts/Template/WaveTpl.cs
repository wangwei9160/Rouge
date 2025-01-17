using System;
using System.Collections.Generic;

[Serializable]
public class WaveTplInfo : BaseTplInfo
{
    public int Wave { get; set; }

    public List<List<int>> EnemyIDs { get; set; }

    public int EnemyWave { get; set; }

    public int Total { get; set; }

}

[Serializable]
public class WaveTpl : BaseTpl<WaveTplInfo>
{

    protected override string TplName => "wave";
}
