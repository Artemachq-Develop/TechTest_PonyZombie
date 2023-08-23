using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawnSystem : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public Transform spawnPoint;
    
    private float nextFireTime = 0f;
    public float cooldownDuration = 2f;

    private void Update()
    {
        if (Time.time > nextFireTime)
        {
            nextFireTime = Time.time + cooldownDuration + Random.Range(1f, 3f);
            LeanPool.Spawn(enemyPrefab[Random.Range(0, enemyPrefab.Length)], spawnPoint.position, transform.rotation);
        }
    }
}
