using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {
    Animator animator;

    [SerializeField] Camera playerCamera;
    [SerializeField] Transform projectileSpawn;
    [SerializeField] Rigidbody projectilePrefab;
    [SerializeField] float projectileForce = 10.0f;

    [SerializeField] float moveSpeed = 0;
    [SerializeField] float moveSpeedMultiplier = 1;
    [SerializeField] float gravity = 9.81f;
    [SerializeField] float jumpSpeed = 10.0f;

    [SerializeField] HealthBar healthBar;
    [SerializeField] float maxHealth = 100;

    CharacterController controller;

    Coroutine speedChange;

    Vector3 moveVel = new Vector3(0, 0, 0);
    Vector3 curMoveInput;
    Vector2 move;

    private float _health = 100;
    public float health {
        get { return _health; }
        set {
            if (value <= 0) {
                GameManager.Instance.GameOver();
            }

            
            _health = value;
            if (_health > maxHealth) {
                _health = maxHealth;
            }

            healthBar.SetHealth(_health);
            Debug.Log("Health is:" + health.ToString());
        }
    }


    // Start is called before the first frame update
    void Start() {
        healthBar.SetHealth(_health);
        try {
            controller = GetComponent<CharacterController>();
            controller.minMoveDistance = 0.0f;

            animator = GetComponentInChildren<Animator>();

            if (!animator) {
                Debug.Log("No animation component in child for the gameobject " + gameObject.name);
            }

            if (moveSpeed <= 0.0f) {
                moveSpeed = 6.0f;
                throw new ArgumentNullException("Movespeed Argument is null so the value has been defaulted to 6.0");
            }
        } catch (ArgumentNullException e) {
            Debug.Log(e.Message);
        }

    }

    // Update is called once per frame
    void Update() {
        CalculateMovement();
        curMoveInput.y -= gravity * Time.deltaTime;
        controller.Move(curMoveInput * Time.deltaTime);

        animator.SetFloat("Forward", move.y);
        animator.SetFloat("Right", move.x);
    }

    public void MovePlayer(InputAction.CallbackContext context) {
        //Debug.Log("Move vector is: " + context.action.ReadValue<Vector2>());
        move = context.action.ReadValue<Vector2>();
        move.Normalize();

        moveVel = new Vector3(move.x, 0, move.y);
        
    }

    void CalculateMovement() {
        curMoveInput = moveVel * moveSpeed * moveSpeedMultiplier;
        if (controller.isGrounded) {
            curMoveInput = transform.TransformDirection(curMoveInput);
        }
    }


    public void StartSpeedChange() {
        if (speedChange != null) {
            StopCoroutine(speedChange);
            speedChange = null;
            moveSpeedMultiplier = 1;
        }
        speedChange = StartCoroutine(SpeedChange());
    }

    IEnumerator SpeedChange() {
        moveSpeedMultiplier = 2;
        yield return new WaitForSeconds(5.0f);
        speedChange = null;
        moveSpeedMultiplier = 1;
    }

    public void Fire(InputAction.CallbackContext context) {
        if (context.action.WasPressedThisFrame()) {
            if (projectilePrefab && projectileSpawn) {
                Rigidbody temp = Instantiate(projectilePrefab, projectileSpawn.position, projectileSpawn.rotation);
                temp.AddForce(projectileSpawn.forward * projectileForce, ForceMode.Impulse);

                Destroy(temp.gameObject, 2.0f);
            }
        }
    }

    public void Punch(InputAction.CallbackContext context) {
        //Debug.Log("Move vector is: " + context.action.ReadValue<Vector2>());
        if (context.action.WasPressedThisFrame()) {
            animator.SetTrigger("Punch");
        }
    }

    public void Kick(InputAction.CallbackContext context) {
        //Debug.Log("Move vector is: " + context.action.ReadValue<Vector2>());
        if (context.action.WasPressedThisFrame()) {
            animator.SetTrigger("Kick");
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.transform.tag == "SlowArea") {
            moveSpeedMultiplier = 0.5f;
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.gameObject.transform.tag == "SlowArea") {
            moveSpeedMultiplier = 1;
        }
    }

    private void OnTriggerStay(Collider other) {
        if (other.gameObject.transform.tag == "DamageArea") {
            health -= 0.5f;
        }
    }
}