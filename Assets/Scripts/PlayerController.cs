using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private GameManager gameManager;

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

    // UI
    [SerializeField] private Slider healthSlider;
    [SerializeField] private TMP_Text healthText;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();  
        gameManager = FindObjectOfType<GameManager>();
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
        healthSlider.value = health;
        healthText.text = health.ToString();

        CheckPlayerDied();
    }

    private void CheckPlayerDied()
    {
        if (health <= 0)
        {
            PlayerDied();
            healthSlider.value = 0;
            healthText.text = "0";
        }
    }

    private void PlayerDied()
    {
        gameManager.RestartGame();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finisher"))
        {
            gameManager.WinLevel();
        }
    }
}
