using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlate : MonoBehaviour, IImpulseForce
{
    private Rigidbody rigid;
    [SerializeField] private float jumpPlatePower;
    [SerializeField] private float bounceJumpPower;
    [SerializeField] private float originJumpPower;


    // void Start()
    // {
    //     rigid = GetComponent<Rigidbody>();
    // }


    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            PlayerController player = other.gameObject.GetComponent<PlayerController>();
            // if (other.gameObject.CompareTag("Player"))
            // {
            //     AddJumpForce();
            // }
            if (player != null)
            {
                ApplyJumpBoost(player);
                StartCoroutine(RemoveBoostAfterDelay(player));
            }
        }
    }

    IEnumerator RemoveBoostAfterDelay(PlayerController player)
    {
        yield return new WaitForSeconds(5f);
        RemoveJumpBoost(player);
    }

    void AddJumpForce(Rigidbody rigid)
    {
        Debug.Log("점프 파워: " + jumpPlatePower);
        rigid.AddForce(Vector3.up * jumpPlatePower, ForceMode.Impulse);
    }

    // public float GetJumpBoost() => bounceJumpPower;

    // public void ApplyJumpBoost(PlayerController player)
    // {
    //     originJumpPower = player.jumpPower;
    //     player.maxJumpPower *= bounceJumpPower;
    // }

    // public void RemoveJumpBoost(PlayerController player)
    // {
    //     player.maxJumpPower = originJumpPower;
    // }
}
