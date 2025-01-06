using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public GameManager gameManager;
    // 关卡UI
    public Text mCurrentLevel;      // 当前波次
    public Text EnemyCount;         // 敌人计数

    private Action EnemyNumberUpdate;   
    private Action GameOverUI;

    // 武器伤害相关UI
    public Button DamageCountUIButton;
    public GameObject DamageCount;

    private Dictionary<string, int> WeaponDamage = new Dictionary<string, int>();

    // 商店UI
    public GameObject ShopUI;

    void Awake()
    {
        Myinit();
    }

    void Start()
    {
        Myinit();
        GameContext.number = new EnemyNumber(0, 7);
        DamageCountUIButton.onClick.AddListener(() =>
        {
            DamageCount.SetActive(!DamageCount.activeInHierarchy);
        });
    }

    public void Myinit()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();   
        
        ShopUI.SetActive(false);
    }

    void Update()
    {
        if (gameManager.Instance.state == StateID.ShopState)
        {
            ShopUI.SetActive(true);
            mCurrentLevel.gameObject.SetActive(false);
            EnemyCount.gameObject.SetActive(false);
            return;
        }
        if(gameManager.Instance.state == StateID.FightState)
        {
            UpdateCurrentLevel();
            ShopUI.SetActive(false);
            mCurrentLevel.gameObject.SetActive(true);
            EnemyCount.gameObject.SetActive(true);
        }
        if (EnemyNumberUpdate != null)
        {
            EnemyNumberUpdate();
        }
    }

    // 更新武器伤害
    public void UpdateWeaponDamage(string name, int value)
    {
        if (WeaponDamage.ContainsKey(name))
        {
            WeaponDamage[name] += value;
        }else
        {
            WeaponDamage.Add(name, value);
        }
    }

    public Dictionary<string,int> allWeaponDamage()
    {
        return WeaponDamage;
    }

    public void UpdateCurrentLevel()
    {
        mCurrentLevel.text = "第 " + GameContext.CurrentLevel + " 关";
    }

    // 更新敌人数量
    public void UpdateEnemyNumber(int num)
    {
        GameContext.number.res += num;
        EnemyNumberUpdate += UpdateEnemy;
    }

    private void UpdateEnemy()
    {
        EnemyCount.text = GameContext.number.res.ToString() + " / " + GameContext.number.total.ToString();
    }

}