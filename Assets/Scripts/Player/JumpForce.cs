using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForce : MonoBehaviour
{
    private Rigidbody rigid;
    [SerializeField] private float jumpPlatePower;


    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            AddJumpForce();
        }
    }

    void AddJumpForce()
    {
        rigid.AddForce(Vector3.up * jumpPlatePower, ForceMode.Impulse);
    }
}
