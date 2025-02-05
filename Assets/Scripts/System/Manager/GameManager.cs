using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameManager : ManagerBase<GameManager>
{
    public StateID state = StateID.ReadyState;

    public GameData gameData;       // ��Ϸ����
    public bool isLoadAsset = false;

    // ������Ϣ
    private Dictionary<int, WaveTplInfo> _waveDic;
    public Dictionary<int, WaveTplInfo> WaveDic
    {
        get
        {
            if(_waveDic == null)
            {
                _waveDic = TplUtil.GetWaveTplDic();
            }
            return _waveDic;
        }
    }

    private void OnEnable()
    {
        //Debug.Log("AddListener EventDefine.StartGame");
        EventCenter.AddListener(EventDefine.StartGame, DoBeforStartGame);
    }

    private void OnDisable()
    {
        //Debug.Log("RemoveListener EventDefine.StartGame");
        EventCenter.RemoveListener(EventDefine.StartGame, DoBeforStartGame);
    }

    private void Start()
    {
        gameData = new GameData();
    }

    void Update()
    {
        if(gameData.SaveIndex != -1)
        {
            gameData.playTime += Time.deltaTime;
        }
        if (!isLoadAsset)
        {
            isLoadAsset = true;
            StartCoroutine(LoadAsset());
        }
        if(state == StateID.FightState)
        {
            if(GameContext.CurrentWaveKill == GameContext.CurrentWaveCount)
            {
                CurrentWaveFinish();
            }
        }
    }

    public void DoBeforStartGame()
    {
        GameContext.CurrentWaveKill = 0;
        GameContext.CurrentWaveCount = WaveDic[gameData.CurrentWave].Total;
        foreach (var buff in gameData.playerBuffList.buffs)
        {
            buff.OnBeforeWaveStart();
        }
        gameData.curHp = (int)gameData.playerAttr.maxHp;
        EventCenter.Broadcast(EventDefine.RefreshPlayerAttribute);
        //Debug.Log(string.Format("{0} , {1} / {2}", gameData.CurrentWave, GameContext.CurrentWaveKill , GameContext.CurrentWaveCount));
        TransState(StateID.FightState);
    }

    public void CurrentWaveFinish()
    {
        TransState(StateID.ShopState);
        OnMoneyChange(gameData.playerAttr.Revenues); // ÿ�غϹ̶�����
        // �غϽ���buff����
        foreach (var buff in gameData.playerBuffList.buffs)
        {
            buff.OnWaveEnd();
        }
        EventCenter.Broadcast(EventDefine.ShowShopUI);
        // �浵
        SaveGameData(gameData.SaveIndex);
        OverridePlayerPrefsData(gameData.SaveIndex);
    }

    public IEnumerator LoadAsset()
    {
        yield return new WaitForSeconds(0.1f);
        if (AssetManager.Instance != null)
        {
            Debug.Log("Load AssetManager");
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

    public void OnGetFreeCnt(int cnt)
    {
        gameData.freeTime += cnt;
        EventCenter.Broadcast(EventDefine.OnGetFree);
    }

    public void OnMoneyChange(int money_)
    {
        if(money_ < 0)
        {
            gameData.Cost -= money_;
        }
        gameData.money += money_;
        EventCenter.Broadcast(EventDefine.RefreshPlayerAttribute);
    }

    public void OnHpChange(int damage)
    {
        gameData.curHp -= damage;
        if(gameData.curHp <= 0)
        {
            gameData.curHp = 0;
            EventCenter.Broadcast(EventDefine.GameOver);
            return;
        }
        EventCenter.Broadcast(EventDefine.RefreshPlayerAttribute);
    }

    public bool TryPayMoney(int money_)
    {
        if(gameData.money < money_)
        {
            return false;
        }
        return true;
    }

    public bool HasItem(int id)
    {
        return gameData.HasItem(id);
    }

    #region �̵��߼�

    public bool BuyItemByID(int id)
    {
        //Debug.Log(TplUtil.GetItemTplDic()[id].Name);
        
        int needMoney = TplUtil.GetItemTplDic()[id].Price;
        if (!TryPayMoney(needMoney))
        {
            EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ��Ҳ���");
            return false;
        }
        OnMoneyChange( -1 * needMoney);
        ItemFactory.GetItemByID(id).OnGet();
        gameData.OnGetItemByID(id);
        EventCenter.Broadcast(EventDefine.RefreshItem);
        return true;
    }

    public bool BuyWeaponByID(int id)
    {
        //Debug.Log(TplUtil.GetWeaponTplDic()[id].Name);
        int idx = FindWeaponSlotEmptyIndex(); 
        if (idx == -1) // û�п�λ
        {
            WeaponTplInfo weapon = TplUtil.GetWeaponTplDic()[id];
            if(weapon.Next == -1) // �޷��ϳ�
            {
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ������û�п�λ");
                return false;
            }
            idx = FindWeaponSlotIndexSameAsID(id);
            if(idx == -1) 
            {
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ������û�п�λ");
                return false;
            }else
            {
                int needMoney = TplUtil.GetWeaponTplDic()[id].Price;
                if (!TryPayMoney(needMoney))
                {
                    EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ��Ҳ���");
                    return false;
                }
                OnMoneyChange(-1 * needMoney);
                gameData.WeaponIDs[idx] = weapon.Next;
            }
        }else
        {
            int needMoney = TplUtil.GetWeaponTplDic()[id].Price;
            if (!TryPayMoney(needMoney))
            {
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ��Ҳ���");
                return false;
            }
            OnMoneyChange( -1 * needMoney);
            gameData.WeaponIDs[idx] = id;
        }
        EventCenter.Broadcast(EventDefine.RefreshWeapon);
        return true;
    }

    /// <summary>
    /// ���Ժϳ�
    /// </summary>
    /// <param name="id"> ��ƷID </param>
    /// <param name="pos"> ����ֵ </param>
    /// <returns></returns>
    public bool TryMerge(int id , int pos)
    {
        WeaponTplInfo weapon = TplUtil.GetWeaponTplDic()[id];
        if (weapon.Next == -1) // û����һ���ȼ�������
        {
            EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ�����޷���һ������");
            return false;
        }
        int idx = FindWeaponSlotIndexSameAsID(id, pos);
        if (idx == -1)      // û���ҵ���ͬ������
        {
            EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "��ǰ��������ͬϡ�жȵ�ͬ�������޷���һ���ϳ�");
            return false;
        }
        gameData.WeaponIDs[Math.Min(idx , pos)] = weapon.Next;
        gameData.WeaponIDs[Math.Max(idx , pos)] = -1;
        EventCenter.Broadcast(EventDefine.RefreshWeapon);
        return true;
    }

    public void ScaleWeaponByIndex(int idx)
    {
        gameData.WeaponIDs[idx] = -1;
        EventCenter.Broadcast(EventDefine.RefreshWeapon);
    }

    /// <summary>
    /// ��������
    /// </summary>

    // �ҵ�һ����λ
    private int FindWeaponSlotEmptyIndex()
    {
        int idx = -1;
        for (int i = 0; i < gameData.WeaponSlot; i++)
        {
            if (gameData.WeaponIDs[i] == -1)
            {
                idx = i;
                break;
            }
        }
        return idx;
    }

    // �ҵ���һ����id��ͬ��ֵ��λ��
    private int FindWeaponSlotIndexSameAsID(int id)
    {
        int idx = -1;
        for (int i = 0; i < gameData.WeaponSlot; i++)
        {
            if (gameData.WeaponIDs[i] == id)
            {
                idx = i;
                break;
            }
        }
        return idx;
    }

    // �ҵ���һ����������pos��ֵ��id��λ��
    private int FindWeaponSlotIndexSameAsID(int id , int pos)
    {
        int idx = -1;
        for (int i = 0; i < gameData.WeaponSlot; i++)
        {
            if (i == pos) continue;
            if (gameData.WeaponIDs[i] == id)
            {
                idx = i;
                break;
            }
        }
        return idx;
    }


    #endregion

    #region SaveOrLoad 

    // �츳���������
    public void TalentOnGet(int number)
    {
        string js = PlayerPrefs.GetString(Constants.TALENTPLAYERPREFS);
        TalentData talentData = JsonUtility.FromJson<TalentData>(js);
        talentData.Total = math.min(math.max(talentData.Total + number, 0) , talentData.MaxNumber);
        string json = JsonUtility.ToJson(talentData);
        //Debug.Log(json);
        PlayerPrefs.SetString(Constants.TALENTPLAYERPREFS, json);
        PlayerPrefs.Save();
    }

    // ������߼���
    public void SaveOrLoadData(bool isLoad , int idx)
    {
        StartCoroutine(LoadScene(isLoad, idx));
    }

    IEnumerator LoadScene(bool isLoad, int idx)
    {
        if (isLoad)
        {
            LoadGameData(idx);
            SaveDataByPlayerPrefs(idx);
            if(gameData.playTime != 0f)
            {
                state = StateID.ShopState;
                AsyncOperation t = SceneManager.LoadSceneAsync("BattleScene");
                while (!t.isDone)
                {
                    yield return null;
                    //Debug.Log("���س���");
                }
                EventCenter.Broadcast(EventDefine.ShowShopUI);
            }else
            {
                state = StateID.FightState;
                var t = SceneManager.LoadSceneAsync("BattleScene");
                while (!t.isDone)
                {
                    yield return null;
                    //Debug.Log("���س���");
                }
                EventCenter.Broadcast(EventDefine.HideShopUI);
                DoBeforStartGame();
            }
            
        }
        else
        {
            SaveGameData(idx);
            SaveDataByPlayerPrefs(idx);
            state = StateID.FightState;
            var t = SceneManager.LoadSceneAsync("BattleScene");
            while (!t.isDone)
            {
                yield return null;
                //Debug.Log("���س���");
            }
            EventCenter.Broadcast(EventDefine.HideShopUI);
            DoBeforStartGame();
        }
    }
    
    // �浵
    public void SaveGameData(int Idx)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Create(Application.persistentDataPath + string.Format("/SaveData{0}.data", Idx)))
            {
                bf.Serialize(fs, gameData);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    // ����������ʾ�����ݣ�ͨ��PlayerPrefs
    public void SaveDataByPlayerPrefs(int Idx)
    {
        FileShowData data = new FileShowData(gameData);
        string jsonData = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(string.Format("{0}-{1}", Constants.PLAYERPREFES, Idx), jsonData);
        PlayerPrefs.Save();
    }

    public FileShowData LoadPlayerPrefsData(int Idx)
    {
        string key = string.Format("{0}-{1}", Constants.PLAYERPREFES, Idx);
        if (PlayerPrefs.HasKey(key))
        {
            string json = PlayerPrefs.GetString(key);
            return JsonUtility.FromJson<FileShowData>(json);
        }
        return null;
    }

    public void ClearGameData(int Idx)
    {
        try
        {
            string path = Application.persistentDataPath + string.Format("/SaveData{0}.data", Idx);
            if (File.Exists(path))
            {
                File.Delete(path);
                Debug.Log(string.Format("{0} ��ɾ��", path));
            }else
            {
                Debug.LogError(string.Format("{0} δ�ҵ�", path));
            }
            string key = string.Format("{0}-{1}", Constants.PLAYERPREFES, Idx);
            if (PlayerPrefs.HasKey(key))
            {
                PlayerPrefs.DeleteKey(key);
                PlayerPrefs.Save();
                Debug.Log(string.Format("{0} ��ɾ��", key));
            }
            else
            {
                Debug.LogError(string.Format("{0} δ�ҵ�", key));
            }
            // �����׼������ʱɾ���浵�ᵼ�µ�ǰû������Ȼ�󸲸�һ���µĴ浵���´���
            //gameData = new GameData(); // ���
        }
        catch (Exception e)
        {
            Debug.LogError("ɾ�������쳣" + e.Message); ;
        }
    }

    // ���Ǳ����������ʾ�����ݣ�ͨ��PlayerPrefs
    public void OverridePlayerPrefsData(int Idx)
    {
        FileShowData data = LoadPlayerPrefsData(Idx);
        data.Set(gameData);
        string jsonData = JsonUtility.ToJson(data);
        //Debug.Log(jsonData);
        PlayerPrefs.SetString(string.Format("{0}-{1}", Constants.PLAYERPREFES, Idx), jsonData);
        PlayerPrefs.Save();
    }

    // ����
    public void LoadGameData(int Idx)
    {
        try
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream fs = File.Open(Application.persistentDataPath + string.Format("/SaveData{0}.data", Idx) , FileMode.Open))
            {
                gameData = (GameData)bf.Deserialize(fs);
                state = StateID.ShopState;
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
        }
    }

    #endregion
}
