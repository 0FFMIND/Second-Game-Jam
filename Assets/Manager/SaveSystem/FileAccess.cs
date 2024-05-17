using System.IO;
using UnityEngine;
using System;

public static class FileAccess 
{
    private const string _defaultExtension = ".json";

    // 选择persistanceDataPath作为储存的目录，这是跨平台的，支持热更新的，但同时支持写入和读取的
    private static string BasePath => Path.Combine(Application.persistentDataPath, "NOC");
    public static bool SaveString(string filename, bool includesExtension, string value)
    {
        filename = GetFilenameWithExtension(filename, includesExtension);
        try
        {
            CreateRootFolder();
            using (StreamWriter writer = new StreamWriter(Path.Combine(BasePath, filename)))
            {
                writer.Write(value);
            }
            return true;
        }
        catch
        {

        }
        return false;
    }
    public static string LoadString(string fileName, bool includesExtension)
    {
        fileName = GetFilenameWithExtension(fileName, includesExtension);
        try
        {
            CreateRootFolder();

            if (Exist(fileName, true))
            {
                using(StreamReader reader = new StreamReader(Path.Combine(BasePath, fileName)))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        catch (Exception)
        {
        }
        return null;
    }
    private static string GetFilenameWithExtension(string filename, bool includesExtension)
    {
        return includesExtension ? filename : filename + _defaultExtension;
    }
    private static void CreateRootFolder()
    {
        if (!Directory.Exists(BasePath))
        {
            Directory.CreateDirectory(BasePath);
        }
    }
    public static bool Exist(string filename, bool includesExtension)
    {
        filename = GetFilenameWithExtension(filename, includesExtension);
        string fileLocation = Path.Combine(BasePath, filename);
        return File.Exists(fileLocation);
    }
}
