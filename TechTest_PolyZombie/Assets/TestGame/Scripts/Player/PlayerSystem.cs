using Lean.Pool;
using UnityEngine;

public class PlayerSystem : MonoBehaviour
{
    public AudioClip shootAudioClip;

    public GameObject bulletPrefab;
    public Transform bulletPointTransform;

    public ParticleSystem shootParticleSystem;

    [Range(0.2f, 5f)] public float playerSpeed = 2f;
    
    private Animator animator;
    private AudioSource audioSource;
    private UIManager uiManager;
    private void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponentInChildren<AudioSource>();
        uiManager = UIManager.Instance;
        
        animator.SetBool("isWalk", true);
    }

    private void Update()
    {
        Move();
        
        DetectObject();
    }

    private void Move()
    {
        transform.position += transform.right * playerSpeed * Time.deltaTime;
    }

    private void ChangePlayerSpeed(float speed)
    {
        playerSpeed = speed;
        
        if (playerSpeed > 1f)
        {
            animator.speed = playerSpeed - 0.8f;
        }
        else
        {
            animator.speed = 1f;
        }
    }

    private void DetectObject()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Shoot"))
        {
            Collider2D targetObject = Physics2D.OverlapPoint(mousePos);
            if (targetObject != null && targetObject.CompareTag("Enemy"))
            {
                ChangePlayerSpeed(0f);
                animator.SetTrigger("isShoot");
            }
        }
        
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.CompareTag("CompleteLevel"))
            {
                Time.timeScale = 0f;
                uiManager.ShowCompletePanel();
            }
        }
    }

    private void Shoot()
    {
        CameraShake.Instance.ShakeCamera(2f, 0.1f);
        
        GameObject bullet = LeanPool.Spawn(bulletPrefab, bulletPointTransform.position, transform.rotation);
        if (bullet.activeSelf) LeanPool.Despawn(bullet.gameObject, 4f);
        
        
        ParticleSystem muzzle = LeanPool.Spawn(shootParticleSystem, bulletPointTransform.position, transform.rotation);
        if (muzzle.gameObject.activeSelf) LeanPool.Despawn(muzzle.gameObject, 2f);
    }

    public void Die()
    {
        Time.timeScale = 0;
        uiManager.ShowLosePanel();
    }
    
    public void ShootFireEffect()
    {
        audioSource.PlayOneShot(shootAudioClip);
        Shoot();
    }

    public void GetBackPlayerSpeed()
    {
        ChangePlayerSpeed(0f);
        ChangePlayerSpeed(2f);
    }
}
