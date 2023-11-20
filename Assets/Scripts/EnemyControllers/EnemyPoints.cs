using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPoints : MonoBehaviour
{
    private GameObject enemyCanvas;
    [SerializeField] private Health health;

    private void Awake()
    {
        enemyCanvas = GetComponent<GameObject>();
    }
}
