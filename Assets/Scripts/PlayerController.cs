using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    private Vector2 curMovementInput;
    private Rigidbody _rigidbody;

    [Header("Look")]
    public Transform cameraContainer;
    public float minXLook;
    public float maxXLook;
    private float camPitchRot;
    public float lookSensitivity;
    private Vector2 mouseDelta;


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; //커서를 보이지 않기 위한 코드.    
    }

    void FixedUpdate() //물리연산, 움직임을 호출하는 경우
    {
        Move();   
    }

    void LateUpdate()
    {
        CameraLook();
    }


    void Move()
    {
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        dir *= moveSpeed;
        dir.y = _rigidbody.velocity.y;

        _rigidbody.velocity = dir;
    }

    void CameraLook()
    {
        camPitchRot -= mouseDelta.y * lookSensitivity;
        camPitchRot = Mathf.Clamp(camPitchRot, minXLook, maxXLook);
        cameraContainer.localEulerAngles = new Vector3(camPitchRot, 0, 0);

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
    
}
