using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    EActorType ActorType { get; }
    void TakeDamage(int amount, EActorType attacker);
}
