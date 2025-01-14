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
    // �ؿ�UI
    public Text CurrentWave;                    // ��ǰ����
    public Text CurrentWaveEnemyCount;         // ���˼���

    private Action EnemyNumberUpdate;   
    private Action GameOverUI;

    // �����˺����UI
    public Button DamageCountUIButton;
    public GameObject DamageCount;

    private Dictionary<string, int> WeaponDamage = new Dictionary<string, int>();

    // �̵�UI
    public GameObject ShopUI;
    public bool[] isLock = new bool[4];

    // ��ɫ��Ϣ��ʾ
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

    // ���������˺�
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
        CurrentWave.text = "�� " + GameManager.Instance.gameData.CurrentWave + " ��";
    }

    // ���µ�������
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