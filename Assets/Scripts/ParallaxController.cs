using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform[] layers;
    [SerializeField] private float[] xCoeff;
    [SerializeField] private float[] yCoeff;
    [SerializeField] private float[] yCoeffPlus;

    private int LayersCount;

    private void Start()
    {
        LayersCount = layers.Length;
    }

    private void Update()
    {
        for (int i = 0; i < LayersCount; i++)
        {
            layers[i].position = new Vector2(transform.position.x * xCoeff[i], transform.position.y * yCoeff[i] + yCoeffPlus[i]);
        }
    }
}
