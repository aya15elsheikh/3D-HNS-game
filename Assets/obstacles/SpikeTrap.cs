using CodeMonkey.HealthSystemCM;
using UnityEngine;
public class SpikeTrapColliderCheck : MonoBehaviour
{
    public string playerTag = "Player";
    public float damage;
    private GameObject Player;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag(playerTag);
    }
    void Update()
    {
        BoxCollider[] colliders = GetComponentsInChildren<BoxCollider>();

        foreach (BoxCollider col in colliders)
        {
            if (col.bounds.Intersects(Player.GetComponent<Collider>().bounds))
            {
                Player.GetComponent<HealthSystemComponent>().GetHealthSystem().Damage(damage);

            }
        }
    }
}
