using UnityEngine;
using UnityEngine.SceneManagement;

public class menu : MonoBehaviour
{
   
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

    }

    public void PlayGame()
    {
        SceneManager.LoadScene("Level 1");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
