using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySystem : MonoBehaviour
{
    private float Health = 100f;
    private float enemySpeed;
    public GameObject dieParticle;
    private bool isHitPlayer = false;


    private void Start()
    {
        enemySpeed = 2f + Random.Range(0.2f, 1f);
    }

    private void Update()
    {
        transform.position += -transform.right * enemySpeed * Time.deltaTime;

        if (!isHitPlayer)
        {
            Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.CompareTag("Player"))
                {
                    isHitPlayer = true;
                    hitCollider.GetComponent<PlayerSystem>().Die();
                }
            }
        }
    }

    public void Damage(float damage)
    {
        if (Health - damage > 0)
        {
            Health -= damage;
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        GameObject spawnDieParticle = LeanPool.Spawn(dieParticle, new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z), transform.rotation);
        LeanPool.Despawn(this.gameObject);
    }
}
