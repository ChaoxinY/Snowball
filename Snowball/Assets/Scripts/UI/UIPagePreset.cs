using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewUIPagePreset", menuName = "UI/UIPagePreset")]
public class UIPagePreset : ScriptableObject
{
    public List<UIPanel> panels;
}

//Note : Create panel presets directly under the main page and not under other panels.
[System.Serializable]
public class UIPanel
{
    public string panelName;
    //Changed to gameobject because it handles prefab name changes easier.
    public GameObject panelPreset;
    public Vector2 panelPosition, panelSize;
    public bool usesCustomTransformProperty;
    public LayoutTypes layout;
    public List<UIButton> uIButtons = new List<UIButton>();
    public enum LayoutTypes {
        None,
        Horizontal,
        Vertical,
        Grid,
    }
}

//Editor check when an enum is select, the nesscary information should appear as well.
[System.Serializable]
public class UIButton
{
    public string buttonName, sceneToOpen;
    public GameObject panelToOpen;
    public bool usesCustomTransformProperty;
    public Vector2 buttonPosition, buttonSize;
    public ButtonFunctions buttonFunction;
    public enum ButtonFunctions
    {
        OpenPanel,
        ClosePanel,
        ExitGame,
        LoadScene,
        OpenUIPage,
    }
}