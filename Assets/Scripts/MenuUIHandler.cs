using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuUIHandler : MonoBehaviour
{
    public ColorPicker ColorPicker;
    private Color tempColor;
    private string savedColor;

    [SerializeField]
    private Canvas mainCanvas;

    public void NewColorSelected(Color color)
    {
        // add code here to handle when a color is selected
        //tempColor = color;
        MainManager.Instance.TeamColor = color;
   
    }

    private void Awake()
    {
        //DontDestroyOnLoad(this.gameObject);
    }

    private void Start()
    {
        ColorPicker.Init();
        //this will call the NewColorSelected function when the color picker have a color button clicked.
        ColorPicker.onColorChanged += NewColorSelected;
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
    private void Update()
    {
        //Debug.Log(tempColor);
    }

    public void BackToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ExitGame()
    {
        MainManager.Instance.SaveColor();
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    
        Application.Quit();
#endif
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
        mainCanvas.gameObject.SetActive(false);
    }
    public void SaveColor()
    {
        PlayerPrefs.SetString("SavedColor", ColorToHex(tempColor));
        
    }

    public void GetSavedColor()
    {
        Color GetedColor = HexToColor(PlayerPrefs.GetString("SavedColor"));
        //ColorPicker.
        
    }




    // Parsing from Color to Hex and back code


    // Note that Color32 and Color implictly convert to each other. You may pass a Color object to this method without first casting it.
    string ColorToHex(Color32 color)
    {
        string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
        return hex;
    }

    Color HexToColor(string hex)
    {
        byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
        byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
        byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
        return new Color32(r, g, b, 255);
    }


    public void SaveColorClicked()
    {
        MainManager.Instance.SaveColor();
    }

    public void LoadColorClicked()
    {
        MainManager.Instance.LoadColor();
        ColorPicker.SelectColor(MainManager.Instance.TeamColor);
    }
}
