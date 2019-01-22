using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIElementLerp : MonoBehaviour {

    RectTransform rectTransform;
    Vector3 startPos;
    //Vector3 endPos;

    //float timeOfTravel = 5; //time after object reach a target place 
    float currentTime = 0; // actual floting time 
    float normalizedValue;
    

    

    // Use this for initialization
    void Start ()
    {
        //getting reference to this component 
        rectTransform = GetComponent<RectTransform>();
        startPos = transform.position;
    }
	
    public IEnumerator LerpObject(Vector3 endPos, float timeOfTravel)
    {
        while (currentTime <= timeOfTravel)
        {
            currentTime += Time.deltaTime;
            normalizedValue = currentTime / timeOfTravel; // we normalize our time 

            rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, normalizedValue);
            yield return null;
        }
    }
}
