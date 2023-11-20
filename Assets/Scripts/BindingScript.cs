using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BindingScript : MonoBehaviour
{
    [SerializeField] private Transform offset;
    [SerializeField] private Vector3 position;
    // Start is called before the first frame update

    private void Update()
    {
        transform.position = offset.position + position;
    }
}
