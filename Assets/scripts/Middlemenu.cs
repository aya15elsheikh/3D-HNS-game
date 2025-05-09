using UnityEngine;
using UnityEngine.SceneManagement;

public class Middlemenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }

    public void ContinueGame()
    {
   
        SceneManager.LoadScene("Level 2");
    }
    public void QuitGame()
    {
        //Application.Quit();
        SceneManager.LoadScene("menu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
