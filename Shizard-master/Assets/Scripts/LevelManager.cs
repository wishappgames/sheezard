using UnityEngine.SceneManagement;
using UnityEngine;
using Tabtale.TTPlugins;
using System.Collections.Generic;

/// <summary>
/// A base level manager to handle game and level states
/// </summary>
public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    [HideInInspector]
    public bool levelStarted = false;
    [SerializeField]
    private GameObject nextButton;
    [SerializeField]
    private GameObject retryButton;
    private bool gameStarted = false;
    private int currentLevelIndex = 0;

    private void Awake()
    {
        // Initialize CLIK Plugin
        TTPCore.Setup();

        if (instance != null)
            Destroy(gameObject);
        else
            instance = this;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    //Called when the player presses on the start button to start the game
    public void StartGame()
    {
        gameStarted = true;
        levelStarted = true;
    }

    //Called when the player presses on the retry button to restart the current level after lose
    public void RetryLevel()
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("Mission","Level " + (currentLevelIndex + 1));
        TTPGameProgression.FirebaseEvents.MissionFailed(parameters);

        StartLevel(currentLevelIndex);
    }

    //Called to start the current level
    public void StartLevel(int levelIndex)
    {
        currentLevelIndex = levelIndex;

        var parameters = new Dictionary<string, object>();
        parameters.Add("Mission","Level " + (currentLevelIndex + 1));
        if (currentLevelIndex + 1 == 1)
        {
            TTPGameProgression.FirebaseEvents.MissionStarted(1, parameters);
        }
        else
        {
            TTPGameProgression.FirebaseEvents.MissionStarted(currentLevelIndex + 1, parameters);
        }

        SceneManager.LoadScene(levelIndex, LoadSceneMode.Single);
        levelStarted = true;
    }

    //Called when the player presses on next button to go to next level when win
    public void NextLevel()
    {
        levelStarted = false;
        int nextLevel;
        if (currentLevelIndex == 5)
            nextLevel = Random.Range(1, 6);
        else
            nextLevel = currentLevelIndex + 1;
        StartLevel(nextLevel);
    }

    //Called when the player finish the current level with state win or lose
    public void OnLevelPassed(bool nextLevel)
    {
        var parameters = new Dictionary<string, object>();
        parameters.Add("Mission","Level " + (currentLevelIndex + 1));
        TTPGameProgression.FirebaseEvents.MissionComplete(parameters);

        nextButton.SetActive(nextLevel);
        retryButton.SetActive(!nextLevel);
    }

    //Called when the current level has been loaded to change game state to started
    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject.Find("Player").transform.GetChild(1).GetComponent<Animator>().enabled = true;
        if (gameStarted)
        {
            levelStarted = true;
        }

    }
}