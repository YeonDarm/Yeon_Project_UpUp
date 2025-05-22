using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    [SerializeField] private float useStamina = 1f;
    public float jumpPower;
    public float maxJumpPower;
    public float originJumpPower;
    private bool isCharging = false;
    
    private Vector2 curMovementInput;
    public Rigidbody _rigidbody;
    public Animator anim;

    public LayerMask groundLayerMask;

    [Header("Look")]
    [SerializeField] private Transform cameraContainer;
    [SerializeField] private float minXLook;
    [SerializeField] private float maxXLook;
    private float camPitchRot;
    [SerializeField] private float lookSensitivity;
    private Vector2 mouseDelta;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        anim = GetComponentInChildren<Animator>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //커서를 보이지 않기 위한 코드.    
        if (_rigidbody == null)
        {
            Debug.Log("_rigidbody 할당되지 않음");
        }
    }

    void FixedUpdate() //물리연산, 움직임을 호출하는 경우
    {
        Move();
        Charging();
        if (Mathf.Abs(_rigidbody.velocity.y) < 0.1f && !IsGrounded())
        {
            Debug.Log("최고점 좌표: " + transform.position.y);
        }
    }

    void LateUpdate()
    {
        CameraLook();
    }


    void Move()
    {
        Vector3 dir = (transform.forward * curMovementInput.y
             + transform.right * curMovementInput.x).normalized * moveSpeed;
        
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camPitchRot += mouseDelta.y * lookSensitivity;
        camPitchRot = Mathf.Clamp(camPitchRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(-camPitchRot, 0, 0);

        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivity, 0);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        // bool isMoving = context.ReadValue<Vector2>().magnitude > 0.1f;

        // if (IsGrounded())
        // {
        //     bool isMoving = context.ReadValue<Vector2>().magnitude > 0.1f;
        //     anim.SetBool("IsMoving", isMoving);
        // }
        // else
        // {
        //     anim.StopPlayback();
        // }

        if (context.phase == InputActionPhase.Performed)
        {
            anim.SetBool("Idle", false);
            anim.SetTrigger("IsMove");
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            // anim.ResetTrigger("IsMove");
            anim.SetBool("Idle", true);
            curMovementInput = Vector2.zero;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }


    public void OnChargeJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started) // 버튼 누름
        {
            originJumpPower = jumpPower;
            isCharging = true;
        }
        else if (context.phase == InputActionPhase.Canceled && isCharging) // 버튼을 떼면 실행
        {
            _rigidbody.AddForce(Vector3.up * maxJumpPower, ForceMode.Impulse);
            maxJumpPower = originJumpPower;
            jumpPower = originJumpPower;
            isCharging = false;
        }
    }

    void Charging()
    {
        if (isCharging && CharacterManager.Instance.Player.condition.UseStamina(useStamina))
        {
            jumpPower += Time.deltaTime * 100f;
            jumpPower = Mathf.Clamp(jumpPower, 80f, 200f);
            maxJumpPower = jumpPower;
        }
    }


    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction * 0.56f, Color.red);
            if (Physics.Raycast(rays[i], 0.56f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
