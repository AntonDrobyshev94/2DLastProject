using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenLeverArm : MonoBehaviour
{
    private PlayerInput playerInput;
    private bool leverArmTriggerEnter;
    private bool isOpenLeverArm;
    private Animator anim;
    [SerializeField] private Gate gateScriptObject;
    [SerializeField] private GameObject gateCanvas;
    [SerializeField] private Text textGate;
    public bool isLeverArmActive;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        playerInput = GameObject.FindObjectOfType<PlayerInput>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(!isLeverArmActive)
            {
                leverArmTriggerEnter = true;
                StartCoroutine(LeverArmCoroutine());
                gateCanvas.SetActive(true);
                textGate.text = "ֽאזלטעו E";
                
            } 
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            leverArmTriggerEnter = false;
            gateCanvas.SetActive(false);
        }
            
    }

    private void ToggleLeverArm()
    {
        isOpenLeverArm = true;
        anim.SetBool("isOpenLeverArm", isOpenLeverArm);
        gateCanvas.SetActive(false);
        isLeverArmActive = true;
    }

    private IEnumerator LeverArmCoroutine()
    {
        while (leverArmTriggerEnter)
        {
            if (playerInput.gateCallRequest)
            {
                ToggleLeverArm();
                yield return new WaitForSeconds(5);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    public void LeverArmOpen()
    {
        gateScriptObject.leverArmNumber += 1;
    }
}
