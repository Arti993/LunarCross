using Ami.BroAudio;
using Data;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AudioPlayback;
using UnityEngine;

namespace Infrastructure.Services.Factories.ParticleSystemFactory
{
    public class ParticleSystemFactory : IParticleSystemFactory
    {
        private readonly IAssetsProvider _provider;
        private IAudioPlayback _audioPlayback;

        public ParticleSystemFactory(IAssetsProvider provider, IAudioPlayback audioPlayback)
        {
            _provider = provider;
            _audioPlayback = audioPlayback;
        }

        public void ShowExplosionEffect(Vector3 position)
        {
            ShowEffect(position, PrefabsPaths.Explosion);
            
            _audioPlayback.PlayExplosionSound();
        }

        public void ShowGreenCollectEffect(Vector3 position)
        {
            ShowEffect(position, PrefabsPaths.RayPulling);
            
            PlayEffectSound(_audioPlayback.SoundsContainer.SavingAstronaut);
        }

        public void ShowCollectEffect(Vector3 position)
        {
            ShowEffect(position, PrefabsPaths.CollectEffect);
        }

        public void ShowAlienEjectEffect(Vector3 position)
        {
            ShowEffect(position, PrefabsPaths.EjectEffect);
            
            PlayEffectSound(_audioPlayback.SoundsContainer.AlienGrab);
        }

        public void ShowTornadoEjectEffect(Vector3 position)
        {
            ShowEffect(position, PrefabsPaths.EjectEffect);
            
            PlayEffectSound(_audioPlayback.SoundsContainer.Tornado);
        }

        public void ShowYellowBurstEffect(Vector3 position)
        {
            ShowEffect(position, PrefabsPaths.BurstYellow);
        }

        private void ShowEffect(Vector3 position, string prefabPath)
        {
            _ = _provider.Instantiate(prefabPath, position);
        }

        private void PlayEffectSound(SoundID sound)
        {
            _audioPlayback.PlaySound(sound);
        }
    }
}