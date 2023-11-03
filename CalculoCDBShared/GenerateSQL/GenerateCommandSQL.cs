using SALES.Integracao.Vtex.Repository.Base;
using System;
using System.Linq;

namespace CalculoCDBShared.GenerateSQL;

public static class GenerateCommandSql
{
    public static PropertyContainer ParseProperties(object obj)
    {
        var propertyContainer = new PropertyContainer();

        var typeName = obj.GetType().Name;
        var validKeyNames = new[] { "Idx", string.Format("{0}Id", typeName), string.Format("{0}_Idx", typeName) };
        var properties = obj.GetType().GetProperties();

        foreach (var property in properties)
        {
            // Skip reference types (but still include string!)
            if (property.PropertyType.IsClass && property.PropertyType != typeof(string))
                continue;

            // Skip methods without a public setter
            if (property.GetSetMethod() == null)
                continue;

            // Skip methods specifically ignored
            if (property.IsDefined(typeof(object), false))
                continue;

            var name = property.Name;
            var value = obj.GetType().GetProperty(property.Name).GetValue(obj, null); 

            if (property.IsDefined(typeof(DapperKey), false) || validKeyNames.Contains(name))
                propertyContainer.AddId(name, value);
            else
                propertyContainer.AddValue(name, value);
        }
        return propertyContainer;
    }
}