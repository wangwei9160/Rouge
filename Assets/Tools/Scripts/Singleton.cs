using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance = null;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType(typeof(T)) as T;
                if (_instance == null)
                {   
                    _instance = new GameObject(typeof(T).Name).AddComponent<T>();
                }
                if (_instance == null)
                    Debug.Log("Failed to create instance of " + typeof(T).FullName + ".");
            }
            return _instance;
        }
    }

    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
