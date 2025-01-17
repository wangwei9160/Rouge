using UnityEngine;

public abstract class ManagerBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }else
        {
            Instance = this as T;
        }
        DontDestroyOnLoad(gameObject);
    }



    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(Instance);
    }
}

public abstract class ManagerBaseWithoutPersist<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this as T;
        }
    }

    protected virtual void OnApplicationQuit()
    {
        Instance = null;
    }

}
