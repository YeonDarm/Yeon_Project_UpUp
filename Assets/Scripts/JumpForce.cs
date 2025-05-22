using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IJumpPower
{
    float GetJumpBoost();
}

public interface IImpulseForce
{
    void AddJumpForce(Rigidbody rb);
}
