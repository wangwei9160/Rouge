using System.Collections.Generic;

public class Constants
{
    public static string PLAYERPREFES = "data-";
    public static string TALENTPLAYERPREFS = "TALENT";

    public static int FILEDATA_SLOT_MAX = 10;

    public static int ENDLESS_WAVE = 20;
    public static int[] NORLMAL_WAVE = new int[] { 21, 22, 23 };
    public static int ONE_BOSS_WAVE = 24;

    // 商店刷新武器和道具的概率
    public static List<LevelProbability> WeaponOrItemProbability = new List<LevelProbability>
    {
        // 武器 道具
        new LevelProbability(3 , new float[]{60f , 40f}),
        new LevelProbability(5 , new float[]{55f , 45f}),
        new LevelProbability(7 , new float[]{50f , 50f}),
        new LevelProbability(9 , new float[]{45f , 55f}),
        new LevelProbability(11 , new float[]{40f , 60f}),
        new LevelProbability(13 , new float[]{35f , 65f}),
        new LevelProbability(15 , new float[]{30f , 70f}),
        new LevelProbability(17 , new float[]{25f , 75f}),
        new LevelProbability(20 , new float[]{20f , 80f}),
        new LevelProbability(21 , new float[]{10f , 90f}),
    };

    public static List<LevelProbability> RankTypeProbability = new List<LevelProbability>
    {
        // Base, Common, Rare, Epic, legendary
        new LevelProbability(1 , new float[]{100f , 0f , 0f , 0f , 0f}),
        new LevelProbability(2 , new float[]{90f , 9f , 1f , 0f , 0f}),
        new LevelProbability(3 , new float[]{80f , 15f , 5f , 0f , 0f}),
        new LevelProbability(4 , new float[]{70f , 20f , 10f , 0f , 0f}),
        new LevelProbability(5 , new float[]{59f , 30f , 10f , 1f , 0f}),
        new LevelProbability(6 , new float[]{45f , 35f , 16f , 4f , 0f}),
        new LevelProbability(7 , new float[]{25f , 40f , 27f , 7f , 1f}),
        new LevelProbability(8 , new float[]{10f , 25f , 35f , 20f , 10f}),
    };

    public static float[] GetRankTypeProbabilityByLevel(int level)
    {
        for(int i = 0; i < level; i++)
        {
            if(level <= RankTypeProbability[i].level)
            {
                return RankTypeProbability[i].Probability;
            }
        }
        // 默认返回最高等级的概率
        return RankTypeProbability[RankTypeProbability.Count - 1].Probability;
    }

    public static float[] GetWeaponOrItemProbabilityByLevel(int level)
    {
        for (int i = 0; i < level; i++)
        {
            if (level <= WeaponOrItemProbability[i].level)
            {
                return WeaponOrItemProbability[i].Probability;
            }
        }
        // 默认返回最高等级的概率
        return WeaponOrItemProbability[WeaponOrItemProbability.Count - 1].Probability;
    }

}

public class LevelProbability
{
    public int level {  get; set; }
    public float[] Probability { get; set; }

    public LevelProbability(int level, float[] probability)
    {
        this.level = level;
        Probability = probability;
    }   
}