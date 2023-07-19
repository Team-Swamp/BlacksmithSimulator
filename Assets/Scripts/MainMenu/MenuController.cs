using UnityEngine;
using UnityEngine.SceneManagement;

public sealed class MenuController : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject activeExplanationMenu;
    [SerializeField] private GameObject activeMainMenu;
    
    public void ToggleToMainMenuFormExplanation(bool goingToExplanation)
    {
        activeMainMenu.SetActive(!goingToExplanation);
        activeExplanationMenu.SetActive(goingToExplanation);
    }
    
    public void CreditScreen() => SceneManager.LoadScene("CreditScreen");

    public void PlayGame() => SceneManager.LoadSceneAsync(sceneName);

    public void QuitGame() => Application.Quit();
}
