using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : Singleton<MainSceneManager> 
{
    public TalentData talentData;   // 天赋树数据

    public bool isSignle = true;
    public Button signleButton;     // 单人游戏进入选择界面
    public Button teamButton;       // 修改为存档系统

    public Transform UI;            // UI root

    public GameObject BG;           // 背景
    public GameObject buttons;      // 所有按钮
    public GameObject SelectUI;     // 选择界面

    public GameObject Info;         // 提示信息

    protected override void Awake()
    {
        base.Awake();
        EventCenter.AddListener(EventDefine.ShowMainScene, Show);
        EventCenter.AddListener(EventDefine.HideMainScene, Hide);
        EventCenter.AddListener<string>(EventDefine.ShowNoticeInfoUI, Notice);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowMainScene, Show);
        EventCenter.RemoveListener(EventDefine.HideMainScene, Hide);
        EventCenter.RemoveListener<string>(EventDefine.ShowNoticeInfoUI, Notice);
    }

    void Start()
    {
        //PlayerPrefs.DeleteKey(Constants.TALENTPLAYERPREFS);
        // 防止build后playerprefs不存在导致的无效问题
        if (!PlayerPrefs.HasKey(Constants.TALENTPLAYERPREFS))
        {
            talentData = new TalentData();
            string json = JsonUtility.ToJson(talentData);
            //Debug.Log(json);
            PlayerPrefs.SetString(Constants.TALENTPLAYERPREFS, json);
            PlayerPrefs.Save();
        }
        string js = PlayerPrefs.GetString(Constants.TALENTPLAYERPREFS);
        //Debug.Log(js);
        talentData = JsonUtility.FromJson<TalentData>(js);
        signleButton.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.ShowSelectUI);
        });
        teamButton.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.ShowDataFileUI , 1);
        });
        StartCoroutine(PreLoadInfo());
    }

    public IEnumerator PreLoadInfo()
    {
        yield return new WaitForSeconds(0.1f);
        var weapon = TplUtil.GetWeaponTplDic();
        Debug.Log(weapon[1].ID);
        yield return new WaitForSeconds(0.1f);
        //var weapon = TplUtil.GetWeaponTplDic();
    }

    public void Hide()
    {
        BG.SetActive(false);
        buttons.SetActive(false);
    }

    public void Show()
    {
        BG.SetActive(true);
        buttons.SetActive(true);
    }

    public void Notice(string arg)
    {
        GameObject tmp = Instantiate(Info , UI);
        tmp.GetComponent<NoticeInfoUI>().SetInfo(arg);
    }


    #region Talent

    public void TalentNumberOnChange(int number)
    {
        MainSceneManager.Instance.talentData.Total = math.max(MainSceneManager.Instance.talentData.Total + number, 0);
        string json = JsonUtility.ToJson(MainSceneManager.Instance.talentData);
        Debug.Log(json);
        PlayerPrefs.SetString(Constants.TALENTPLAYERPREFS, json);
        PlayerPrefs.Save();
    }

    public bool Check(int idx)
    {
        if (idx == -1) return true;
        return MainSceneManager.Instance.talentData.Cnt[idx] == MainSceneManager.Instance.talentData.MaxCnt[idx];
    }

    public void UpTalent(int idx)
    {
        if (talentData.Roots[idx].root.Count == 1)
        {
            if (Check(talentData.Roots[idx].root[0]))
            {
                if (talentData.Total >= 1)
                {
                    if (Check(idx))
                    {
                        return;
                    }
                    talentData.Cnt[idx] += 1;
                    TalentNumberOnChange(-1);
                }
                else
                {
                    EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "没有多余的天赋点");
                    return;
                }

            }
            else
            {
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "前置节点未完成");
                return;
            }
        }
        else
        {
            if (Check(talentData.Roots[idx].root[0]) && Check(talentData.Roots[idx].root[1]))
            {
                if (talentData.Total >= 1)
                {
                    if (Check(idx))
                    {
                        return;
                    }
                    talentData.Cnt[idx] += 1;
                    TalentNumberOnChange(-1);
                }
                else
                {
                    EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "没有多余的天赋点");
                    return;
                }
            }
            else
            {
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "前置节点未完成");
                return;
            }
        }
    }

    #endregion
}