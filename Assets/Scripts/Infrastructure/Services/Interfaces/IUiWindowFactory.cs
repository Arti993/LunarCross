using UnityEngine;

public interface IUiWindowFactory : IService
{
    public GameObject GetUIRoot();
    public GameObject GetPauseButton();
    public GameObject GetLevelCompleteWindow();
    public GameObject GetPauseMenuWindow();
    public GameObject GetLevelFailedWindow();
}
