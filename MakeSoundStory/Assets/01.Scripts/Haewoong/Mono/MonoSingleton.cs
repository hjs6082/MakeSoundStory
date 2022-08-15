using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static object lockObject = new object();
    private static T instance = null;
    private static bool isQuitting = false;

    public static T Instance
    {
        get
        {
            lock(lockObject)
            {
                if(isQuitting)
                {
                    return null;
                }

                if(instance == null)
                {
                    instance = GameObject.Instantiate(Resources.Load<T>("MonoSingleton/" + typeof(T).Name));
                    DontDestroyOnLoad(instance.gameObject);
                }

                return instance;
            }
        }
    }

    private void OnDisable()
    {
        isQuitting = true;
        instance = null;
    }
}
