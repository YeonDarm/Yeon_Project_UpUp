using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForce : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private float jumpPlatePower = 300f;
    public PlayerController player;
    float playerFallingSpeed; // 떨어지는 속도
    float lastFallingSpeed;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision other) // 충돌체에 대한 모든 정보가 담겨 있다.
    {
        if (other.gameObject.CompareTag("Player"))
        {
            rigid.velocity = new Vector3(rigid.velocity.x, 0f, rigid.velocity.z);
            player = other.gameObject.GetComponent<PlayerController>(); //other를 통해 컴포넌트를 찾기.

            playerFallingSpeed = Mathf.Max(lastFallingSpeed, Mathf.Abs(player.fallingSpeed));
            Debug.Log("낙하 최고 속도: " + playerFallingSpeed);
            AddJumpForce();
        }
    }

    void AddJumpForce()
    {
        float adjustJumpFall = Mathf.Max(jumpPlatePower, playerFallingSpeed) * 1.5f;

        rigid.AddForce(Vector3.up * adjustJumpFall, ForceMode.Impulse);
    }
}
