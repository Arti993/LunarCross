using Ami.BroAudio;
using UnityEngine;

public class ParticleSystemFactory : IParticleSystemFactory
{
    private readonly IAssetsProvider _provider;

    public ParticleSystemFactory(IAssetsProvider provider)
    {
        _provider = provider;
    }

    public GameObject GetExplosionEffect(Vector3 position)
    {
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayExplosionSound();
        
        return _provider.Instantiate(PrefabsPaths.Explosion, position);
    }

    public GameObject GetGreenCollectEffect(Vector3 position)
    {
        SoundID saveSound = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.SavingAstronaut;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(saveSound);
        
        return _provider.Instantiate(PrefabsPaths.RayPulling, position);
    }

    public GameObject GetCollectEffect(Vector3 position)
    {
        return _provider.Instantiate(PrefabsPaths.CollectEffect, position);
    }

    public GameObject GetAlienEjectEffect(Vector3 position)
    {
        SoundID alienGrabSound = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.AlienGrab;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(alienGrabSound);
        
        return _provider.Instantiate(PrefabsPaths.EjectEffect, position);
    }

    public GameObject GetTornadoEjectEffect(Vector3 position)
    {
        SoundID tornadoSound = DIServicesContainer.Instance.GetService<IAudioPlayback>().SoundsContainer.Tornado;
        
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlaySound(tornadoSound);
        
        return _provider.Instantiate(PrefabsPaths.EjectEffect, position);
    }

    public GameObject GetYellowBurstEffect(Vector3 position)
    {
        return _provider.Instantiate(PrefabsPaths.BurstYellow, position);
    }
}