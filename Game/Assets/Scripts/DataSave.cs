using System;
using System.Globalization;
using UnityEngine;

public static class DataSave
{
    public static void SetDateTime(DateTime value)
    {
        var convert = value.ToString("u", CultureInfo.InvariantCulture);
        PlayerPrefs.SetString("Date", convert);
    }

    public static DateTime GetDateTime(DateTime defaultValue)
    {
        if (PlayerPrefs.HasKey("Date"))
        {
            var stored = PlayerPrefs.GetString("Date");
            var result = DateTime.ParseExact(stored, "u", CultureInfo.InvariantCulture);
            return result;
        }
        else
            return defaultValue;
    }
}
