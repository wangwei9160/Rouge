using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SelectUI : MonoBehaviour 
{
    public Button Weapon;
    public Button Hero;
    public Button StartGame;
    public Button Back2Main;

    public GameObject WeaponSelect;

    private void Awake()
    {
        EventCenter.AddListener(EventDefine.ShowSelectUI , Show) ;
        EventCenter.AddListener(EventDefine.HideSelectUI, Hide);
        EventCenter.AddListener(EventDefine.RefreshWeapon, RefreshWeapon);
    }

    private void OnDestroy()
    {
        EventCenter.RemoveListener(EventDefine.ShowSelectUI, Show);
        EventCenter.RemoveListener(EventDefine.HideSelectUI, Hide);
        EventCenter.RemoveListener(EventDefine.RefreshWeapon, RefreshWeapon);
    }

    private void Start()
    {
        Weapon.onClick.AddListener(() =>
        {
            //Debug.Log("ÌôÑ¡ÎäÆ÷");
            WeaponSelect.SetActive(true);
        });
        StartGame.onClick.AddListener(() =>
        {
            EventCenter.Broadcast<int>(EventDefine.ShowDataFileUI, 0);
            //EventCenter.Broadcast(EventDefine.StartGame);
            //SceneManager.LoadScene("BattleScene");
        });
        Back2Main.onClick.AddListener(() =>
        {
            Hide();
            EventCenter.Broadcast(EventDefine.ShowMainScene);
        });
        WeaponSelect.SetActive(false);
        Hide();
    }

    private void RefreshWeapon()
    {
        Weapon.GetComponent<WeaponButtonUI>().SetUI(GameManager.Instance.gameData.WeaponIDs[0]);
    }

    private void Show()
    {
        gameObject.SetActive(true); 
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }

}