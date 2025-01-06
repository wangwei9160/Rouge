using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    private GameManager instance;
    public StateID state = StateID.FightState;

    public UIManager uiInfoManager;             // 界面UI
    public EnemyGenerator enemyGenerator;       // 敌人生成器
    public DamageUIManager damageUIManager;     // 跳字伤害显示UI
    public WeaponManager weaponManager;         // 装备管理

    public GameManager Instance
    {
        get { return instance; }
        private set { instance = value; }
    }


    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        weaponManager = GameObject.Find("WeaponManager").GetComponent<WeaponManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameContext.number != null)
        {
            //Debug.Log(GameContext.number.res + " " + GameContext.number.total);
            if(state == StateID.FightState)
            {
                if (GameContext.number.res == GameContext.number.total)
                {
                    TransState(StateID.ShopState);
                }
            }
        }
    }

    public void TransState(StateID newState)
    {
        if(state == newState)
        {
            Debug.Log(string.Format("Current State is {0:D}" ,state));
        }else
        {
            Debug.Log(string.Format("State From {0:D} -> {1:D}", state, newState));
            state = newState;
        }
    }

    // 获取一个最近的敌人的接口
    public GameObject GetEnemyOne()
    {
        return enemyGenerator.GetEnemy();
    }

}
