using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float fireSpeed;
    [SerializeField] private float destroyTime;
    private Vector2 localDirection;
    [SerializeField] private Rigidbody2D rgb;

    // Start is called before the first frame update

    void Start()
    {
        localDirection = transform.TransformDirection(Vector2.right);
        rgb.velocity = localDirection * fireSpeed;
        Destroy(gameObject, destroyTime);
    }
        
    /// <summary>
    /// Ранее использовал данный метод, но решил отказаться от него ввиду необходимости постоянного апдейта
    /// </summary>
    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector2.right * fireSpeed * Time.deltaTime);
    }
}
