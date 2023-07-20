using System.IO;
using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System;

public static class FileAccess 
{
    private const string _defaultExtension = ".json";
    private static string BasePath => Path.Combine(Application.persistentDataPath, "CGJ");
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
