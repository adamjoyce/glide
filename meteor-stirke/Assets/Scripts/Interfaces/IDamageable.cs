using System.Collections;
using UnityEngine;

public interface IDamageable<T>
{
    /* Damages the implementing object the given amount. */
    void TakeDamage(T damageAmount);
}
