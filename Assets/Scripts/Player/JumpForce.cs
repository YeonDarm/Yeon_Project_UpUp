using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForce : MonoBehaviour
{
    private Rigidbody rigid;
    public float jumpPower = 5.0f;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision other)
    {
        AddJumpForce();
        // if (other.gameObject.CompareTag("Player"))
        // {
        //     AddJumpForce();
        // }
    }

    void AddJumpForce()
    {
        Debug.Log("JumpPower!!!");
        rigid.velocity = Vector3.zero;
        rigid.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
    }
}
