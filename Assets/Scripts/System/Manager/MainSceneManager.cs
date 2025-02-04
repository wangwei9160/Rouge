using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : Singleton<MainSceneManager> 
{
    public TalentData talentData;   // �츳������

    public bool isSignle = true;
    public Button signleButton;     // ������Ϸ����ѡ�����
    public Button teamButton;       // �޸�Ϊ�浵ϵͳ

    public Transform UI;            // UI root

    public GameObject BG;           // ����
    public GameObject buttons;      // ���а�ť
    public GameObject SelectUI;     // ѡ�����

    public GameObject Info;         // ��ʾ��Ϣ

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
        // ��ֹbuild��playerprefs�����ڵ��µ���Ч����
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
                    EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "û�ж�����츳��");
                    return;
                }

            }
            else
            {
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "ǰ�ýڵ�δ���");
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
                    EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "û�ж�����츳��");
                    return;
                }
            }
            else
            {
                EventCenter.Broadcast(EventDefine.ShowNoticeInfoUI, "ǰ�ýڵ�δ���");
                return;
            }
        }
    }

    #endregion
}