using UnityEngine;

public class Menu : FsmGameState
{
    private GameObject _menuUI;
    private GameObject _gameplayUI;
    private Player _player;
    private Vector2 _startPos;

    public Menu(FsmGame fsmUI, GameObject menuUI, GameObject gameplayUI, Player player, Vector2 startPos) : base(fsmUI)
    {
        _menuUI = menuUI;
        _gameplayUI = gameplayUI;
        _player = player;
        _startPos = startPos;
    }

    public override void Enter()
    {
        base.Enter();

        Time.timeScale = 0f;


        _player.transform.position = _startPos;

        _menuUI.SetActive(true);
        _gameplayUI.SetActive(false);
        FocusBackgroundPanel.FocusBackPanel.SetActive(true);
    }

    public override void Exit()
    {
        base.Exit();

        _menuUI.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space))
            Fsm.SetState<StartGame>();
    }
}
