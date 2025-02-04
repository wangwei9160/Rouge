using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;


public class TalentTreeUI : MonoBehaviour 
{
    public Button RefreshTalentBtn;
    public Button AddTalentBtn;
    public Button SubTalentBtn;
    public Button[] Buttons;
    public Text TalentNumber;

    public void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.UpTalentByID, UpTalent);
        EventCenter.AddListener(EventDefine.ShowSelectUI, Refresh);
    }

    public void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.UpTalentByID, UpTalent);
        EventCenter.RemoveListener(EventDefine.ShowSelectUI, Refresh);
    }

    private void Start()
    {
        
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

    public void Refresh()
    {
        RefreshTalentNumber();
        for (int i = 0; i < Buttons.Length; i++)
        {
            Buttons[i].GetComponent<TalentButton>().Set(i, MainSceneManager.Instance.talentData.MaxCnt[i]);
            Refresh(i);
        }
    }

    public void TalentNumberOnChange(int number)
    {
        MainSceneManager.Instance.TalentNumberOnChange(number);
        RefreshTalentNumber();
    }

    public void RefreshTalentNumber()
    {
        TalentNumber.text = string.Format(" £”‡ÃÏ∏≥µ„£∫{0}", MainSceneManager.Instance.talentData.Total);
    }

    public void Refresh(int idx)
    {
        Buttons[idx].GetComponent<TalentButton>().RefreshCount(MainSceneManager.Instance.talentData.Cnt[idx]);
    }

    public void ClearAllTalent()
    {
        int total = 0;
        for(int i = 0; i < Buttons.Length; i++)
        {
            total += MainSceneManager.Instance.talentData.Cnt[i];
            MainSceneManager.Instance.talentData.Cnt[i] = 0;
            Refresh(i);
        }
        MainSceneManager.Instance.TalentNumberOnChange(total);
        RefreshTalentNumber();
    }

    public void UpTalent(int idx)
    {
        MainSceneManager.Instance.UpTalent(idx);
        Refresh(idx);
        RefreshTalentNumber();
    }

}