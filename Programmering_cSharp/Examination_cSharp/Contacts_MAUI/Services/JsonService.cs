﻿using System.Diagnostics;
using Contacts_MAUI.Interfaces;

namespace Contacts_MAUI.Services;

public class JsonService : IJsonService
{

    // Skriver ut inskickade kontakter mot Json-fil i angiven sökväg
    public bool SaveToJson(string contentAsJson, string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                File.Create(filePath).Close();
            }

            using var sw = new StreamWriter(filePath);
            sw.WriteLine(contentAsJson);
            return true;
        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return false;
    }

    // Hämtar kontakterna i Json-fil enligt sökvägen som skickats in
    public string ReadFromJson(string filePath)
    {
        try
        {
            // Kollar om det finns befintlig fil, annars försöker den inte hämta data från sökvägen
            if (File.Exists(filePath))
            {
                using var sr = new StreamReader(filePath);
                return sr.ReadToEnd();
            }

        }
        catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null;
    }

}

