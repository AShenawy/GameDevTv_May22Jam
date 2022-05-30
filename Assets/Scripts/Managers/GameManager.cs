using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static event Action OnWorldSpeedChanged;

    public static GameManager Instance { get; private set; }
    public static float worldSpeed = 1f;

    public PlayerController player;

    public const string tagPlayer = "Player";
    public const string tagEnemy = "Enemy";

    void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            StopCoroutine(nameof(SpeedUp));
            StartCoroutine(nameof(SlowDown));
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            StopCoroutine(nameof(SlowDown));
            StartCoroutine(nameof(SpeedUp));
        }
    }

    IEnumerator SlowDown()
    {
        while (worldSpeed > 0f)
        {
            worldSpeed -= 0.05f;
            OnWorldSpeedChanged?.Invoke();
            //print("Current world speed: " + worldSpeed);
            yield return new WaitForSeconds(0.1f);
        }

        if (worldSpeed < 0f)
        {
            worldSpeed = 0f;
        }
    }

    IEnumerator SpeedUp()
    {
        while (worldSpeed < 1f)
        {
            worldSpeed += 0.05f;
            OnWorldSpeedChanged?.Invoke();
            //print("Current world speed: " + worldSpeed);
            yield return new WaitForSeconds(0.1f);
        }

        if (worldSpeed > 1f)
        {
            worldSpeed = 1f;
        }
    }
}
