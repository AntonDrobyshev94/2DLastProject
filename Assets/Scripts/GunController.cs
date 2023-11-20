using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public float offset;
    private Vector3 diference;
    private float rotateZ;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        diference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rotateZ = Mathf.Atan2(diference.y, diference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotateZ + offset);

        Vector3 LocalScale = Vector3.one;

        if (rotateZ > 90 || rotateZ < -90)
        {
            LocalScale.y = -1f;
        }
        else
        {
            LocalScale.y = +1f;
        }

        transform.localScale = LocalScale;
    }
}
