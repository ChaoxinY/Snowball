using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class SystemToolMethods
{
    public static object ReturnObjectComponent(object referenceObject, PropertyInfo propertyInfo, string name)
    {
		object referenceObjectComponent = propertyInfo.GetValue(referenceObject, null);
        return referenceObjectComponent;
    }

    public static PropertyInfo ReturnPropertyInfo(object referenceObject, string name)
    {
        Type classType = referenceObject.GetType();
        PropertyInfo propertyInfo = classType.GetProperty(name);
        return propertyInfo;
    }

    public static bool CheckIfPropertyExsists(object referenceObject, string name)
    {
        bool propertyExsists = false;
        Type classType = referenceObject.GetType();
        if (classType.GetProperty(name) != null)
        {
            propertyExsists = true;
        }
        return propertyExsists;
    }

    public static List<object> ReturnObjectPointers(object referenceObject, List<string> requirementPointerNames)
    {
        List<object> pointers = new List<object>();
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

    public static void UpdateIUpdaters(List<IUpdater> updaters)
    {
        foreach (IUpdater updater in updaters)
        {
            updater.UpdateComponent();
        }
    }

	/// <summary>
	/// Look for object type in transform type. 
	/// </summary>
	/// <param name="transform">Parent transform</param>
	/// <param name="objecTypeTocheck">Type To search</param>
	/// <param name="resultList">List to store</param>
	public static void TransformRecursionSearch<T>(Transform transform,List<T> resultList)
	{
		if (transform.childCount > 0)
		{
			Transform[] childTransforms = transform.GetComponentsInChildren<Transform>();
			for (int i = 0; i < childTransforms.Length; i++)
			{
				if (childTransforms[i] != transform)
				{
					if (childTransforms[i].GetComponent<T>() != null)
					{
						resultList.Add(childTransforms[i].GetComponent<T>());
					}
					TransformRecursionSearch(childTransforms[i], resultList);
				}
			}	
		}
	}
}
