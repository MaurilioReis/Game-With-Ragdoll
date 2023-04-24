using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;

    public JoystickMove joystick;
    public Transform cameraTransform; // Referência para a câmera
    private Rigidbody rb;

    public float rangeGround = 3;
    public LayerMask maskGround;

    [HideInInspector]
    public bool isGrounded;
    public bool idle;

    [HideInInspector]
    public Vector3 moveDirection;

    Animator anim;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Punch"))
        {
            // Verifica se o jogador está no chão
            isGrounded = Physics.Raycast(transform.position, Vector3.down, rangeGround, ~maskGround);

            // Movimento horizontal
            float horizontal = joystick.directionJoystick.x;
            // Movimento vertical
            float vertical = joystick.directionJoystick.y;

            // Rotação baseada na câmera
            moveDirection = new Vector3(horizontal, 0f, vertical);
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(cameraForward);

            // Normaliza a direção do movimento
            if (moveDirection.magnitude > 0.1f)
            {
                moveDirection.Normalize();

                Vector3 camRight = cameraTransform.right;
                camRight.y = 0;

                // Calcula a nova rotação baseada na direção do movimento e na orientação da câmera
                Quaternion newRotationFreeRot = Quaternion.LookRotation(cameraForward * moveDirection.z + camRight * moveDirection.x);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotationFreeRot, Time.deltaTime * 10f);
            }

            // Normaliza a direção do movimento se estiver diagonal
            if (moveDirection.magnitude > 1f)
            {
                moveDirection.Normalize();
            }

            // Move o jogador
            Vector3 moveVelocity = moveDirection * moveSpeed;
            moveVelocity = newRotation * moveVelocity;

            if (moveDirection.magnitude > 0.5f)
            {
                rb.velocity = new Vector3(moveVelocity.x, rb.velocity.y, moveVelocity.z);
            }
            else
            {
                rb.velocity = new Vector3(0, rb.velocity.y, 0);
            }
        }
    }
}