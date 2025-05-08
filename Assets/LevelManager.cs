using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public Transform player;
    public int score;
    public int levelItems;

    void Awake()
    {
        // Ensure that this GameObject is not destroyed when loading a new scene
        instance = this;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
