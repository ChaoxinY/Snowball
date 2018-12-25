using System;
using System.Collections;
using System.Reflection;

public static class SystemToolMethods
{
    public static Object ReturnObjectComponent(Object referenceObject, PropertyInfo propertyInfo, string name)
    {
        Object referenceObjectComponent = propertyInfo.GetValue(referenceObject, null);
        return referenceObjectComponent;
    }

    public static PropertyInfo ReturnPropertyInfo(Object referenceObject, string name)
    {
        Type classType = referenceObject.GetType();
        PropertyInfo propertyInfo = classType.GetProperty(name);
        return propertyInfo;
    }

    public static bool CheckIfPropertyExsists(Object referenceObject, string name)
    {
        bool propertyExsists = false;
        Type classType = referenceObject.GetType();
        if (classType.GetProperty(name) != null)
        {
            propertyExsists = true;
        }
        return propertyExsists;
    }
}
