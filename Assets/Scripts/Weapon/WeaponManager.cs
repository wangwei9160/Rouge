
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Weapon
{
    public int type = 0;
    public int damage = 0;
    public float colddown = 0f;
    public float moveSpeed = 0f;
    public string name = "";

    public Weapon(int type , int damage, float colddown, float moveSpeed, string name)
    {
        this.type = type;
        this.damage = damage;
        this.colddown = colddown;
        this.moveSpeed = moveSpeed;
        this.name = name;
    }
}

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager instance;

    public static WeaponManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }

    public Dictionary<int, Weapon> allWeapon = new Dictionary<int, Weapon>();

    private void Awake()
    {
        Myinit();
    }

    private void Start()
    {
        Myinit();
    }

    private void Myinit()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        if (!allWeapon.ContainsKey(1))
        {
            allWeapon.Add(1, new Weapon(1, 10, 2f, 10f, "×Óµ¯"));   // ×Óµ¯
        }
        
    }

    public Weapon getWeapon(int tp)
    {
        if (allWeapon.ContainsKey(tp))
        {
            //Debug.Log("Get " + JsonUtility.ToJson(allWeapon[tp]));
            return allWeapon[tp];
        }
        return null;
    }

    public void resetWeapon(int tp , int dam , float cd , float sd)
    {
        if(allWeapon.ContainsKey(tp))
        {
            //Debug.Log("Update" + JsonUtility.ToJson(allWeapon[tp]));
            allWeapon[tp].type = tp;
            allWeapon[tp].damage = dam;
            allWeapon[tp].colddown = cd;
            allWeapon[tp].moveSpeed = sd;
        }
    }

}
