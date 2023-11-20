using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    [SerializeField] float speed =3;
    private Rigidbody2D rgbPlayer;
    private Animator animPlayer;
    public bool isClimbing;
    [SerializeField] private GameObject groundColliderObject;

    private void Awake()
    {
        rgbPlayer = GameObject.FindFirstObjectByType<PlayerInput>().GetComponent<Rigidbody2D>();
        animPlayer = GameObject.FindObjectOfType<PlayerInput>().GetComponent<Animator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            rgbPlayer.gravityScale = 0;

            if (Input.GetKey(KeyCode.W))
            {
                if (!isClimbing)
                {
                    groundColliderObject.SetActive(false);
                }
                rgbPlayer.velocity = new Vector2(0, speed);
                isClimbing = true;
                animPlayer.SetFloat("climbVelocity", Mathf.Abs(rgbPlayer.velocity.y));
            }
            else if(Input.GetKey(KeyCode.S))
            {
                if (!isClimbing)
                {
                    groundColliderObject.SetActive(false);
                }
                rgbPlayer.velocity = new Vector2(0, -speed);
                isClimbing = true;
                animPlayer.SetFloat("climbVelocity", Mathf.Abs(rgbPlayer.velocity.y));
            }
            else 
            {
                rgbPlayer.velocity = new Vector2(0, 0);
                animPlayer.SetFloat("climbVelocity", Mathf.Abs(rgbPlayer.velocity.y));
                animPlayer.SetBool("isClimbing", isClimbing);
            }
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        groundColliderObject.SetActive(true);
        rgbPlayer.gravityScale = 1;
        isClimbing = false;
        animPlayer.SetBool("isClimbing", isClimbing);
    }
}
