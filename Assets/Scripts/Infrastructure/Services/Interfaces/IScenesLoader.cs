public interface IScenesLoader : IService
{
    void LoadScene(int index);
    void LoadGameplayScene();
    void LoadMainMenuScene();
    void LoadLevelChooseScene();
    void LoadTutorialScene();
    void LoadFinalScene();
    int GetTutorialSceneIndex();
    int GetGameplaySceneIndex();
}
