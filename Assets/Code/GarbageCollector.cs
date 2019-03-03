using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour {

    public static GarbageCollector Instance { get; set; }

    [SerializeField]
    private float _waitTime;

    private void Awake() {
        if (Instance == null)
            Instance = this;
        else if (Instance != this)
            Destroy(gameObject);
    }

    // FOR SOME REASON, SOME SLICED BITS STOP DISSOLVING AND NEVER REACH THE POINT OF DESCTRUCTION
    public IEnumerator FadeOut(GameObject go, Material[] materials, float duration) {
        yield return new WaitForSeconds(_waitTime);

        while (duration >= 0) {
            float dissolveAmount = 1 / duration;

            foreach(Material mat in materials) {
                mat.SetFloat("Vector1_79115611", Mathf.Clamp01(dissolveAmount));
            }

            if (dissolveAmount >= 1) {
                // Change this to disable if objects are pooled.
                Destroy(go);
                yield break;
            }

            duration -= Time.deltaTime;
            yield return null;
        }
    }
}
