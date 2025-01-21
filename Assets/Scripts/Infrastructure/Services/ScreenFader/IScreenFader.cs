using System;

namespace Infrastructure.Services.ScreenFader
{
    public interface IScreenFader : IService
    {
        public event Action FadingCompleted;

        public event Action FadingStarted;

        public bool IsActive();

        public void FadeIn();

        public void FadeOutAndLoadScene(int sceneIndex);
    }
}
