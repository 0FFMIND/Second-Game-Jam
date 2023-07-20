using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveWriter : SaveBase
{
    private SaveWriter(string root, SaveSettings settings)
    {
        _root = root;
        _settings = settings;
    }
    //先判断是否事先生成了东西
    public static bool LoadString(string filename)
    {
        return LoadString(filename, new SaveSettings());
    }
    public static bool LoadString(string filename, SaveSettings settings)
    {
        var content = FileAccess.LoadString(filename, true);
        if(content != null)
        {
            return true;
        }else 
        {
            return false;
        }
    }
    //这里为创建的入口，用root存储信息，比方说传入"Settings"或者当前"SceneName";
    public static SaveWriter Create(string root)
    {
        return Create(root, new SaveSettings());
    }
    public static SaveWriter Create(string root, SaveSettings settings)
    {
        SaveWriter saveWriter = new SaveWriter(root, settings);
        saveWriter.Load(true);
        return saveWriter;
    }
    public SaveWriter Write<T>(string key, T value)
    {
        if (Exist(key))
        {
            _items.Remove(key);
        }
        _items.Add(key, JsonSerialiser.SerializeKey(value));
        return this;
    }
    public void Commit()
    {
        Save();
    }
}
