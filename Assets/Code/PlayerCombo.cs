using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{

    public float fireRate;
    string[] comboParams;
    int comboIndex = 0;
    Animator animator;
    float resetTimer;

    void Awake()
    {
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
            comboParams = new string[] { "Attack 1", "Attack 2" };

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        // Reset combo if the user has not clicked quickly enough
        if (comboIndex > 0)
        {
            resetTimer += Time.deltaTime;
           
            if (resetTimer > fireRate)
            {
                animator.SetTrigger("Reset");
                comboIndex = 0;
            }
        }
        if (Input.GetButtonDown("Fire1") && comboIndex < comboParams.Length)
        {
            
           
            
            animator.SetTrigger(comboParams[comboIndex]);
            
            // If combo must not loop
            //comboIndex++;

            // If combo can loop
            comboIndex = (comboIndex + 1) % comboParams.Length;
            if(comboIndex >= 1)
            {
                comboIndex = 1;
            }
            resetTimer = 0f;
        }
        
    }
 
}
