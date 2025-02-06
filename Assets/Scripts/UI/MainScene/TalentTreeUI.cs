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
    public int INDEX;
    public Button button;

    public void Awake()
    {
        EventCenter.AddListener<int>(EventDefine.ShowPanelByIDInSelectPannel , Show);
        EventCenter.AddListener<int>(EventDefine.UpTalentByID, UpTalent);
        EventCenter.AddListener(EventDefine.ShowSelectUI, Refresh);
    }

    public void OnDestroy()
    {
        EventCenter.RemoveListener<int>(EventDefine.ShowPanelByIDInSelectPannel, Show);
        EventCenter.RemoveListener<int>(EventDefine.UpTalentByID, UpTalent);
        EventCenter.RemoveListener(EventDefine.ShowSelectUI, Refresh);
    }

    private void Start()
    {
        INDEX = 1;
        button.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.ShowPanelByIDInSelectPannel , INDEX);
        });
        AddTalentBtn.onClick.AddListener(() =>
        {
            TalentNumberOnChange(1);
        });
        SubTalentBtn.onClick.AddListener(() =>
        {
            TalentNumberOnChange(-1);
        });
        RefreshTalentBtn.onClick.AddListener(ClearAllTalent);
        gameObject.SetActive(false);
    }

    public void Show(int idx)
    {
        if(INDEX == idx)
        {
            Color color = new Color(1f , 0.16f,0f);
            button.GetComponent<Image>().color = color;
            gameObject.SetActive(true);
        }else
        {
            Color color = new Color(1f,1f,1f);
            button.GetComponent<Image>().color = color;
            gameObject.SetActive(false);
        }
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