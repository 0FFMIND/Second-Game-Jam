using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using System;

public abstract class SaveBase
{
    protected JObject _items;
    protected string _root;
    protected SaveSettings _settings;
    public bool Exist(string key)
    {
        return _items[key] != null;
    }
    protected void Load(bool rootMightNotExist)
    {
        var json = FileAccess.LoadString(_root, false);

        if (string.IsNullOrEmpty(json))
        {
            if (rootMightNotExist)
            {
                _items = new JObject();
                return;
            }
            Debug.LogError("Roots not exist");
        }
        try
        {
            _items = JObject.Parse(json);
        }
        catch (Exception)
        {
            Debug.LogError("Deserialization failed");
        }
    }
    protected void Save()
    {
        string json = null;
        try
        {
            json = JsonSerialiser.Serialize(_items);
        }
        catch (Exception)
        {
            Debug.LogError("Serialization failed");
        }
        if(!FileAccess.SaveString(_root, false, json))
        {
            Debug.LogError("Failed to write to file");
        }
    }

}
