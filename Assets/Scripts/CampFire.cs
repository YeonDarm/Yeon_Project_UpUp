using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CampFire : MonoBehaviour
{
    //데미지를 얼마 줄건지
    //얼마나 자주 줄건지

    [SerializeField] private int damage;
    [SerializeField] private float damageRate;
    List<IDamagalbe> things = new List<IDamagalbe>();

    void Start()
    {
        InvokeRepeating("DealDamage", 0f, damageRate);
    }

    void DealDamage()
    {
        for (int i = 0; i < things.Count; i++)
        {
            things[i].TakePhysicalDamage(damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out IDamagalbe damagalbe))
        {
            things.Add(damagalbe);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out IDamagalbe damagalbe))
        {
            things.Remove(damagalbe);
        }
    }
}
