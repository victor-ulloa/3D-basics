using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Singelton<T> : MonoBehaviour where T : Component {
    static T instance;
    public static T Instance {
        get {
            if (!instance)
                instance = FindObjectOfType<T>();

            if (!instance) {
                GameObject obj = new GameObject();
                obj.name = typeof(T).Name;
                instance = obj.AddComponent<T>();
            }

            return instance;
        }
    }

    protected virtual void Awake() {
        if (!instance) {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }
}