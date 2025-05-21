using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemEffectObject : MonoBehaviour
{
    public ItemEffectData itemEffect;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(ItemEffectActive(other.GetComponent<PlayerController>()));
            Destroy(gameObject);
        }
    }

    IEnumerator ItemEffectActive(PlayerController player)
    {
        itemEffect.ApplyToPlayer(player);
        Debug.Log($"아이템 효과 적용. 지속 시간: {itemEffect.Duration}");
        yield return new WaitForSeconds(itemEffect.Duration);
    }
}
