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

        public ParticleSystemFactory(IAssetsProvider provider)
        {
            _provider = provider;
        }

        public void ShowExplosionEffect(Vector3 position)
        {
            DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayExplosionSound();

            _ = _provider.Instantiate(PrefabsPaths.Explosion, position);
        }

        public void ShowGreenCollectEffect(Vector3 position)
        {
            SoundID saveSound = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer
                .SavingAstronaut;

            DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(saveSound);

            _ = _provider.Instantiate(PrefabsPaths.RayPulling, position);
        }

        public void ShowCollectEffect(Vector3 position)
        {
            _ = _provider.Instantiate(PrefabsPaths.CollectEffect, position);
        }

        public void ShowAlienEjectEffect(Vector3 position)
        {
            SoundID alienGrabSound =
                DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.AlienGrab;

            DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(alienGrabSound);

            _ = _provider.Instantiate(PrefabsPaths.EjectEffect, position);
        }

        public void ShowTornadoEjectEffect(Vector3 position)
        {
            SoundID tornadoSound = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.Tornado;

            DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(tornadoSound);

            _ = _provider.Instantiate(PrefabsPaths.EjectEffect, position);
        }

        public void ShowYellowBurstEffect(Vector3 position)
        {
            _ = _provider.Instantiate(PrefabsPaths.BurstYellow, position);
        }
    }
}