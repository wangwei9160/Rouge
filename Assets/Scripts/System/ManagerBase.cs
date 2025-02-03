using Unity.VisualScripting;
using UnityEngine;

public abstract class StaticBase<T> : MonoBehaviour where T : MonoBehaviour
{
    public static T Instance { get; private set; }
    protected virtual void Awake() => Instance = this as T;
    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(Instance);
    }
}

public abstract class Singleton<T> : StaticBase<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }else
        {
            base.Awake();
        }
    }
}

public abstract class ManagerBase<T> : Singleton<T> where T : MonoBehaviour
{
    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }
}
