using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : ManagerBaseWithoutPersist<MainSceneManager> 
{
    public bool isSignle = true;
    public Button signleButton;
    public Button teamButton;
    public Button GoButton;         // 进入选择界面
    public Button BackButton;       // 返回主界面

    public GameObject BG;           // 背景
    public GameObject buttons;      // 所有按钮
    public GameObject SelectUI;     // 选择界面

    void Start()
    {
        SelectUI.SetActive(false);
        signleButton.onClick.AddListener(SelectShow);
        GoButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BattleScene");
        });
        BackButton.onClick.AddListener(MainShow);
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