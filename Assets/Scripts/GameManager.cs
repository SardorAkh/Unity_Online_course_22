using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Tower tower;
    [SerializeField] private EnemySpawner enemySpawner;

    [SerializeField] private TMP_Text coinText;
    [SerializeField] private Button startButton;

    private int _coinAmount;

    public void StartGame()
    {
        Debug.Log("Start Game");
        enemySpawner.SetSpawnState(true);
    }

    private void Start()
    {
        startButton.onClick.AddListener(StartGame);

        UpdateUI();
        tower.OnKill += TowerOnKill;
    }

    private void TowerOnKill()
    {
        IncreaseCoins(1);
        UpdateUI();
    }

    private void UpdateUI()
    {
        // coinText.text = string.Format(coinText.text, _coinAmount);
        coinText.text = "Coins: " + _coinAmount;
    }

    public void IncreaseCoins(int amount)
    {
        _coinAmount += amount;
    }

    public void DecreaseCoins(int amount)
    {
        _coinAmount -= amount;
    }

    private void OnDisable()
    {
        tower.OnKill -= TowerOnKill; // Отписка.
    }
}