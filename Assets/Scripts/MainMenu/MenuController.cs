using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject activeObject;


    private void Update()
    {
        BackToMainMenu();
    }

    public void GoToThisScreen()
    {
        activeObject.SetActive(this);
    }

    public void CreditScreen()
    {
        SceneManager.LoadScene("CreditScreen");
    }

    public void BackToMainMenu()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
    
    public void PlayGame()
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
