using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTimeManager {

    private static TaskBehaviour mTask;

    static WaitTimeManager() {
        GameObject go = new GameObject("#WaitTimeManager#");
        GameObject.DontDestroyOnLoad(go);
        mTask = go.AddComponent<TaskBehaviour>();
        Debug.Log("WaitTimeManager");
    }

    public static Coroutine WaitTime(float time, Action callback) {
        return mTask.StartCoroutine(coroutine(time, callback));
    }

    static IEnumerator coroutine(float time, Action callback) {
        Debug.Log("Start Wait Time");
        yield return new WaitForSeconds(time);
        Debug.Log("Wait Time end");
        callback?.Invoke();
    }

    class TaskBehaviour : MonoBehaviour { }
}