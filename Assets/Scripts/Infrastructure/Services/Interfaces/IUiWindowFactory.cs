using UnityEngine;

public interface IUiWindowFactory : IService
{
    public GameObject GetUIRoot();
    public GameObject GetPauseButton(GameObject parent);
    public GameObject GetLevelCompleteWindow(GameObject parent);
    public GameObject GetPauseMenuWindow(GameObject parent);
    public GameObject GetLevelFailedWindow(GameObject parent);

    public GameObject GetRestartGameQuestionWindow(GameObject parent);
}
