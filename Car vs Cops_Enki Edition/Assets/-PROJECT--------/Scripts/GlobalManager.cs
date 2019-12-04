using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GlobalManager : MonoBehaviour
{

    public static GlobalManager instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GlobalManager>();
            return _instance;
        }
    }
    private static GlobalManager _instance;

    public float scorePerSeconds = 1;
    
    [Header("References")]
    public Spawner[] spawners;
    public CarControls carControls;
    public Canvas canvasEndGame;
    public Text highScoreUI;

    [Header("Don't touch")]
    public int currentScore;
    private float scoreFloat;

    public int hightScore;
    public int coins;

    private bool gameStarted = false;
    private bool gameEnded = false;

    // Start is called before the first frame update
    void Awake()
    {
        hightScore = PlayerPrefs.GetInt("highscore", 0); 
        coins = PlayerPrefs.GetInt("coins", 0);
        canvasEndGame.enabled = false;
        highScoreUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameStarted)
            return;
        if (gameEnded)
            return;

        scoreFloat += Time.deltaTime * scorePerSeconds;
        currentScore = (int)scoreFloat;


    }

    /// <summary>
    /// Called by the button in CanvasStart
    /// </summary>
    /// <param name="input"></param>
    public void StartGame(BaseEventData input)
    {
        if (gameStarted)
            return;
        gameStarted = true;

        foreach (Spawner spawner in spawners)
        {
            spawner.StartGame();
        }
        carControls.StartGame(input);
    }

    /// <summary>
    /// Not the avenger one
    /// </summary>
    public void EndGame()
    {
        gameEnded = true;
        foreach (Spawner spawner in spawners)
        {
            spawner.EndGame();
        }
        carControls.EndGame();
        canvasEndGame.enabled = true;

        PlayerPrefs.SetInt("coins", coins);
        if(currentScore > hightScore)
        {
            hightScore = currentScore;
            PlayerPrefs.SetInt("highscore", hightScore);
            highScoreUI.text = "Hightscore !!!\n" + hightScore;
            highScoreUI.enabled = true;
        }

    }

    /// <summary>
    /// Called by the button in CanvasEndGame.
    /// </summary>
    public void ReloadGame()
    {
        SoundManager.instance.UiButton();
        SceneManager.LoadScene("Game"); //When the player dies, we reload the scene
        //In a realistic scenario, I would have setuped a persistent scene that never unload, and to reset the game, unload the game scene, then reload it just after
    }

}
