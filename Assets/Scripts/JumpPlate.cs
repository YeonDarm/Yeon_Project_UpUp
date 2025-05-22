using System.Collections;
using System.Collections.Generic;
using Palmmedia.ReportGenerator.Core.CodeAnalysis;
using UnityEngine;

public class JumpPlate : MonoBehaviour, IImpulseForce
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
            AddJumpForce(rigid);
        }
    }

    public void AddJumpForce(Rigidbody rigidbody)
    {
        Debug.Log("점프 파워: " + jumpPlatePower);
        rigidbody.AddForce(Vector3.up * jumpPlatePower, ForceMode.Impulse);
    }
}
