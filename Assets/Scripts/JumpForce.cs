using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpForce
{
    float GetJumpBoost();
    void ApplyJumpBoost(PlayerController player);
    void RemoveJumpBoost(PlayerController player);
}

public interface IImpulseForce
{
    void AddJumpForce(Rigidbody rb);
}
