using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AudioPlayback;
using Infrastructure.Services.Factories.GameplayFactory;
using Infrastructure.Services.Factories.ParticleSystemFactory;
using Infrastructure.Services.Factories.UiFactory;
using Infrastructure.Services.FocusTest;
using Infrastructure.Services.GameProgress;
using Infrastructure.Services.InterstitialAd;
using Infrastructure.Services.LevelSettings;
using Infrastructure.Services.Localization;
using Infrastructure.Services.ScreenFader;
using Infrastructure.UIStateMachine;
using UnityEngine;
using Reflex.Core;

namespace Infrastructure
{
    public class ProjectInstaller : MonoBehaviour, IInstaller
    {
        public void InstallBindings(ContainerBuilder builder)
        {
            builder.AddSingleton(typeof(AssetsProvider), typeof(IAssetsProvider));
            builder.AddSingleton(typeof(Localization), typeof(ILocalization));
            builder.AddSingleton(typeof(GameProgress), typeof(IGameProgress));
            builder.AddSingleton(typeof(LevelsSettingsNomenclature), typeof(ILevelsSettingsNomenclature));
            builder.AddSingleton(typeof(AudioPlayback), typeof(IAudioPlayback));
            builder.AddSingleton(typeof(UiStateMachine), typeof(IUiStateMachine));
            builder.AddSingleton(typeof(FocusTestStateChanger), typeof(IFocusTestStateChanger));
            builder.AddSingleton(typeof(ScreenFader), typeof(IScreenFader));
            builder.AddSingleton(typeof(UiWindowFactory), typeof(IUiWindowFactory));
            builder.AddSingleton(typeof(ParticleSystemFactory), typeof(IParticleSystemFactory));
            builder.AddSingleton(typeof(GameplayFactory), typeof(IGameplayFactory));
            builder.AddSingleton(typeof(InterstitialAdService), typeof(IInterstitionalAdService));
        }
    }
}