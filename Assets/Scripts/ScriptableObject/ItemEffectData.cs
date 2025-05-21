using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItemEffect", menuName = "Item/Effect")]
public class ItemEffectData : ScriptableObject
{
    [SerializeField] private string itemName;
    public string ItemName { get { return itemName; }}
    [SerializeField] private float duration;
    public float Duration { get { return duration; }}
    [SerializeField] private float speedBoost;
    public float SpeedBoost { get { return speedBoost; }}


    public void ApplyToPlayer(PlayerController player)
    {
        player.moveSpeed *= speedBoost;
        player.StartCoroutine(effectClose(player));
    }

    private IEnumerator effectClose(PlayerController player)
    {
        float remainingTime = duration;
        while (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            Debug.Log($"남은 효과 시간: {remainingTime}초");
            yield return null;
        }
        RemoveFromPlayer(player);
    }

    public void RemoveFromPlayer(PlayerController player)
    {
        player.moveSpeed /= speedBoost;
    }
}

    

