using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance = null;

    [SerializeField]
    private TextMeshProUGUI text;

    [SerializeField]
    private GameObject gameoverPanel;


    private int coin = 0;

    [HideInInspector]
    public bool isGameOver = false;

    [SerializeField]
    private int UpgradeWeapon = 30;

    void Awake() // Start 보다 더 빠르게 호출
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void IncreaseCoin()
    {
        coin += 1;
        text.SetText(coin.ToString());

        if (coin % UpgradeWeapon == 0)
        {
            Player player = FindObjectOfType<Player>();
            if (player != null) 
            {
                player.UpgradeWeapon(); 
            }
        }
    }

    public void SetGameOver()
    {
        isGameOver = true;

        EnemySpawner enemySpawner = FindObjectOfType<EnemySpawner>();   
        if (enemySpawner != null)
        {
            enemySpawner.StopEnemyRountine();
        }

        Invoke("ShowGameOverPanel", 1f);
    }

    void ShowGameOverPanel()
    {
        gameoverPanel.SetActive(true);
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene("SampleScene");
    }
}
