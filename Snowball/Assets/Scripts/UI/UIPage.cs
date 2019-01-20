using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "NewUIPage", menuName = "UI/UIPage")]
public class UIPage : ScriptableObject
{
    public List<UIPanel> panels;
}

//Note : Create panel presets directly under the main canvas and not under other panels.
[System.Serializable]
public class UIPanel
{
    public string panelName;
    public string panelPresetName;
    public Vector2 panelPosition = new Vector2();
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
    public string buttonName, panelName, sceneName;
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