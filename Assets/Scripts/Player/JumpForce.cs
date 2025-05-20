using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForce : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private float jumpPlatePower = 300f;
    public PlayerController player;
    float playerFallingSpeed;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        player = GetComponent<PlayerController>();
    }


    void OnCollisionEnter(Collision other)
    {
        if (player != null)
        {
            playerFallingSpeed = player.fallingSpeed;
            Debug.Log($"낙하 최고 속도: {playerFallingSpeed}");
        }

        if (other.gameObject.CompareTag("Player"))
        {
            AddJumpForce();
        }
    }

    void AddJumpForce()
    {
        rigid.velocity = Vector3.zero;
        float adjustJumpPower = jumpPlatePower + Mathf.Abs(playerFallingSpeed) * 100f;

        rigid.AddForce(Vector3.up * adjustJumpPower, ForceMode.Impulse);
    }
}
