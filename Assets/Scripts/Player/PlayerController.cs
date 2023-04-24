using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 10f;

    public JoystickMove joystick;
    public Transform cameraTransform; // Refer�ncia para a c�mera
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
            // Verifica se o jogador est� no ch�o
            isGrounded = Physics.Raycast(transform.position, Vector3.down, rangeGround, ~maskGround);

            // Movimento horizontal
            float horizontal = joystick.directionJoystick.x;
            // Movimento vertical
            float vertical = joystick.directionJoystick.y;

            // Rota��o baseada na c�mera
            moveDirection = new Vector3(horizontal, 0f, vertical);
            Vector3 cameraForward = cameraTransform.forward;
            cameraForward.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(cameraForward);

            // Normaliza a dire��o do movimento
            if (moveDirection.magnitude > 0.1f)
            {
                moveDirection.Normalize();

                Vector3 camRight = cameraTransform.right;
                camRight.y = 0;

                // Calcula a nova rota��o baseada na dire��o do movimento e na orienta��o da c�mera
                Quaternion newRotationFreeRot = Quaternion.LookRotation(cameraForward * moveDirection.z + camRight * moveDirection.x);
                transform.rotation = Quaternion.Slerp(transform.rotation, newRotationFreeRot, Time.deltaTime * 10f);
            }

            // Normaliza a dire��o do movimento se estiver diagonal
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