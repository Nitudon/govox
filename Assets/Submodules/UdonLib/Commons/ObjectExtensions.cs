using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//ネットでいいなあって思ったやつ
public static class ObjectExtensions{

    private const string SEPARATOR = ",";
    private const string FORMAT = "{0}:{1}";

    public static string ToStringFields<T>(this T obj)
    {
        return string.Join(SEPARATOR, obj
            .GetType()
            .GetFields(BindingFlags.Instance | BindingFlags.Public)
            .Select(str => string.Format(FORMAT, str.Name, str.GetValue(obj)))
            .ToArray()
            );
    }

    public static string ToStringProperties<T>(this T obj)
    {
        return string.Join(SEPARATOR, obj
            .GetType()
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .Where(str => str.CanRead)
            .Select(str => string.Format(FORMAT, str.Name, str.GetValue(obj,null)))
            .ToArray()
            );
    }

    public static string ToStringReflection<T>(this T obj)
    {
        return string.Join(SEPARATOR, new string[] { obj.ToStringFields(),obj.ToStringProperties()});
    }

}
