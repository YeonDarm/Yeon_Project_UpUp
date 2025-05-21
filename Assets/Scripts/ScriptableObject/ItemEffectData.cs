using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemEffect", menuName = "Item/Effect")]
public class ItemEffectData : ScriptableObject
{
    public float duration = 5f;
    public float speedBoost = 1.5f;

    public void ApplyToPlayer(PlayerController player)
    {
        player.moveSpeed *= speedBoost;
    }

    public void RemoveFromPlayer(PlayerController player)
    {
        player.moveSpeed /= speedBoost;
    }//ScriptableObject에서 private제한자에는 접근 불가?
}

    

