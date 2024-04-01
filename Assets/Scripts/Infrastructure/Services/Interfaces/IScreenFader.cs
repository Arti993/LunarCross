
public interface IScreenFader : IService
{
    public void FadeIn();
    
    public void FadeOutAndLoadScene(int sceneIndex);
}
