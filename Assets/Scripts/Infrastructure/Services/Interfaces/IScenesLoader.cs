public interface IScenesLoader : IService
{
    void LoadScene(int index);
    void LoadGameplayScene();
    void LoadMainMenuScene();
    void LoadLevelChooseScene();
    void LoadTutorialScene();
    int GetTutorialSceneIndex();
}
