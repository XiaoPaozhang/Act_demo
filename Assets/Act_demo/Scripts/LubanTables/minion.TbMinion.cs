
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Luban;
using SimpleJSON;


namespace cfg.minion
{
public partial class TbMinion
{
    private readonly System.Collections.Generic.Dictionary<int, Minion> _dataMap;
    private readonly System.Collections.Generic.List<Minion> _dataList;
    
    public TbMinion(JSONNode _buf)
    {
        _dataMap = new System.Collections.Generic.Dictionary<int, Minion>();
        _dataList = new System.Collections.Generic.List<Minion>();
        
        foreach(JSONNode _ele in _buf.Children)
        {
            Minion _v;
            { if(!_ele.IsObject) { throw new SerializationException(); }  _v = Minion.DeserializeMinion(_ele);  }
            _dataList.Add(_v);
            _dataMap.Add(_v.Id, _v);
        }
    }

    public System.Collections.Generic.Dictionary<int, Minion> DataMap => _dataMap;
    public System.Collections.Generic.List<Minion> DataList => _dataList;

    public Minion GetOrDefault(int key) => _dataMap.TryGetValue(key, out var v) ? v : null;
    public Minion Get(int key) => _dataMap[key];
    public Minion this[int key] => _dataMap[key];

    public void ResolveRef(Tables tables)
    {
        foreach(var _v in _dataList)
        {
            _v.ResolveRef(tables);
        }
    }

}

}

