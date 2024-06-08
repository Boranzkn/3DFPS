using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private int health;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetDamage(int damage)
    {
        health -= damage;

        CheckEnemyDied();
    }

    private void CheckEnemyDied()
    {
        if (health <= 0)
        {
            EnemyDied();
        }
    }

    private void EnemyDied()
    {
        Destroy(gameObject);
    }
}
