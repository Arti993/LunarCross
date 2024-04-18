using UnityEngine;

public class EntitiesDisabler : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Entity entity))
        {
            Destroy(entity.gameObject);
            //entity.gameObject.SetActive(false);
        }
    }
}
