using UnityEngine;
using UnityEditor;
using System.Collections;

public static class InputToolMethod 
{
    public static string ReturnInputString()
    {
        string buttonString = null;
        Object inputManager = AssetDatabase.LoadAllAssetsAtPath("ProjectSettings/InputManager.asset")[0];
        SerializedObject obj = new SerializedObject(inputManager);
        SerializedProperty axisArray = obj.FindProperty("m_Axes");

        for (int i = 0; i < axisArray.arraySize; ++i)
        {
            SerializedProperty axis = axisArray.GetArrayElementAtIndex(i);
            string name = axis.FindPropertyRelative("m_Name").stringValue;
            //int axisType = axis.FindPropertyRelative("type").enumValueIndex;
            //Might not work if controller axis are considered as buttons 
            //Not returning string when hitting buttons 
            if (Input.GetButton(name))
            {
                buttonString = name;
                Debug.Log(name);
                break;
            }
            else if (Input.GetAxis(name) != 0)
            {
                buttonString = name;
                break;
            }

        }
        Debug.Log(buttonString);
        return buttonString;
    }

}
