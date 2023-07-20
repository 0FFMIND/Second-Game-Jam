using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveReader : SaveBase
{
    private SaveReader(string root, SaveSettings settings)
    {
        _root = root;
        _settings = settings;
    }
    public static SaveReader Create(string root)
    {
        return Create(root, new SaveSettings());
    }
    public static SaveReader Create(string root, SaveSettings settings)
    {
        SaveReader saveReader = new SaveReader(root, settings);
        saveReader.Load(false);
        return saveReader;
    }
    public SaveReader Read<T>(string key, Action<T> result)
    {
        if (!Exist(key))
        {
            Debug.LogError("Key does not Exist");
        }
        try
        {
            result(JsonSerialiser.DeserializeKey<T>(key, _items));
        }
        catch
        {
            Debug.Log("Deserialisation failed");
        }
        return this;
    }
}
