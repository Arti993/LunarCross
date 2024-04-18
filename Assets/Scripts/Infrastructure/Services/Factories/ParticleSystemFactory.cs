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
        return _provider.Instantiate("Prefabs/Particles/ExplosionFireballFire", position);
    }

    public GameObject GetRayPullingEffect(Vector3 position)
    {
        return _provider.Instantiate("Prefabs/Particles/RayPulling", position);
    }

    public GameObject GetCollectEffect(Vector3 position)
    {
        return _provider.Instantiate("Prefabs/Particles/CollectEffect", position);
    }

    public GameObject GetEjectEffect(Vector3 position)
    {
        return _provider.Instantiate("Prefabs/Particles/EjectEffect", position);
    }

    public GameObject GetYellowBurstEffect(Vector3 position)
    {
        return _provider.Instantiate("Prefabs/Particles/BurstYellow", position);
    }
}