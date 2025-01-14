using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : ManagerBaseWithoutPersist<MainSceneManager> 
{
    public bool isSignle = true;
    public Button signleButton;
    public Button teamButton;
    public Button GoButton;         // ����ѡ�����
    public Button BackButton;       // ����������

    public GameObject BG;           // ����
    public GameObject buttons;      // ���а�ť
    public GameObject SelectUI;     // ѡ�����

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