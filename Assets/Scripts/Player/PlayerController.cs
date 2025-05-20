using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float useStamina;
    [SerializeField] private float jumpPower = 80f;
    [SerializeField] private float maxJumpPower = 80f;
    public float fallingSpeed;
    private bool isCharging = false;
    
    private Vector2 curMovementInput;
    private Rigidbody _rigidbody;
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
    }

    void LateUpdate()
    {
        CameraLook();
    }


    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y + Physics.gravity.y * Time.deltaTime;

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
        if (context.phase == InputActionPhase.Performed)
        {
            curMovementInput = context.ReadValue<Vector2>();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            curMovementInput = Vector2.zero;
        }
        //Start : 키 입력되는 순간 한번만 작동.
        // performed: 키가 눌리고 내부 로직이 실행되고 나서 계속.
        //canceled: 취소됐을 때.
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        mouseDelta = context.ReadValue<Vector2>();
    }

    // public void OnJump(InputAction.CallbackContext context)
    // {
    //     Debug.Log("IsGrounded(): " + IsGrounded());

    //     //점프 차징(IsGrounded(ture)) >> 스태미나 닳는다 >> 점프 키 뗌 >> 점프(닳은 스태미나만큼 점프력 상승)
    //     if (context.phase == InputActionPhase.Started && IsGrounded())
    //     {
    //         Debug.Log("점프 Started");
    //     }
    //     else if (context.phase == InputActionPhase.Performed)
    //     {
    //         Debug.Log("점프 Performed");
    //     }
    //     else if (context.phase == InputActionPhase.Canceled)
    //     {
    //         Debug.Log("점프 Canceled");
    //     }
    // }

    public void OnChargeJump(InputAction.CallbackContext context)
    {
        Debug.Log("점프 시스템 on");
        if (context.phase == InputActionPhase.Started && IsGrounded()) // 버튼 누름
        {
            Debug.Log("차징 중...");
            isCharging = true;
            jumpPower = 80f;
        }
        else if (context.phase == InputActionPhase.Canceled && isCharging) // 버튼을 떼면 실행
        {
            Debug.Log("점프 파워!");
            _rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            isCharging = false;
            jumpPower = 80f;
            maxJumpPower = jumpPower;
        }
    }

    void Charging()
    {
        if (isCharging)
        {
            maxJumpPower += Time.deltaTime * 100f;
            maxJumpPower = Mathf.Clamp(maxJumpPower, 0f, 200f);
            jumpPower = maxJumpPower;
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
            Debug.DrawRay(rays[i].origin, rays[i].direction * 0.5f, Color.red);
            if (Physics.Raycast(rays[i], 0.5f, groundLayerMask))
            {
                return true;
            }
        }
        return false;
    }
}
