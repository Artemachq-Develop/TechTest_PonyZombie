using System;
using Lean.Pool;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    private float bulletSpeed = 6f;
    public float bulletDamage = 100f;

    private void Update()
    {
        transform.position += transform.right * bulletSpeed * Time.deltaTime;
        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.2f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("Enemy"))
            {
                hitCollider.GetComponent<EnemySystem>().Damage(bulletDamage);
                LeanPool.Despawn(this.gameObject);
            }
        }
    }
}
