using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;

    private bool _gameIsPaused = false;

    private void Update()
    {
        if(GameManager.IsPlayingRound)
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (_gameIsPaused)
                    Resume();
                else
                    Pause();
            }
    }

    public void Pause()
    {
        _gameIsPaused = true;
        Time.timeScale = 0f;
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
        _pauseMenu.SetActive(true);
    }

    public void Resume()
    {
        _gameIsPaused = false;
        Time.timeScale = 1f;
        FocusBackgroundPanel.FocusBackPanel.SetActive(false);
        _pauseMenu.SetActive(false);
    }
}
