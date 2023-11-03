using System;

namespace CalculoCDBShared.GenerateSQL;

[AttributeUsage(AttributeTargets.Property)]
public class DapperKey : System.Attribute { }

[AttributeUsage(AttributeTargets.Property)]
public class DapperIgnore : System.Attribute { }