using System;
using System.Collections.Generic;

[Serializable]
public class FileShowData
{
    public string saveTime;
    public int level;
    public int wave;
    public int money;
    public int playTime;

    public FileShowData(GameData data)
    {
        saveTime = DateTime.Now.ToString("yyyy/mm/dd hh:mm:ss");
        level = data.curLevel;
        wave = data.CurrentWave;
        money = data.money;
        playTime = 0;
    }

    public void Set(GameData data)
    {
        level = data.curLevel;
        wave = data.CurrentWave;
        money = data.money;
        playTime = (int)data.playTime;
    }

}

[Serializable]
public class RootNode
{
    public List<int> root;

    public RootNode(List<int> l)
    {
        root = l;
    }
}

[Serializable]
public class TalentData
{
    public int Total;                  // ID
    public int MaxNumber;              // �츳��������
    public List<int> Cnt;              // �Ѿ����õ�����
    public List<int> MaxCnt;           // ������õ�����
    public List<RootNode> Roots;       // ���ڵ�

    public TalentData()
    {
        Total = 0;
        MaxNumber = 16;
        Cnt = new List<int> { 0, 0, 0, 0 };
        MaxCnt = new List<int> { 5, 5, 5, 1 };
        Roots = new List<RootNode>
        {
            new RootNode(new(){-1}),
            new RootNode(new(){0}),
            new RootNode(new(){0}),
            new RootNode(new(){1,2}),
        };
    }

}