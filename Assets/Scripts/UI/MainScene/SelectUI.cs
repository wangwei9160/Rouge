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
        RefreshWeapon();
    }

    private void OnEnable()
    {
        EventCenter.AddListener(EventDefine.RefreshWeapon, RefreshWeapon);
    }

    private void OnDisable()
    {
        EventCenter.RemoveListener(EventDefine.RefreshWeapon, RefreshWeapon);
    }

    private void Start()
    {
        WeaponSelect.SetActive(false);
        Weapon.onClick.AddListener(() =>
        {
            //Debug.Log("ÌôÑ¡ÎäÆ÷");
            WeaponSelect.SetActive(true);
        });
        StartGame.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.StartGame);
            SceneManager.LoadScene("BattleScene");
        });
        Back2Main.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.HideSelectUI);
        });
    }

    private void RefreshWeapon()
    {
        Weapon.GetComponent<WeaponButtonUI>().SetUI(GameManager.Instance.gameData.WeaponIDs[0]);
    }

}