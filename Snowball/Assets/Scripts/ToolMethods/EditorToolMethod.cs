using UnityEngine;
using UnityEditor;
using System.Collections;

public static class EditorToolMethod 
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
            //Might not work if controller axis are considered as buttons 
            if (Input.GetAxis(name)!=0)
            {
                buttonString = name;
                Debug.Log(name);
                break;
            }
        }
        return buttonString;
    }

}
