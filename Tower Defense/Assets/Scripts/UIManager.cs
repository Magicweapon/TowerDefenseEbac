using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject GameOverMenu;

    public EnemySpawner enemySpawner;
    public Target targetReference;

    private void OnEnable()
    {
        targetReference.OnDestroyedObject += DisplayGameOverMenu;
    }
    private void OnDisable()
    {
        targetReference.OnDestroyedObject -= DisplayGameOverMenu;
    }
    public void DisplayGameOverMenu()
    {
        GameOverMenu.SetActive(true);
    }
    public void HideGameOverMenu()
    {
        GameOverMenu.SetActive(false);
    }
    public void DisplayWaveEndMenu()
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
