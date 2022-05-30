using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static event Action OnWorldSpeedChanged;

    public static GameManager Instance { get; private set; }
    public static float worldSpeed = 1f;

    public PlayerController player;
    public UIManager uiMan;

    public const string tagPlayer = "Player";
    public const string tagEnemy = "Enemy";
    public const string level1 = "Level";


    void Awake()
    {
        Instance = this;
        PlayerController.OnDestroyed += GameOver;
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

    public void NewGame()
    {
        //player.hp = 3;
        //player.transform.position = new Vector3(0f, 1f, 0f);
        //uiMan.NewGame();
        worldSpeed = 1;
        SceneManager.LoadScene(level1);
    }

    public void GameOver()
    {
        StopCoroutine(nameof(SpeedUp));
        StartCoroutine(nameof(SlowDown));
        uiMan.GameOver();
    }

    private void OnDestroy()
    {
        PlayerController.OnDestroyed -= GameOver;
    }
}
