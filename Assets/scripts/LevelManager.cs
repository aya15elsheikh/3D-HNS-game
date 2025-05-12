using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform player;
    public int score;
    public int levelItems;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Re-acquire player reference after scene loads
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
                Debug.Log("Player reference restored");
            }
            else
            {
                Debug.LogWarning("Player not found in scene!");
            }
        }

        InitializeLevel();
    }

    private void InitializeLevel()
    {
        score = 0;
        levelItems = 0;
        Debug.Log("Level Initialized: Score = " + score + ", Level Items = " + levelItems);
    }

    void OnDestroy()
    {
       
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

 
    public Vector3 GetPlayerPosition()
    {
        return player != null ? player.position : Vector3.zero;
    }
}