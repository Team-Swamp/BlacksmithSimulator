using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject activeExplanationMenu;
    [SerializeField] private GameObject activeMainMenu;

    private bool _setUi;


    private void Update()
    {
        BackToMainMenu();
    }

    public void GoToExplantion()
    {
        activeExplanationMenu.SetActive(this);
        activeMainMenu.SetActive(false);
    }

    public void GoToMainMenu()
    {
        activeExplanationMenu.SetActive(false);
        activeMainMenu.SetActive(true);
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
        SceneManager.LoadSceneAsync(sceneName);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
