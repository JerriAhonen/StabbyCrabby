using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{

    public float fireRate = 0.5f;

    public string[] comboParams;
    private int comboIndex = 0;
    private Animator animator;
    private float resetTimer;
    void Awake()
    {
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
            comboParams = new string[] { "Attack 1", "Attack 2" };

        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && comboIndex < comboParams.Length)
        {
            Debug.Log(comboParams[comboIndex] + " triggered");
            animator.SetTrigger(comboParams[comboIndex]);

            // If combo must not loop
           // comboIndex++;

            // If combo can loop
             comboIndex = (comboIndex + 1) % comboParams.Length;

            resetTimer = 0f;
        }
        // Reset combo if the user has not clicked quickly enough
        if (comboIndex > 0)
        {
            resetTimer += Time.deltaTime;
            Debug.Log(fireRate);
            if (resetTimer > fireRate)
            {
                animator.SetTrigger("Reset");
                comboIndex = 0;
            }
        }
    }
}