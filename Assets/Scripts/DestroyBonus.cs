using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyBonus : MonoBehaviour
{
    [SerializeField] private GameObject parentObject;

    public void DestroyBonusBullet()
    {
        Destroy(parentObject);
    }
}
