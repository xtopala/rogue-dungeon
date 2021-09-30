using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public float waitToLoad = 4f;

    public string nextLevel;

    public bool isPaused;

    public int currentCoins;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        Time.timeScale = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseUnpause();
        }
    }

    public IEnumerator LevelEnd()
    {
        AudioManager.instance.PlayLevelWin();

        PlayerController.instance.canMove = false;

        UIController.instance.StartFadeToBlack();

        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(nextLevel);
    }

    public void PauseUnpause()
    {
        if (!isPaused)
        {
            UIController.instance.pauseMenu.SetActive(true);
            isPaused = true;

            Time.timeScale = 0f;
        } else
        {
            UIController.instance.pauseMenu.SetActive(false);
            isPaused = false;

            Time.timeScale = 1f;
        }
    }

    public void GetCoins(int amount)
    {
        currentCoins += amount;
    }

    public void SpendCoins(int amount)
    {
        currentCoins -= amount;
        if (currentCoins < 0)
        {
            currentCoins = 0;
        }
    }
}
