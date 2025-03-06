using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using CsvHelper.Configuration;

namespace MauiBlazor.Shared.Services;




public static class CSVService
{

    private static readonly CsvConfiguration config = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        Delimiter = ",",
        IgnoreBlankLines = true,
        TrimOptions =TrimOptions.Trim,
    };

    private static readonly string tmpFilePath = System.IO.Path.GetTempFileName();


    public static string  WriteCSV<T>(IEnumerable<T> records)
    {
        return WriteCSV(tmpFilePath, records);
    }

    public static  string WriteCSV<T>(string path, IEnumerable<T> records)
    {
        try { 
        using var writer = new StreamWriter(path);
        using var csv = new CsvWriter(writer, config);

        csv.WriteRecords(records);
        }
        catch (Exception e)
        {
            Debug.WriteLine(e.Message);
            throw;
        }
        return path;
    }
}
