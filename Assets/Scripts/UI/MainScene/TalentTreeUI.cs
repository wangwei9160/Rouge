using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;


public class TalentTreeUI : MonoBehaviour 
{
    public Button RefreshTalentBtn;
    public Button AddTalentBtn;
    public Button SubTalentBtn;
    public Button[] Buttons;
    public Text TalentNumber;

    TalentData talentData;

    public void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.UpTalentByID, UpTalent);
    }

    public void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.UpTalentByID, UpTalent);
    }

    private void Start()
    {
        //PlayerPrefs.DeleteKey("Talent");
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
        TalentNumberOnChange(0);
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<TalentButton>().Set(i, talentData.MaxCnt[i]);
            Refresh(i);
        }
        AddTalentBtn.onClick.AddListener(() =>
        {
            TalentNumberOnChange(1);
        });
        SubTalentBtn.onClick.AddListener(() =>
        {
            TalentNumberOnChange(-1);
        });
        RefreshTalentBtn.onClick.AddListener(ClearAllTalent);
    }

    public void TalentNumberOnChange(int number)
    {
        talentData.Total = math.max(talentData.Total + number, 0);
        string json = JsonUtility.ToJson(talentData);
        //Debug.Log(json);
        PlayerPrefs.SetString(Constants.TALENTPLAYERPREFS, json);
        PlayerPrefs.Save();
        RefreshTalentNumber();
    }

    public void RefreshTalentNumber()
    {
        TalentNumber.text = string.Format("剩余天赋点：{0}", talentData.Total);
    }

    public void Refresh(int idx)
    {
        Buttons[idx].GetComponent<TalentButton>().RefreshCount(talentData.Cnt[idx]);
    }

    public bool Check(int idx)
    {
        if (idx == -1) return true;
        return talentData.Cnt[idx] == talentData.MaxCnt[idx];
    }

    public void ClearAllTalent()
    {
        int total = 0;
        for(int i = 0; i < Buttons.Length; i++)
        {
            total += talentData.Cnt[i];
            talentData.Cnt[i] = 0;
            Refresh(i);
        }
        TalentNumberOnChange(total);
    }

    public void UpTalent(int idx)
    {
        if (talentData.Roots[idx].root.Count== 1)
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
        }else
        {
            if(Check(talentData.Roots[idx].root[0]) && Check(talentData.Roots[idx].root[1]) )
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
        Refresh(idx);
        RefreshTalentNumber();
    }

}