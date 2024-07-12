using UnityEngine;

public class ModalWindowController : MonoBehaviour
{
    public static ModalWindowController instanceModalWindow;

    public ModalWindowPanel modalWindow => _modalWindow;

    [SerializeField] ModalWindowPanel _modalWindow;
    [SerializeField] GameStateController _gameController;

    private void Awake()
    {
        instanceModalWindow = this;
    }

    public void ShowWarningExitToMenu()
    {
        if (Score.CurrentScore >= 10)
        {
            _modalWindow.gameObject.SetActive(true);
            _modalWindow.ShowHorizontal("�����?", "�� �������, ��� ������ �����? \n �������� �� ���� ����� ����� �������!",
                "�����", "������", GoToMenu, CloseModalWindow);
        }
        else
            GoToMenu();
    }

    public void ShowWarningRestart()
    {
        if (Score.CurrentScore >= 10)
        {
            _modalWindow.gameObject.SetActive(true);
            _modalWindow.ShowHorizontal("����������?", "�� �������, ��� ������ ���������� �����? \n �������� �� ���� ����� ����� �������!",
                "����������", "������", Restart, CloseModalWindow);
        }
        else
            Restart();
    }

    void CloseModalWindow() => _modalWindow.CloseModalWindow();

    void GoToMenu()
    {
        _gameController.SetMenuState();
        CloseModalWindow();
    }

    void Restart()
    {
        _gameController.SetStartGameState();
        CloseModalWindow();
    }
}
