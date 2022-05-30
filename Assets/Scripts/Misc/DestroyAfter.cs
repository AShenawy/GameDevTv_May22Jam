using System.Collections;
using UnityEngine;

public class DestroyAfter : MonoBehaviour, IWorldSpeedEffect
{
    public float delay = 3f;

    float timeRemaining;
    bool isCounting = true;

    void Start()
    {
        GameManager.OnWorldSpeedChanged += OnWorldSpeedChanged;
        timeRemaining = delay;
        StartCoroutine(nameof(DelayedDestroy), delay);
    }

    IEnumerator DelayedDestroy(float delayTime)
    {
        yield return new WaitForSeconds(delayTime / GameManager.worldSpeed);
        Destroy(gameObject);
    }

    private void Update()
    {
        if (isCounting && timeRemaining > 0f)
        {
            timeRemaining -= Time.deltaTime * GameManager.worldSpeed;
        }
    }

    public void OnWorldSpeedChanged()
    {
        if (GameManager.worldSpeed > 0f)
        {
            StopAllCoroutines();
            isCounting = true;
            StartCoroutine(nameof(DelayedDestroy), timeRemaining);
        }
        else
        {
            StopAllCoroutines();
            isCounting = false;
        }
    }

    private void OnDestroy()
    {
        GameManager.OnWorldSpeedChanged -= OnWorldSpeedChanged;
    }
}
