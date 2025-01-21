using UnityEngine;
using UnityEngine.UI;

public class TalentButton : MonoBehaviour 
{
    public GameObject UpInfo;
    public Button Up;       // 升级按钮
    public Button Close;    // 关闭按钮
    public int ID;          // 当前对应的按钮
    public int MaxCount;    // 最大升级次数
    public Text Cnt;

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            UpInfo.SetActive(!UpInfo.activeInHierarchy);
        });
        Up.onClick.AddListener(() =>
        {
            EventCenter.Broadcast<int>(EventDefine.UpTalentByID, ID);
        });
        Close.onClick.AddListener(() =>
        {
            UpInfo.SetActive(false);
        });
        UpInfo.SetActive(false);
    }

    public void Set(int id , int mxCnt)
    {
        this.ID = id;
        MaxCount = mxCnt;
    }

    public void RefreshCount(int count)
    {
        Cnt.text = string.Format("{0}/{1}", count, MaxCount);
    }

}