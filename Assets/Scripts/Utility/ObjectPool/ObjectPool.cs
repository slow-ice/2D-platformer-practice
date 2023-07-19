using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    private static ObjectPool instance;

    public static ObjectPool Instance {
        get { return instance; }
        private set { }
    }

    protected Queue<GameObject> Pool;

    public GameObject objectPrefab;

    public void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public virtual void Init() {

    }
}
