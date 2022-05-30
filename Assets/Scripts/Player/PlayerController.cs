using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, IDamageable
{
    public static event Action<int> OnHpChanged;
    public static event Action OnDestroyed;

    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;
    public int hp;
    public ParticleSystem deathParticlesPrefab;

    [SerializeField] LayerMask groundDetectMask;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sfxTakeDamage;
    [SerializeField] AudioClip sfxExplode;
    
    CameraManager camMan;

    public EActorType ActorType => EActorType.Player;

    void Awake()
    {
        camMan = CameraManager.Instance;
    }

    void Update()
    {
        LookAtTarget();
        Move();
    }

    #region Movement

    Vector3 lookDirection;
    void LookAtTarget()
    {
        var mouseRay = camMan.MouseRay;
        if (Physics.Raycast(mouseRay, out RaycastHit hit, 75f, groundDetectMask))
        {
            var targetPos = hit.point;
            targetPos.y = transform.position.y;
            lookDirection = (targetPos - transform.position).normalized;
            var newRotation = Quaternion.FromToRotation(Vector3.forward, lookDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, GameManager.worldSpeed * rotationSpeed * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, lookDirection * 50f);
    }

    void Move()
    {
        var inputHl = Input.GetAxisRaw("Horizontal");
        var inputVl = Input.GetAxisRaw("Vertical");
        var newPosition = transform.position;

        if (inputHl > 0f && transform.position.x < camMan.screenRight
            || inputHl < 0f && transform.position.x > camMan.screenLeft)
        {
            newPosition.x += inputHl;
        }

        if (inputVl > 0f && transform.position.z < camMan.screenTop
            || inputVl < 0f && transform.position.z > camMan.screenBot)
        {
            newPosition.z += inputVl;
        }

        transform.position = Vector3.MoveTowards(transform.position, newPosition, GameManager.worldSpeed * moveSpeed * Time.deltaTime);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(GameManager.tagEnemy))
        {
            TakeDamage(1, EActorType.Enemy);
            other.GetComponent<EnemyController>().Destroy();
        }
    }

    public void TakeDamage(int amount, EActorType attacker)
    {
        if (attacker == EActorType.Player)
        {
            return;
        }

        hp -= amount;
        if (hp <= 0)
        {
            hp = 0;
            DestroyPlayer();
        }
        else
        {
            SoundManager.Instance.PlayeSFX(sfxTakeDamage);
        }

        OnHpChanged?.Invoke(hp);
    }

    public void AddHp(int amount)
    {
        hp += amount;
        if (hp > 3)
        {
            hp = 3;
        }

        OnHpChanged?.Invoke(hp);
    }

    void DestroyPlayer()
    {
        SoundManager.Instance.PlayeSFX(sfxExplode);
        Destroy(gameObject);
        Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
        OnDestroyed?.Invoke();
    }
}