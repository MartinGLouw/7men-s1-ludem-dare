using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance != null)
            {
                return _instance;
            }
            else
            {
                _instance = FindObjectOfType<T>();
                if(_instance != null)
                {
                    return _instance;
                }
                else
                {
                    GameObject obj = new GameObject();
                    _instance = obj.AddComponent<T>();
                    obj.name = typeof(T) + " - [Singleton]";
                    DontDestroyOnLoad(obj);
                    return _instance;
                }
            }
        }
    }

    protected virtual void Awake()
    {
    
        if (_instance == null)
        {
            _instance = (T)this;
            DontDestroyOnLoad(gameObject);
        }
        else if (_instance != this) // Check if _instance is not equal to this
        {
            Destroy(gameObject);
            return; // Return to prevent further execution
        }

        SetupSingletonOnAwake();
    }

    protected virtual void SetupSingletonOnAwake()
    {

    }
}
