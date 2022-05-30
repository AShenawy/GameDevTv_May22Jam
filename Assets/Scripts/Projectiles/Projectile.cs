using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public EActorType attackSource;
    public int damageAmount;

    private void OnTriggerEnter(Collider other)
    {
        print("hit!");
        var damageable = other.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.TakeDamage(damageAmount, attackSource);
            Destroy(gameObject);
        }
    }
}
