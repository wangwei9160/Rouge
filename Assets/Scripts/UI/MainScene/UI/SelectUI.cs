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

    private void Start()
    {
        Weapon.GetComponent<WeaponButtonUI>().SetUI(GameManager.Instance.gameData.WeaponIDs[0]);
        WeaponSelect.SetActive(false);
        Weapon.onClick.AddListener(() =>
        {
            //Debug.Log("ÌôÑ¡ÎäÆ÷");
            WeaponSelect.SetActive(true);
        });
        StartGame.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("BattleScene");
        });
        Back2Main.onClick.AddListener(() =>
        {
            EventCenter.Broadcast(EventDefine.HideSelectUI);
        });
    }

    private void Update()
    {
        Weapon.GetComponent<WeaponButtonUI>().SetUI(GameManager.Instance.gameData.WeaponIDs[0]);
    }
}