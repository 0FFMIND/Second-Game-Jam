using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SecurityMode
{
    None,
    Aes,
    Base64,
}
public enum CompressionMode
{
    None,
    Gzip,
}
public class SaveSettings
{
    public SecurityMode SecurityMode { get; set; }
    public CompressionMode CompressionMode { get; set; }
    public string Password { get; set; }
    public SaveSettings()
    {
        SecurityMode = SecurityMode.None;
        CompressionMode = CompressionMode.None;
    }
}
