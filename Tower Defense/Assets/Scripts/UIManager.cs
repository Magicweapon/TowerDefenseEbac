using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject GameOverMenu;
    public GameObject WaveDefeatedMenu;
    public GameObject LastEnemyMsg;
    public GameManager gameManager;
    public EnemySpawner enemySpawner;
    public Target targetReference;
    public Text resourcesText;
    public Text waveText;
    public Text enemiesText;
    public Text bossesText;
    public Text gameWonText;
    public Button startButton;
    public Button leaveButton;

    private void OnEnable()
    {
        targetReference.OnDestroyedObject += DisplayGameOverMenu;
        targetReference.OnDestroyedObject += SetBoolFalse;
        enemySpawner.OnWaveStarted += UpdateWave;
        enemySpawner.OnWaveStarted += DisableStartButton;
        enemySpawner.OnWaveFinished += ShowLastEnemyMessage;
        enemySpawner.OnWaveDefeated += ShowWaveDefeatedMenu;
        enemySpawner.OnWaveDefeated += EnableStartButtonDelayed;
        gameManager.OnResourcesModified += UpdateResources;
    }
    private void OnDisable()
    {
        targetReference.OnDestroyedObject -= DisplayGameOverMenu;
        targetReference.OnDestroyedObject -= SetBoolFalse;
        enemySpawner.OnWaveStarted -= UpdateWave;
        enemySpawner.OnWaveStarted -= DisableStartButton;
        enemySpawner.OnWaveFinished -= ShowLastEnemyMessage;
        enemySpawner.OnWaveDefeated -= ShowWaveDefeatedMenu;
        enemySpawner.OnWaveDefeated -= EnableStartButtonDelayed;
        gameManager.OnResourcesModified -= UpdateResources;
    }
    public void SetBoolFalse()
    {
        enemySpawner.WaveHasStarted = false;
    }
    public void DisableStartButton()
    {
        startButton.interactable = false;
    }
    public void EnableStartButtonDelayed()
    {
        if (enemySpawner.wave == enemySpawner.enemiesPerWave.Count)
        {
            Debug.Log("¡Última oleada derrotada!");
            leaveButton.gameObject.SetActive(true);
            return;
        }
        Invoke("EnableStartButton", 2.5f);
    }
    public void EnableStartButton()
    {
        startButton.interactable = true;
    }
    public void UpdateWave()
    {
        waveText.text = $"Wave: {enemySpawner.wave + 1}";
        HideWaveDefeatedMenu();
    }
    public void ShowLastEnemyMessage()
    {
        LastEnemyMsg.SetActive(true);
        Invoke("HideLastEnemyMessage", 3);
    }
    public void HideLastEnemyMessage()
    {
        LastEnemyMsg.SetActive(false);
    }
    public void ShowWaveDefeatedMenu()
    {
        if (enemySpawner.wave == enemySpawner.enemiesPerWave.Count)
        {
            gameWonText.gameObject.SetActive(true);
        }

        enemiesText.text = "Enemies defeated: " + gameManager.enemiesDefeated;
        bossesText.text = "Bosses defeated: " + gameManager.bossesDefeated;
        WaveDefeatedMenu.SetActive(true);
    }
    public void HideWaveDefeatedMenu()
    {
        WaveDefeatedMenu.SetActive(false);
    }
    public void UpdateResources()
    {
        if (resourcesText != null) resourcesText.text = $"Resources: {gameManager.resources}";
    }
    public void DisplayGameOverMenu()
    {
        GameOverMenu.SetActive(true);
    }
    public void HideGameOverMenu()
    {
        GameOverMenu.SetActive(false);
    }
    public void ShowWaveEndMenu()
    {

    }
    public void HideWaveEndMenu()
    {

    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void GotoMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryGame()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }
}
