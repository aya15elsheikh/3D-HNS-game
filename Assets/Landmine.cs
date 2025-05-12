using CodeMonkey.HealthSystemCM;
using UnityEngine;

public class Landmine : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource beepSound;
    public AudioSource explodeSound;
    public float minVolume = 0.1f;
    public float maxVolume = 1.0f;
    public float damage;


    private Transform player;
    private float beepTimer = 0f;
    private float beepInterval = 2f;
    private bool isActive = true;

    private void Start()
    {

        if (beepSound == null)
        {
            beepSound = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            AudioSource[] srcs = GetComponents<AudioSource>();
            beepSound = srcs[0];
        }
        if (explodeSound == null)
        {
            explodeSound = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            AudioSource[] srcs = GetComponents<AudioSource>();
            explodeSound = srcs[1];
        }


        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }
    private void Update()
    {
        if (!isActive || player == null)
            return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        HandleBeeping(distanceToPlayer);
    }

    private void HandleBeeping(float distanceToPlayer)
    {
        if (distanceToPlayer <= 10)
        {
            beepTimer += Time.deltaTime;

            if (beepTimer >= beepInterval)
            {
                beepTimer = 0f;
                PlayBeepSound(distanceToPlayer);
            }
        }
    }

    private void PlayBeepSound(float distance)
    {
        if (beepSound != null)
        {
            float normalizedDistance = Mathf.Clamp01(1 - (distance / 10));
            float volume = Mathf.Lerp(minVolume, maxVolume, normalizedDistance);

            beepSound.volume = volume;
            beepSound.Play();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive && other.CompareTag("Player"))
        {
            TriggerExplosion(other.gameObject);
        }
    }

    private void TriggerExplosion(GameObject playerObject)
    {
        if (isActive)
        {
            isActive = false;
            explodeSound.Play();
            playerObject.GetComponent<HealthSystemComponent>().GetHealthSystem().Damage(damage);
            Destroy(gameObject, explodeSound.clip.length + 0.1f);
        }
    }
}