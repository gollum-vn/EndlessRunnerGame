using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public string PlayerName;
    public static MenuManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return; 
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void ReadInputField(string input)
    {
        PlayerName = input;
        Debug.Log(PlayerName);
    }
    
}
