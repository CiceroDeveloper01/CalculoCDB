using System;

namespace CalculoCDBShared.Attribute
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class TableDataBaseNameAttribute : System.Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
