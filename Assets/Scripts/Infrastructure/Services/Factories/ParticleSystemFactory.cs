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

    public GameObject GetConfettiBlastEffect(Vector3 position)
    {
        return _provider.Instantiate("Prefabs/Particles/ConfettiBlast", position);
    }

    public GameObject GetYellowBurstEffect(Vector3 position)
    {
        return _provider.Instantiate("Prefabs/Particles/BurstYellow", position);
    }
}