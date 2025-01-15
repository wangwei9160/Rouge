using UnityEngine;
using UnityEngine.UI;

public class MainSceneManager : ManagerBaseWithoutPersist<MainSceneManager> 
{
    public bool isSignle = true;
    public Button signleButton;     // 单人游戏进入选择界面
    public Button teamButton;

    public GameObject BG;           // 背景
    public GameObject buttons;      // 所有按钮
    public GameObject SelectUI;     // 选择界面

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