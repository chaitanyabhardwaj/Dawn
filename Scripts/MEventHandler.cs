using UnityEngine;
using UnityEngine.SceneManagement;

public class MEventHandler : MonoBehaviour
{

    public void OnBackButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }

}
