using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour, IWorldSpeedEffect, IDamageable
{
    public static Action<EnemyController> OnEnemyDestroyed;

    [SerializeField] int hp = 1;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sfxDamage;
    [SerializeField] AudioClip sfxDestroy;
    [SerializeField] NavMeshAgent agent;
    private Transform target;
    private float originalSpeed;
    private bool hasTarget;

    public EActorType ActorType => EActorType.Enemy;

    private void Start()
    {
        GameManager.OnWorldSpeedChanged += OnWorldSpeedChanged;
        PlayerController.OnDestroyed += OnPlayerDestroyed;
        if (GameManager.Instance.player != null)
        {
            SetTarget(GameManager.Instance.player.transform);
        }
        
        originalSpeed = agent.speed;
        agent.speed = originalSpeed * GameManager.worldSpeed;
    }

    void Update()
    {
        if (hasTarget)
        {
            agent.destination = target.position;
        }
    }

    public void OnWorldSpeedChanged()
    {
        agent.speed = originalSpeed * GameManager.worldSpeed;
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        if (newTarget != null)
        {
            hasTarget = true;
        }
        else
        {
            hasTarget = false;
        }
    }

    void OnPlayerDestroyed()
    {
        SetTarget(null);
    }

    public void TakeDamage(int amount, EActorType attacker)
    {
        if (attacker == EActorType.Enemy)
        {
            return;
        }

        hp -= amount;
        if (hp <= 0)
        {
            Destroy();
        }
        else
        {
            SoundManager.Instance.PlayeSFX(sfxDamage);
        }
    }

    public void Destroy()
    {
        SoundManager.Instance.PlayeSFX(sfxDestroy);
        Destroy(gameObject);
        OnEnemyDestroyed?.Invoke(this);
    }

    private void OnDestroy()
    {
        GameManager.OnWorldSpeedChanged -= OnWorldSpeedChanged;
        PlayerController.OnDestroyed -= OnPlayerDestroyed;
    }
}
