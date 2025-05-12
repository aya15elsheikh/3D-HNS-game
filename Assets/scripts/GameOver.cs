using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }


    public void RestartGame()
    {
        SceneManager.LoadScene("Level 1");
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
