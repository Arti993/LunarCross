using UnityEngine;

public interface IParticleSystemFactory : IService
{
    public GameObject GetExplosionEffect(Vector3 position);
    
    public GameObject GetRayPullingEffect(Vector3 position);

    public GameObject GetCollectEffect(Vector3 position);
    
    public GameObject GetEjectEffect(Vector3 position);

    public GameObject GetYellowBurstEffect(Vector3 position);

}
