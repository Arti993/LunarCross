using UnityEngine;

public interface IUiWindowFactory : IService
{
    public GameObject GetUIRoot();
    public void DeleteUIRoot();
    public GameObject GetPauseButton(GameObject parent);
    public GameObject GetLevelCompleteWindow(GameObject parent);
    public GameObject GetPauseMenuWindow(GameObject parent);
    public GameObject GetLevelFailedWindow(GameObject parent);
    public GameObject GetLevelNumberTitle(GameObject parent);
    public GameObject GetRestartGameQuestionWindow(GameObject parent);
    public GameObject GetLeaderBoardElement(GameObject parent);
    public GameObject GetTutorialKeyboardControlWindow(GameObject parent);
    public GameObject GetTutorialTouchscreenControlWindow(GameObject parent);
    public GameObject GetTutorialCollectingWindow(GameObject parent);
    public GameObject GetTutorialAliensWindow(GameObject parent);
    public GameObject GetTutorialObstaclesWindow(GameObject parent);
    public GameObject GetTutorialTornadoWindow(GameObject parent);
    public GameObject GetTutorialFinishWindow(GameObject parent);
    public GameObject GetMainMenuButtonsWindow(GameObject parent);
    public GameObject GetLeaderboardWindow(GameObject parent);
    public GameObject GetSettingsWindow(GameObject parent);
    public GameObject GetCompleteGameWindow(GameObject parent);

}
