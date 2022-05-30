using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class EnemyController : MonoBehaviour, IWorldSpeedEffect
{
    [SerializeField] int hp = 1;
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sfxDamage;
    [SerializeField] AudioClip sfxDestroy;
    [SerializeField] NavMeshAgent agent;
    private Transform target;
    private float originalSpeed;

    private void Start()
    {
        GameManager.OnWorldSpeedChanged += OnWorldSpeedChanged;
        target = GameManager.Instance.player.transform;
        originalSpeed = agent.speed;
        agent.speed = originalSpeed * GameManager.worldSpeed;
    }

    void Update()
    {
        agent.destination = target.position;
    }

    public void OnWorldSpeedChanged()
    {
        agent.speed = originalSpeed * GameManager.worldSpeed;
    }

    public void TakeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy();
        }
        else
        {
            audioSource.PlayOneShot(sfxDamage);
        }
    }

    public void Destroy()
    {
        audioSource.PlayOneShot(sfxDestroy);
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        GameManager.OnWorldSpeedChanged -= OnWorldSpeedChanged;
    }
}
