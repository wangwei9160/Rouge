using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : ManagerBaseWithoutPersist<MainSceneManager> 
{
    public bool isSignle = true;
    public Button signleButton;     // ������Ϸ����ѡ�����
    public Button teamButton;

    public GameObject BG;           // ����
    public GameObject buttons;      // ���а�ť
    public GameObject SelectUI;     // ѡ�����

    private void OnEnable()
    {
        EventCenter.AddListener(EventDefine.ShowSelectUI, SelectShow);
        EventCenter.AddListener(EventDefine.HideSelectUI, MainShow);
    }

    void Start()
    {
        SelectUI.SetActive(false);
        signleButton.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.ShowSelectUI);
        });
    }

    public void SelectShow()
    {
        BG.SetActive(false);
        buttons.SetActive(false);
        SelectUI.SetActive(true);
    }

    public void MainShow()
    {
        BG.SetActive(true);
        buttons.SetActive(true);
        SelectUI.SetActive(false);
        GameManager.Instance.gameData.Init();
    }

}