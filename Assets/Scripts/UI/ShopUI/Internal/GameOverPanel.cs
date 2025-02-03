using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    public Button BackMenuBtn;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.GameOver, Show);
    }

    public void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.GameOver, Show);
    }

    private void Start()
    {
        BackMenuBtn.onClick.AddListener(() =>
        {
            GameManager.Instance.ClearGameData(GameManager.Instance.gameData.SaveIndex);
            GameManager.Instance.TalentOnGet(1);    // 获得一点天赋点
            SceneManager.LoadScene("MainScene");
        });    
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

}