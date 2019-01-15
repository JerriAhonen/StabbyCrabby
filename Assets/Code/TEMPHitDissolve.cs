using System.Collections;
using UnityEngine;

public class TEMPHitDissolve : MonoBehaviour {

    Renderer rend;
    public float smooth = 0.25f;

    public Material[] mats;
    
	// Use this for initialization
	void Start () {
        //rend = GetComponent<Renderer>();
        //rend.material.shader = Shader.Find("Dissolve1");
        foreach (Material mat in mats)
        {
            mat.SetFloat("Vector1_79115611", -10);
        }
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartCoroutine(StartDissolve(2));
        }	
	}

    IEnumerator StartDissolve(float duration)
    {
        while (duration >= 0)
        {
            float dissolveAmount = 1 / duration;
            //rend.material.SetFloat("Vector1_79115611", dissolveAmount);

            foreach (Material mat in mats)
            {
                mat.SetFloat("Vector1_79115611", Mathf.Clamp01(dissolveAmount));
            }

            if (dissolveAmount >= 1)
            {
                // Change this to disable if objects are pooled.
                Destroy(gameObject);
                yield break;
            }
            
            duration -= Time.deltaTime;
            yield return null;
        }
        
    }
}
