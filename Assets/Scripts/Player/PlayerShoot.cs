using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour, IWorldSpeedEffect
{
    public GameObject muzzle;
    public GameObject projectilePrefab;
    public float shootRate = 0.5f;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip sfxShoot;

    private bool canShoot = true;
    private IEnumerator shootCR;

    private void Awake()
    {
        GameManager.OnWorldSpeedChanged += OnWorldSpeedChanged;
    }

    void Update()
    {
        if (!canShoot)
        {
            return;
        }

        if (Input.GetButton("Fire1") || Input.GetButton("Jump"))
        {
            if (shootCR == null)
            {
                shootCR = RepeatShootCR();
                StartCoroutine(shootCR);
            }
        }

        if (Input.GetButtonUp("Fire1") || Input.GetButtonUp("Jump"))
        {
            StopShooting();
        }
    }

    IEnumerator RepeatShootCR()
    {
        while (true)
        {
            if (!canShoot)
            {
                break;
            }
            Shoot();
            yield return new WaitForSeconds(shootRate * GameManager.worldSpeed);
        }
    }

    void Shoot()
    {
        Instantiate(projectilePrefab, muzzle.transform.position, transform.rotation);
        audioSource.PlayOneShot(sfxShoot);
    }

    void StopShooting()
    {
        if (shootCR != null)
        {
            StopCoroutine(shootCR);
            shootCR = null;
        }
    }

    public void OnWorldSpeedChanged()
    {
        if (GameManager.worldSpeed > 0f)
        {
            canShoot = true;
        }
        else
        {
            canShoot = false;
            StopShooting();
        }
    }

    private void OnDestroy()
    {
        GameManager.OnWorldSpeedChanged -= OnWorldSpeedChanged;
    }
}
