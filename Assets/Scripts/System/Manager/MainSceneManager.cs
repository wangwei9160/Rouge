using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneManager : MonoBehaviour 
{
    public bool isSignle = true;
    public Button signleButton;
    public Button doubleButton;


    void Awake()
    {
    }

    void Start()
    {
        signleButton.onClick.AddListener(SignleGameStart);
    }

    void Update()
    {
    }

    private void SignleGameStart()
    {
        SceneManager.LoadScene("BattleScene");
        Debug.Log("Load");
    }
}