using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class SystemToolMethods
{
    public static System.Object ReturnObjectComponent(System.Object referenceObject, PropertyInfo propertyInfo, string name)
    {
        System.Object referenceObjectComponent = propertyInfo.GetValue(referenceObject, null);
        return referenceObjectComponent;
    }

    public static PropertyInfo ReturnPropertyInfo(System.Object referenceObject, string name)
    {
        Type classType = referenceObject.GetType();
        PropertyInfo propertyInfo = classType.GetProperty(name);
        return propertyInfo;
    }

    public static bool CheckIfPropertyExsists(System.Object referenceObject, string name)
    {
        bool propertyExsists = false;
        Type classType = referenceObject.GetType();
        if (classType.GetProperty(name) != null)
        {
            propertyExsists = true;
        }
        return propertyExsists;
    }

    public static List<System.Object> ReturnObjectPointers(System.Object referenceObject, List<string> requirementPointerNames)
    {
        List<System.Object> pointers = new List<object>();
        foreach (string requirementName in requirementPointerNames)
        {
            if (CheckIfPropertyExsists(referenceObject, requirementName))
            {
                PropertyInfo propertyInfo = ReturnPropertyInfo(referenceObject, requirementName);
                pointers.Add(ReturnObjectComponent(referenceObject, propertyInfo, requirementName));
            }
            else
            {
                pointers = null;
                break;
            }
        }
        return pointers;
    }
}
