using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class UIManager : ManagerBase<UIManager>
{
    public Transform canvas;
    // 关卡UI
    public Text CurrentWave;                    // 当前波次
    public Text CurrentWaveEnemyCount;         // 敌人计数

    private Action EnemyNumberUpdate;   
    private Action GameOverUI;

    // 武器伤害相关UI
    public Button DamageCountUIButton;
    public GameObject DamageCount;

    private Dictionary<string, int> WeaponDamage = new Dictionary<string, int>();

    // 商店UI
    public GameObject ShopUI;
    public bool[] isLock = new bool[4];

    // 角色信息显示
    public GameObject PlayerInfoShow;
    

    void Start()
    {
        ShopUI.SetActive(false);
        GameContext.number = new EnemyNumber(0, 7);
        DamageCountUIButton.onClick.AddListener(() =>
        {
            DamageCount.SetActive(!DamageCount.activeInHierarchy);
        });
    }


    void Update()
    {
        if (GameManager.Instance.state == StateID.ShopState)
        {
            ShopUI.SetActive(true);
            PlayerInfoShow.gameObject.SetActive(false);
            CurrentWave.gameObject.SetActive(false);
            CurrentWaveEnemyCount.gameObject.SetActive(false);
            return;
        }
        if(GameManager.Instance.state == StateID.FightState)
        {
            UpdateCurrentLevel();
            ShopUI.SetActive(false);
            PlayerInfoShow.gameObject.SetActive(true);
            UpdatePlayerInfoShow();
            CurrentWave.gameObject.SetActive(true);
            CurrentWaveEnemyCount.gameObject.SetActive(true);
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
        CurrentWave.text = "第 " + GameManager.Instance.gameData.CurrentWave + " 关";
    }

    // 更新敌人数量
    public void UpdateEnemyNumber(int num)
    {
        GameContext.number.res += num;
        EnemyNumberUpdate += UpdateEnemy;
    }

    private void UpdateEnemy()
    {
        CurrentWaveEnemyCount.text = GameContext.number.res.ToString() + " / " + GameContext.number.total.ToString();
    }

    public void UpdatePlayerInfoShow()
    {
        PlayerInfoShow.GetComponent<PlayerInfoUI>().UpdateExp(GameManager.Instance.gameData.curLevel , GameManager.Instance.gameData.curExp , GameManager.Instance.gameData.nextLevelExp);
    }

}