using System.Collections.Generic;
using System.Linq;

namespace SALES.Integracao.Vtex.Repository.Base;

public class PropertyContainer
{
    private readonly Dictionary<string, object> _ids;
    private readonly Dictionary<string, object> _values;
    internal PropertyContainer()
    {
        _ids = new Dictionary<string, object>();
        _values = new Dictionary<string, object>();
    }
    public IEnumerable<string> IdNames
    {
        get { return _ids.Keys; }
    }
    public IEnumerable<string> ValueNames
    {
        get { return _values.Keys; }
    }
    public IEnumerable<string> AllNames
    {
        get { return _ids.Keys.Union(_values.Keys); }
    }
    public IDictionary<string, object> IdPairs
    {
        get { return _ids; }
    }
    public IDictionary<string, object> ValuePairs
    {
        get { return _values; }
    }
    public IEnumerable<KeyValuePair<string, object>> AllPairs
    {
        get { return _ids.Concat(_values); }
    }
    internal void AddId(string name, object value)
    {
        _ids.Add(name, value);
    }
    internal void AddValue(string name, object value)
    {
        _values.Add(name, value);
    }
}
