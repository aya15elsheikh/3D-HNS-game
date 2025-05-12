using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class ColliderLogger : MonoBehaviour

{
    public float damage;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthSystemComponent>().GetHealthSystem().Damage(damage);
        }
    }
}
