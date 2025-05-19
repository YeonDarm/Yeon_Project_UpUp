using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpForce : MonoBehaviour
{
    private Rigidbody rigid;
    public float jumpPower;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("JumpPower!!!");
            
            rigid.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
        }
    }   
}
