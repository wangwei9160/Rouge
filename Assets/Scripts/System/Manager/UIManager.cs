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

    // �����˺����UI
    public Button DamageCountUIButton;
    public GameObject DamageCount;

    private Dictionary<string, int> WeaponDamage = new Dictionary<string, int>();

    // �̵�UI
    public GameObject ShopUI;
    public bool[] isLock = new bool[4];


    // �ؿ�UI
    public Text CurrentWave;                    // ��ǰ����
    public Text CurrentWaveEnemyCount;         // ���˼���
    public GameObject CoinUI;
    public GameObject WaveUI;               
    public GameObject EnemyUI;                  

    // ��ɫ��Ϣ��ʾ
    public GameObject PlayerInfoShow;
    public GameObject NoticeInfoPrefab;

    private void OnEnable()
    {
        EventCenter.AddListener(EventDefine.ShowShopUI, ShowShopUI);
        EventCenter.AddListener(EventDefine.HideShopUI, HideShopUI);
        EventCenter.AddListener(EventDefine.RefreshPlayerAttribute, UpdatePlayerInfoShow);
        EventCenter.AddListener(EventDefine.RefreshEnemyCount, UpdateEnemy);
        EventCenter.AddListener<string>(EventDefine.ShowNoticeInfoUI, ShowNoticeInfoUI);
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener(EventDefine.ShowShopUI, ShowShopUI);
        EventCenter.RemoveListener(EventDefine.HideShopUI, HideShopUI);
        EventCenter.RemoveListener(EventDefine.RefreshPlayerAttribute, UpdatePlayerInfoShow);
        EventCenter.RemoveListener(EventDefine.RefreshEnemyCount, UpdateEnemy);
        EventCenter.RemoveListener<string>(EventDefine.ShowNoticeInfoUI, ShowNoticeInfoUI);
    }

    void Start()
    {
        ShopUI.SetActive(false);
        DamageCountUIButton.onClick.AddListener(() =>
        {
            DamageCount.SetActive(!DamageCount.activeInHierarchy);
        });
        
    }

    private void ShowShopUI()
    {
        //Debug.Log("��ʾ�̵�UI");
        ShopUI.SetActive(true);
        PlayerInfoShow.gameObject.SetActive(false);
        WaveUI.SetActive(false);
        EnemyUI.SetActive(false);
    }

    private void HideShopUI()
    {
        //Debug.Log("�����̵�UI");
        ShopUI.SetActive(false);
        PlayerInfoShow.gameObject.SetActive(true);
        UpdatePlayerInfoShow();
        WaveUI.SetActive(true);
        EnemyUI.SetActive(true);
        UpdateCurrentLevel();
        UpdateEnemy();
    }

    private void ShowNoticeInfoUI(string str)
    {
        GameObject go = Instantiate(NoticeInfoPrefab , canvas);
        go.GetComponent<NoticeInfoUI>().SetInfo(str);
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
        CurrentWave.text = string.Format("�� {0} ��", GameManager.Instance.gameData.CurrentWave);
    }

    // ���µ�������
    private void UpdateEnemy()
    {
        CurrentWaveEnemyCount.text = string.Format("{0} / {1}", GameContext.CurrentWaveKill, GameContext.CurrentWaveCount);
    }

    public void UpdatePlayerInfoShow()
    {
        PlayerInfoShow.GetComponent<PlayerInfoUI>().UpdateExp(GameManager.Instance.gameData.curLevel , GameManager.Instance.gameData.curExp , GameManager.Instance.gameData.nextLevelExp);
    }

}