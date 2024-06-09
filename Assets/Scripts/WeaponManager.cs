using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    private int minDamage = 40, maxDamage = 100;
    private float range = 300f;
    private EnemyManager enemyManager;
    [SerializeField] private Camera cam;
    [SerializeField] private ParticleSystem muzzleFlash;
    [SerializeField] private GameObject impactEffect;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Fire();
            muzzleFlash.Play();
        }
    }

    private void Fire()
    {
        RaycastHit hit;

        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));

            enemyManager = hit.transform.GetComponent<EnemyManager>();

            if (enemyManager != null)
            {
                enemyManager.GetDamage(UnityEngine.Random.Range(minDamage, maxDamage));
            }
        }
    }
}
