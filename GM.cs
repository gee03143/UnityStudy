using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI; // for skill

public class GM : MonoBehaviour
{

    [SerializeField]
    private GameObject gameOverUI;

    [SerializeField]
    private GameObject pauseMenuUI;

    [SerializeField]
    private WaveSpawner waveSpawner;


    private static int _remainingLives = 2;
    public static int RemainingLives
    {
        get { return _remainingLives; }
    }

    public static GM gm;
    public int playerScore = 0;
    public Transform playerPrefab;
    public Transform spawnPoint;

    public CameraShake camShake;

    private static float screenWidth;
    public static float ScreenWidth
    {
        get { return screenWidth; }
    }

    private static float screenHeight;
    public static float ScreenHeight
    {
        get { return screenHeight; } 
    }

    void Awake()
    {
        if (gm == null)
        {
            gm = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        screenHeight = 2 * Camera.main.orthographicSize;
        screenWidth = screenHeight * Camera.main.aspect;
        Screen.SetResolution(Screen.width, Screen.width * 9 / 16, true);
        if (camShake == null)
        {
            Debug.LogError("No CameraShake reference in GM ");
        }
    }

    public void EndGame()
    {
        Debug.Log("Game Over!!");
        gameOverUI.SetActive(true);
    }


    public void Pause()
    {
        if (pauseMenuUI.activeSelf)
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        else
        {
            Time.timeScale = 0f;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }
        TogglePauseMenu();
    }


    private void TogglePauseMenu()
    {
        pauseMenuUI.SetActive(!pauseMenuUI.activeSelf);
        waveSpawner.enabled = !pauseMenuUI.activeSelf;
    }

    public void Restart()
    {
        playerScore = 0;
        Time.timeScale = 1f;
        Time.fixedDeltaTime = 0.02f * Time.timeScale;
        TogglePauseMenu();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void _RespawnPlayer()
    {
        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

    }

    public static void KillPlayer()
    {
        gm.EndGame();
    }

    public static void KillPlayer(CharacterController player)
    {
        Destroy(player.gameObject);
        _remainingLives -= 1;
        if (_remainingLives <= 0)
        {
            gm.EndGame();
        }
        else
        {
            //gm.StartCoroutine(gm._RespawnPlayer());
        }
    }

    public void GiveScore(int amount)
    {
        playerScore += amount;
    }

}
