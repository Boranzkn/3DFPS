using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController characterController;
    private int health = 100;

    // Gravity
    private int gravity = -15;
    private Vector3 gravityVector;

    // Ground Checker
    [SerializeField] private Transform groundCheckerTransform;
    [SerializeField] private LayerMask groundLayer;
    private float groundCheckerRadius = 0.35f;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();  
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        Jump();
        AddGravity();
        MovePlayer();
    }

    private void AddGravity()
    {
        gravityVector.y += gravity * Time.deltaTime;
        characterController.Move(gravityVector * Time.deltaTime * 2);
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            gravityVector.y = Mathf.Sqrt(gravity * -7);
        }
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheckerTransform.position, groundCheckerRadius, groundLayer);
        if (isGrounded && gravityVector.y < 0)
        {
            gravityVector.y = -3;
        }
    }

    private void MovePlayer()
    {
        Vector3 movement = (Input.GetAxis("Horizontal") * transform.right) + (Input.GetAxis("Vertical") * transform.forward);
        characterController.Move(movement * 10 * Time.deltaTime);
    }

    public void GetDamage(int damage)
    {
        health -= damage;

        CheckPlayerDied();
    }

    private void CheckPlayerDied()
    {
        if (health <= 0)
        {
            PlayerDied();
        }
    }

    private void PlayerDied()
    {
        throw new NotImplementedException();
    }
}
