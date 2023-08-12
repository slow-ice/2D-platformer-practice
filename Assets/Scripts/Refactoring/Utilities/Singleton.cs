using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : Singleton<T>, new()
{
    private static T instance;

    public static T Instance {
        get {
            if (instance == null) {
                Debug.LogError("No Instance");
            }
            return instance;
        }
    }

    private void Awake() {
        if (instance == null) {
            instance = (T)this;
        }

        DontDestroyOnLoad(gameObject);
    }

    private void Start() {
        OnSingletonInit();
    }

    public virtual void OnSingletonInit() {

    }
}
