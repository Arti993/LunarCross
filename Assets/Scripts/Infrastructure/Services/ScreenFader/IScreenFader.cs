using System;

namespace Infrastructure.Services.ScreenFader
{
    public interface IScreenFader : IService
    {
        public event Action FadingComplete;

        public event Action FadingStart;

        public bool IsActive();

        public void FadeIn();

        public void FadeOutAndLoadScene(int sceneIndex);
    }
}
