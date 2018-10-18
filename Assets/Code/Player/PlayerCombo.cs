using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombo : MonoBehaviour
{
    public float fireRate;
    public string[] comboParams;
    public int comboIndex = 0;

    Animator animator;
    public float resetTimer;

    public UIControl uiControl;

    private PlayerCombat playerCombat;

    void Awake()
    {                                           // onko && tarpeellinen?
        if (comboParams == null || (comboParams != null && comboParams.Length == 0))
            comboParams = new string[] { "Attack 1", "Attack 2" };
    }

    void Start()
    {
        playerCombat = PlayerCombat.Instance;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        
        if (Input.GetButtonDown("Fire1") && comboIndex < comboParams.Length && playerCombat.canAttack)
        {
            
            animator.SetTrigger(comboParams[comboIndex]);
           
            // If combo must not loop
            //comboIndex++;

            // If combo can loop
            comboIndex = (comboIndex + 1) % comboParams.Length;
           
            resetTimer = 0f;
        }
        /*if(comboIndex == 0 && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack 1"))
        {
            animator.SetTrigger("Attack 2");
        }*/
       
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
    }
 
}
