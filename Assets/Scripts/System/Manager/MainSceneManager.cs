using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : Singleton<MainSceneManager> 
{
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

}