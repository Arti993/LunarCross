using UnityEngine;

public class ParticleSystemFactory : IParticleSystemFactory
{
    private readonly IAssets _provider;

    public ParticleSystemFactory(IAssets provider)
    {
        _provider = provider;
    }

    public GameObject GetExplosionEffect(Vector3 position)
    {
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayExplosionSound();
        
        return _provider.Instantiate("Prefabs/Particles/ExplosionFireballFire", position);
    }

    public GameObject GetRayPullingEffect(Vector3 position)
    {
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayAstronautInRaySound();
        
        return _provider.Instantiate("Prefabs/Particles/RayPulling", position);
    }

    public GameObject GetCollectEffect(Vector3 position)
    {
        return _provider.Instantiate("Prefabs/Particles/CollectEffect", position);
    }

    public GameObject GetAlienEjectEffect(Vector3 position)
    {
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayAlienGrabsAstronautSound();
        
        return _provider.Instantiate("Prefabs/Particles/EjectEffect", position);
    }

    public GameObject GetTornadoEjectEffect(Vector3 position)
    {
        DIServicesContainer.Instance.GetService<IAudioPlayback>().PlayTornadoSound();
        
        return _provider.Instantiate("Prefabs/Particles/EjectEffect", position);
    }

    public GameObject GetYellowBurstEffect(Vector3 position)
    {
        return _provider.Instantiate("Prefabs/Particles/BurstYellow", position);
    }
}