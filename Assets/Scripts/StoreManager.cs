using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour 
{
    public Button nextLevelButton;
    void Awake()
    {
    }

    void Start()
    {
        nextLevelButton.onClick.AddListener(nextLevel);
    }

    void Update()
    {
    }

    public void nextLevel()
    {
        GameContext.CurrentLevel += 1;
        GameContext.isGameOver = false;
        SceneManager.LoadScene("BattleScene");
    }
}