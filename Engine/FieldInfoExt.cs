namespace Engine;

using System.Reflection;

public static class FieldInfoExt
{
    public static bool IsSaveable(this FieldInfo fieldInfo)
    {
        return fieldInfo.FieldType.IsPrimitive || fieldInfo.FieldType == typeof(string) && !fieldInfo.IsLiteral;
    }
}