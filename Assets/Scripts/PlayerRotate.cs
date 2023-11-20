using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotate : MonoBehaviour
{
    private Vector3 position;
    private Camera main;
    public bool isRightFlip;

    // Start is called before the first frame update
    void Start()
    {
        main = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        position = main.WorldToScreenPoint(transform.position);
        FlipPlayer();
    }

    private void FlipPlayer()
    {
        if(Input.mousePosition.x < position.x)
        {
            isRightFlip = false;
            transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if(Input.mousePosition.x > position.x)
        {
            isRightFlip = true;
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        }
    }

}
