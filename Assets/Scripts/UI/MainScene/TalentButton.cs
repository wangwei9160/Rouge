using UnityEngine;
using UnityEngine.UI;

public class TalentButton : MonoBehaviour 
{
    public GameObject UpInfo;
    public Button Up;       // ������ť
    public Button Close;    // �رհ�ť
    public int ID;          // ��ǰ��Ӧ�İ�ť
    public int MaxCount;    // �����������
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