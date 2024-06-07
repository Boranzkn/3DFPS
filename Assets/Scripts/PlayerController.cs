using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private float gravity = -9.81f;
    private Vector3 gravityVector;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        gravityVector.y += gravity * Time.deltaTime * Time.deltaTime;
        Vector3 movement = (Input.GetAxis("Horizontal") * transform.right) + (Input.GetAxis("Vertical") * transform.forward) + (gravityVector * 10);
        characterController.Move(movement * 10 * Time.deltaTime);
    }
}
