using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectUI : MonoBehaviour 
{
    public Button Weapon;
    public Button Hero;

    public GameObject WeaponSelect;

    private void Start()
    {
        Weapon.GetComponent<WeaponButtonUI>().SetUI(GameManager.Instance.gameData.WeaponIDs[0]);
        WeaponSelect.SetActive(false);
        Weapon.onClick.AddListener(() =>
        {
            //Debug.Log("ÃÙ—°Œ‰∆˜");
            WeaponSelect.SetActive(true);
        });
    }

    private void Update()
    {
        Weapon.GetComponent<WeaponButtonUI>().SetUI(GameManager.Instance.gameData.WeaponIDs[0]);
    }
}