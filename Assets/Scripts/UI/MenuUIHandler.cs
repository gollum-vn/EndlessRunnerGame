using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MenuUIHandler : MonoBehaviour
{
    public Image howToPlayImage;
    public Image settingImage;
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }
    public void HowToPlay()
    {
        howToPlayImage.gameObject.SetActive(true);
    }
    public void Close()
    {
        var images =  GameObject.FindGameObjectsWithTag("Image");
        foreach(var image in images)
        {
            image.gameObject.SetActive(false);
        }
    }
    public void Setting()
    {
        settingImage.gameObject.SetActive(true);
    }
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // original code to quit Unity player
#endif
    }
}
