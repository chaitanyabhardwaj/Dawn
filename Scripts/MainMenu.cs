using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private string userInput;
    private int wordLen;

    public void GenerateScene()
    {
        Debug.Log("button pressed!");
        string[] theme = {"forest", "desert", "snow"};
        string currTheme = "forest";
        int themeIndex;

        //extract words from user input
        userInput = userInput.Trim();
        userInput = userInput.ToLower();
        userInput += " ";
        string[] words = new string[100];
        string temp = "";
        for(int i = 0; i < userInput.Length; i++)
        {
            if (userInput[i] < 'a' || userInput[i] > 'z')
            {
                words[wordLen++] = temp;
                Debug.Log(words[wordLen - 1]);
                temp = "";
            }
            else
            {
                temp += userInput[i];
            }
        }
        //match words with theme
        for(int i = 0, j; i < wordLen; i++)
        {
            Debug.Log(words[i]);
            for(j = 0; j < theme.Length; j++)
            {
                if(words[i].Equals(theme[j]))
                {
                    currTheme = words[i];
                    break;
                }
            }
            if (j < theme.Length) break;
        }
        //load scene
        switch(currTheme)
        {
            case "desert":
                themeIndex = 2; break;
            case "snow":
                themeIndex = 3; break;
            case "forest":
            default:
                themeIndex = 1; break;
        }
        SceneManager.LoadScene(currTheme);
    }

    public void OnTextChange(string text)
    {
        Debug.Log(text);
        userInput = text;
    }

}
