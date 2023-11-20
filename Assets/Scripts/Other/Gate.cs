using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Gate : MonoBehaviour
{
    public bool isOpened;
    private bool gateTriggerEnter;
    public float leverArmNumber;
    [SerializeField] private float leverArmNeedNumber;
    private float needNumber;
    [SerializeField] private Animator anim;
    private PlayerInput playerInput;
    [SerializeField] private GameObject gateCanvas;
    [SerializeField] private Text textGate;

    private void Awake()
    {
        playerInput = GameObject.FindObjectOfType<PlayerInput>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            gateTriggerEnter = true;
            if (leverArmNumber >= leverArmNeedNumber)
            {
                StartCoroutine(GateCoroutine());
                gateCanvas.SetActive(true);
                textGate.text = "Нажмите E";
            }
            else
            {
                needNumber = leverArmNeedNumber - leverArmNumber;
                gateCanvas.SetActive(true);
                textGate.text = "Дверь закрыта! Найдите и разблокируйте рычаги! \nОсталось " + needNumber;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        gateTriggerEnter = false;
        gateCanvas.SetActive(false);
        StartCoroutine(CloseGate());
    }

    private void OpenGate()
    {
        isOpened = true;
        anim.SetBool("isOpened", isOpened);
        gateCanvas.SetActive(false);
    }

    private IEnumerator GateCoroutine()
    {
        while (gateTriggerEnter)
        {
            if(playerInput.gateCallRequest)
            {
                OpenGate();
                yield return new WaitForSeconds(5);
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
    }

    private IEnumerator CloseGate()
    {
        yield return new WaitForSeconds(2);
        if(!gateTriggerEnter)
        {
            isOpened = false;
            anim.SetBool("isOpened", isOpened);
        }
        if(gateTriggerEnter)
        {
            yield break;
        }
        
    }
}
