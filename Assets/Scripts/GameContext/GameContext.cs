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
    public static bool isGameOver = false;      // ��Ϸ������־
    public static bool isAward = false;         // ͨ�ؽ���

    public static int CurrentLevel = 1;         // ��ǰ�ؿ���Ϣ

    public static EnemyNumber number;           //  ��ǰ�ؿ�����������Ϣ

}