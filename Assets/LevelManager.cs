using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance; // Singleton instance
    public Transform player; // Reference to the player
    public int score; // Player's score
    public int levelItems; // Number of items in the level

    void Awake()
    {
        // Ensure that this GameObject is not destroyed when loading a new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this instance alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate instances
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeLevel();
    }

    // Method to initialize level variables
    private void InitializeLevel()
    {
        score = 0; // Reset score
        levelItems = 0; // Reset level items
        Debug.Log("Level Initialized: Score = " + score + ", Level Items = " + levelItems);
    }

    // Update is called once per frame
    void Update()
    {
        // Any necessary updates can be handled here
    }
}
