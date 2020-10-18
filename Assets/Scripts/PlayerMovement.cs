using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] AudioClip _playerStep;
    [SerializeField] AudioClip _playerJump;
    [Space(10)]

    public CharacterController controller;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float weight = 1f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    Vector3 velocity;
    bool isGrounded;
    float sprint = 1f;

    bool isStepping;
    float stepSpeed = 0.4f;

    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (Input.GetButton("Run"))
        {
            stepSpeed = 0.3f;
            sprint = 1.5f;
        }
        else
        {
            stepSpeed = 0.4f;
            sprint = 1f;
        }

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        if ((x != 0 || z != 0) && !isStepping && isGrounded)
        {
            StartCoroutine("Step");
        } else if (!isStepping)
        {
            StopCoroutine("Step");
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            AudioSource.PlayClipAtPoint(_playerJump, groundCheck.position);
        }

        velocity.y += gravity * weight * Time.deltaTime;
        Vector3 move = transform.right * x * speed * sprint 
                     + transform.forward * z * speed * sprint
                     + transform.up * velocity.y;
        controller.Move(move * Time.deltaTime);
    }

    IEnumerator Step()
    {
        isStepping = true;
        AudioSource.PlayClipAtPoint(_playerStep, groundCheck.position);
        yield return new WaitForSeconds(stepSpeed);
        isStepping = false;
    }
}
