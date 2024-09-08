public class ScenesLoader : IScenesLoader
{
    private int _mainMenuSceneIndex;
    private int _gameplaySceneIndex;
    private int _levelChooseSceneIndex;
    private int _tutorialSceneIndex;
    private int _finalSceneIndex;

    public ScenesLoader()
    {
        _mainMenuSceneIndex = 1;
        _gameplaySceneIndex = 2;
        _levelChooseSceneIndex = 3;
        _tutorialSceneIndex = 4;
        _finalSceneIndex = 5;
    }

    public void LoadScene(int index)
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(index);
    }

    public void LoadGameplayScene()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(_gameplaySceneIndex);
    }

    public void LoadMainMenuScene()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(_mainMenuSceneIndex);
    }

    public void LoadLevelChooseScene()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(_levelChooseSceneIndex);
    }

    public void LoadTutorialScene()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(_tutorialSceneIndex);
    }

    public void LoadFinalScene()
    {
        DIServicesContainer.Instance.GetService<IScreenFader>().FadeOutAndLoadScene(_finalSceneIndex);
    }

    public int GetTutorialSceneIndex()
    {
        return _tutorialSceneIndex;
    }

    public int GetGameplaySceneIndex()
    {
        return _gameplaySceneIndex;
    }
}