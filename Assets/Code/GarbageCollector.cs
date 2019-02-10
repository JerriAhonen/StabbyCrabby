using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour {

    public static GarbageCollector Instance { get; set; }

    [SerializeField]
    private float _waitTime;

    private void Awake() {
        if(Instance == null)
            Instance = this;
        else if(Instance != this)
            Destroy(gameObject);
    }

    public IEnumerator FadeOut(GameObject go) {
        Debug.Log("HERE");

        yield return new WaitForSeconds(_waitTime);

        Destroy(go);
    }
}
