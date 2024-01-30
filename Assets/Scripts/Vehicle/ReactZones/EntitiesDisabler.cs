using UnityEngine;

public class EntitiesDisabler : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out EntityBehaviour entity))
        {
            if (entity.CurrentState != null)
                entity.CurrentState.Stop();

            entity.gameObject.SetActive(false);
        }
    }
}
