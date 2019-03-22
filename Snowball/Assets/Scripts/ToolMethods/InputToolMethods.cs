using UnityEngine;
using UnityEditor;

public static class InputToolMethod
{
    public static string lastInputString =null;

    public static bool IsInputRepeated() {
        Debug.Log("Called");
        bool isRepeated = false;
        if (ReturnInputString() == null)
        {
            lastInputString = null;        
        }
        if (ReturnInputString() == lastInputString) {
            isRepeated = true;
        }
        else if (ReturnInputString() != lastInputString)
        {
            lastInputString = ReturnInputString();
        }
        return isRepeated;
    }

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
                break;
            }
            else if (Input.GetAxis(name) != 0)
            {
                buttonString = name;
                break;
            }

        }
        return buttonString;
    }

    public static string ReturnJoyStickOrder(string inputString) {
        string JoyStickOrder = inputString.Substring(0, 3);
        return JoyStickOrder;
    }

}
