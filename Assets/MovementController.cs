using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    private float horizontal;
    private float vertical;
    private Vector3 playerInput;
    [SerializeField] private CharacterController player;
    [SerializeField] public float m_speed =1f;
    private Vector3 movePlayer;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] public float fallingVelocity;
    [SerializeField] private float jumpForce;
    private float m_runningSpeed = 2f;
    [SerializeField] private Camera m_camera;
    private Vector3 camForward;
    private Vector3 camRight;
    private Animator anim;
    private bool isAttacking = false;
    private bool isBlocking = false;
    private float attackDuration = 0.05f;
    private float attackTimer = 0f;
    private float rayTimer = 0f;
    private float rayCooldown = 2f;
    [SerializeField] private Transform m_raycastPoint;
    [SerializeField] private float m_maxdistance;
    [SerializeField] private LayerMask m_raycastLayers;
    [SerializeField] private float rayDamage = 1f;
    private bool rayAvailable = true;
    //private bool isRunning = false;


    void Start()
    {
        player = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        // Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        playerInput = new Vector3(horizontal, 0, vertical);
        playerInput = Vector3.ClampMagnitude(playerInput, 1);

        camDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * m_speed;

        rotateWithCursor();

        // player.transform.LookAt(player.transform.position + movePlayer);

        SetGravity();
        PlayerActions();
      
        Debug.Log(player.velocity.magnitude);
    }
    //cosas relacionadas al movimiento y gravedad//
    void SetGravity()
    {
        if (player.isGrounded)
        {
            fallingVelocity = -gravity * Time.deltaTime;
            movePlayer.y = fallingVelocity;
        }
        else
        {
            fallingVelocity -= gravity * Time.deltaTime;
            movePlayer.y = fallingVelocity;
        }
    }
    void camDirection()
    {
        camForward = m_camera.transform.forward;
        camRight = m_camera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }
    //llama a las acciones que realizara el jugador//
    void PlayerActions()
    {
        if(player.isGrounded && Input.GetButtonDown("Jump"))
        {
            Jump();
        }
        PlayerMove();
        if (Input.GetKey(KeyCode.Mouse0))
        {
            Attack();
        }

        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0F)
            {
                isAttacking = false;
                anim.SetBool("IsAttacking", false);
            }
        }
        rayLauncher();
    }
    //Acciones que puede realizar el jugador//
    void PlayerMove()
    {
        if (Input.GetKey(KeyCode.LeftShift) && !isBlocking)
        {
            player.Move(movePlayer * Time.deltaTime * m_runningSpeed);
        }
        else
        {
            player.Move(movePlayer * Time.deltaTime);
        }
        var isMoving = player.velocity.magnitude > 0.2f;
        if (isMoving)
        {
            anim.SetBool("IsMoving", true);
        }
        if (!isMoving)
        {
            anim.SetBool("IsMoving", false);
        }
    }
    void Attack()
    {
        isAttacking = true;
        attackTimer = attackDuration;
        anim.SetBool("IsAttacking", true);
    }

    void Jump()
    {
        fallingVelocity = jumpForce;
        movePlayer.y = fallingVelocity;
    }

    void Block()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isBlocking = true;
            m_speed /= 2;
            anim.SetBool("isBlocking", true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            isBlocking = false;
            m_speed *= 2;
            anim.SetBool("isBlocking", false);
        }
    }

    void rayLauncher()
    {
        if (!rayAvailable)
        {
            rayTimer += Time.deltaTime;
            if (rayTimer >= rayCooldown)
            {
                rayAvailable = true;
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            isBlocking = true;
            m_speed /= 2;
            anim.SetBool("isBlocking", true);
        }
        if (Input.GetMouseButton(1))
        {
            bool l_isHitting = Physics.Raycast(m_raycastPoint.position, m_raycastPoint.forward, out RaycastHit l_hit, m_maxdistance, m_raycastLayers);
            Debug.DrawLine(m_raycastPoint.position, m_raycastPoint.forward);
            if (l_isHitting)
            {
                if (l_hit.collider.TryGetComponent(out Enemy l_enemy) && rayAvailable)
                {
                    l_enemy.getDamage(rayDamage);
                    Debug.Log("El enemigo recibe daño");
                    rayAvailable = false;
                }
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            isBlocking = false;
            m_speed *= 2;
            anim.SetBool("isBlocking", false);
        }
    }
    void rotateWithCursor()
    {
        Ray cameraRay = m_camera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLenght;
        if (groundPlane.Raycast(cameraRay, out rayLenght))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLenght);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            player.transform.LookAt(new Vector3(pointToLook.x, player.transform.position.y, pointToLook.z));
        }

    }
}
