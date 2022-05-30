using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas uiCanvas;

    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameManager gm;
    [SerializeField] UIPlayerHealth playerHpDisplay;

    private void Awake()
    {
        PlayerController.OnHpChanged += UpdatePlayerHP;
    }

    private void Start()
    {
        UpdatePlayerHP(gm.player.hp);
    }

    public void UpdatePlayerHP(int newHp)
    {
        playerHpDisplay.SetHealthBars(newHp);
    }

    private void OnDestroy()
    {
        PlayerController.OnHpChanged -= UpdatePlayerHP;
    }

    public void NewGame()
    {
        gameOverPanel.SetActive(false);
        UpdatePlayerHP(3);
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
