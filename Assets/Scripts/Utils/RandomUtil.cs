
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RandomUtil
{
    public static int RandomInt(int mi , int mx)
    {
        return Random.Range(mi, mx);
    }

    public static float RandomFloat(float range , bool neg = true)
    {
        if(neg) return RandomFloat(-range, range);
        else return RandomFloat(0, range);
    }

    public static float RandomFloat(float left , float right)
    {
        return Random.Range(left, right);
    }

    public static int RandomIndexWithProbablity(float[] p)
    {
        return RandomIndexWithProbablity(p.ToList());
    }

    public static int RandomIndexWithProbablity(List<float> p)
    {
        if (p == null || p.Count == 0)
        {
            Debug.Log("概率分布不能为空");
        }
        float total = p.Sum();
        if(total <= 0)
        {
            Debug.LogError("概率小于0");
        }
        // 归一化
        List<float> probabilities = p.Select(x => x / total).ToList();
        // 转换为前缀数组
        float cal = 0f;
        for(int i = 0; i < probabilities.Count; i++)
        {
            cal += probabilities[i];
            probabilities[i] = cal;
        }
        float rd = RandomFloat(0f, 1f);
        for(int i = 0; i < probabilities.Count; i++)
        {
            if(rd <= probabilities[i])
            {
                return i;
            }
        }
        return -1;
    }

    public static T GetRandomValueInList<T>(List<T> list)
    {
        if(list == null || list.Count == 0)
        {
            throw new System.ArgumentNullException("list is null or empty");
        }
        int idx = RandomInt(0, list.Count);
        return list[idx];
    }

}