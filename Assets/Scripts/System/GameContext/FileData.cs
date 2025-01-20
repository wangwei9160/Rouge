using System;

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