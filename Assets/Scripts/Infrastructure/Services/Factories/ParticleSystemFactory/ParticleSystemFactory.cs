using Ami.BroAudio;
using Data;
using Infrastructure.Services.AssetsProvider;
using Infrastructure.Services.AudioPlayback;
using Reflex.Attributes;
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
            _audioPlayback.PlayExplosionSound();

            _ = _provider.Instantiate(PrefabsPaths.Explosion, position);
        }

        public void ShowGreenCollectEffect(Vector3 position)
        {
            SoundID saveSound = _audioPlayback.SoundsContainer.SavingAstronaut;

            _audioPlayback.PlaySound(saveSound);

            _ = _provider.Instantiate(PrefabsPaths.RayPulling, position);
        }

        public void ShowCollectEffect(Vector3 position)
        {
            _ = _provider.Instantiate(PrefabsPaths.CollectEffect, position);
        }

        public void ShowAlienEjectEffect(Vector3 position)
        {
            SoundID alienGrabSound = _audioPlayback.SoundsContainer.AlienGrab;

            _audioPlayback.PlaySound(alienGrabSound);

            _ = _provider.Instantiate(PrefabsPaths.EjectEffect, position);
        }

        public void ShowTornadoEjectEffect(Vector3 position)
        {
            SoundID tornadoSound = _audioPlayback.SoundsContainer.Tornado;

            _audioPlayback.PlaySound(tornadoSound);

            _ = _provider.Instantiate(PrefabsPaths.EjectEffect, position);
        }

        public void ShowYellowBurstEffect(Vector3 position)
        {
            _ = _provider.Instantiate(PrefabsPaths.BurstYellow, position);
        }
    }
}