using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManager : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Player")
        {
            PlayerController playerController = collision.transform.GetComponent<PlayerController>();
            playerController.GetDamage(25);
            Destroy(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject, 1f);
        }
    }
}
