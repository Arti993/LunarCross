using UnityEngine;

public interface IParticleSystemFactory : IService
{
    public void ShowExplosionEffect(Vector3 position);
    
    public void ShowGreenCollectEffect(Vector3 position);

    public void ShowCollectEffect(Vector3 position);
    
    public void ShowAlienEjectEffect(Vector3 position);

    public void ShowTornadoEjectEffect(Vector3 position);

    public void ShowYellowBurstEffect(Vector3 position);

}
