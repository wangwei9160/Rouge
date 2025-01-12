using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyNumber
{
    public int res;
    public int total;

    public EnemyNumber(int res, int total)
    {
        this.res = res;
        this.total = total;
    }

    EnemyNumber()
    {
        res = 0;
        total = 7;
    }
}

public static class GameContext
{

    public static bool isGameOver = false;      // 游戏结束标志
    public static bool isLevelFinish = false;   // 当前关卡通关
    public static bool isAward = false;         // 通关奖励

    public static EnemyNumber number;           //  当前关卡敌人数量信息

}