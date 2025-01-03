using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class UIManager : MonoBehaviour
{
    public TMP_Text CurrentLevel;
    public TMP_Text EnemyTotal;

    private Action EnemyNumberUpdate;
    private Action GameOverUI;

    // 武器伤害相关UI
    public Button DamageCountUIButton;
    public GameObject DamageCount;

    private Dictionary<string, int> WeaponDamage = new Dictionary<string, int>();

    // 通关奖励
    public GameObject AwardUI;

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
        GameContext.isGameOver = false;
        CurrentLevel.text = "第 " + GameContext.CurrentLevel + " 关";
        AwardUI.SetActive(false);
    }

    void Update()
    {
        if (GameContext.isGameOver)
        {
            AwardUI.SetActive(true);
            return;
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

    // 更新敌人数量
    public void UpdateEnemyNumber(int num)
    {
        GameContext.number.res += num;
        EnemyNumberUpdate += UpdateEnemy;
    }

    private void UpdateEnemy()
    {
        EnemyTotal.text = GameContext.number.res.ToString() + " / " + GameContext.number.total.ToString();
    }

}