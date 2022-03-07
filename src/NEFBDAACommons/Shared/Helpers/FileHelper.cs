using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;
using NEFBDAACommons.Shared.Extensions;
using NEFBDAACommons.Shared.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NEFBDAACommons.Shared.Helpers
{
  public class AppFileConfig
  {
    public string TmpWorkingFolder { get; set; }
    public string DstWorkingFolder { get; set; }
    public string UploadsWorkingFolder { get; set; }
  }

  public static class FileHelper

  {
    public static AppFileConfig Config { get; private set; }

    static FileHelper()
    {
      Config = new AppFileConfig()
      {
        TmpWorkingFolder = @"tmp\",
        DstWorkingFolder = @"dst\",
        UploadsWorkingFolder = $"{CommonsConstants.UploadsFolder}\\"
      };
      CheckStructure();
    }
    public static byte[] ReadAsBytes(DocumentViewModel document)
    {
      return File.ReadAllBytes(Path.Combine(AppDataFolder(), document.FilePath));
    }
    public static string[] ReadAsLines(DocumentViewModel document)
    {
      return File.ReadAllLines(Path.Combine(AppDataFolder(), document.FilePath));
    }

    public static byte[] ReadAsBytes(string directory, string fileName)
    {
      return File.ReadAllBytes(Path.Combine(directory, fileName));
    }
    /// <summary>
    /// Create tmpfile in tmp folder inside appdata dir
    /// </summary>
    /// <param name="filename"></param>
    /// <returns>full path to created tmp file</returns>
    public static string CreateTmpFile(string filename)
    {
      return CreateFile(TmpDir(), filename);
    }
    public static string CreateFile(string directory, string filename, string content = "")
    {
      return CreateFile(new string[] { directory }, filename, content);
    }
    public static string CreateFile(IEnumerable<string> dirs, string filename, string content = "")
    {
      CheckStructure();
      var destinationFile = GetUniqueFileName(filename);
      var destinationFolder = CombineAndCheckDirStructure(dirs.ToArray());
      var filePath = Path.Combine(destinationFolder, destinationFile);
      File.WriteAllText(filePath, content);
      return filePath;
    }
    public static string CreateFile(IEnumerable<string> dirs, string filename, byte[] content)
    {
      CheckStructure();
      var destinationFile = GetUniqueFileName(filename);
      var destinationFolder = CombineAndCheckDirStructure(dirs.ToArray());
      var filePath = Path.Combine(destinationFolder, destinationFile);
      File.WriteAllBytes(filePath, content);
      return filePath;
    }
    public static void CreateDirIfNotExists(string path)
    {
      System.IO.Directory.CreateDirectory(path);
    }

    public static DocumentViewModel UploadAndSaveFile(IFormFile file, Guid ownerGuid, string ownerDocumentType = "", string parentDir = "")
    {
      var originalFileName = file.FileName;
      var dstFileName = GetUniqueFileName(originalFileName);
      var pathToSave = CombineAndCheckDirStructure(UploadsDir(), parentDir, ownerGuid.ToString());
      var fullPath = Path.Combine(pathToSave, dstFileName);
      using (var stream = new FileStream(fullPath, FileMode.Create))
      {
        file.CopyTo(stream);
      }
      return new DocumentViewModel()
      {
        ContentType = file.ContentType,
        OriginalFilename = originalFileName,
        OwnerGuid = ownerGuid,
        OwnerDocumentType = ownerDocumentType,
        FilePath = fullPath,
        FileSize = FormattingHelper.FormatFileSizeInMb(file.Length)
      };
    }
    public static string GetFileSizeInMbAsString(string filename)
    {
      return FormattingHelper.FormatFileSizeInMb(GetFileSize(filename));
    }
    public static long GetFileSize(string filename)
    {
      return new FileInfo(filename).Length;
    }

    public static string GetContentTypeBasedOnFileExtension(string path)
    {
      var provider = new FileExtensionContentTypeProvider();
      var contentType = "application/octet-stream";
      provider.TryGetContentType(path, out contentType);
      return contentType;
    }
    private static string GetUniqueFileName(string originalFileName)
    {
      return $"{Path.GetFileNameWithoutExtension(originalFileName)}_{Guid.NewGuid().ToString()}{Path.GetExtension(originalFileName)}";
    }

    public static string AppDataFolder()
    {
      //var userPath = Environment.GetEnvironmentVariable(
      //  RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ?
      //  "LOCALAPPDATA" : "Home");
      //var assy = System.Reflection.Assembly.GetEntryAssembly();
      //var companyName = assy.GetCustomAttributes<AssemblyCompanyAttribute>()
      //  .FirstOrDefault();
      //var path = System.IO.Path.Combine(userPath, companyName.Company);

      var location = Assembly.GetEntryAssembly().Location;
      if (Assembly.GetEntryAssembly().Location.IndexOf("bin\\") > -1)
      {
        location = Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.IndexOf("bin\\"));
      }
      return Path.GetDirectoryName(location);
    }

    public static string ClearFilePath(string filePath)
    {
      return filePath?.Replace(AppDataFolder(), "").Replace("\\", "/");
    }

    private static void CheckStructure()
    {
      CreateDirIfNotExists(UploadsDir());
      CreateDirIfNotExists(DstDir());
      CreateDirIfNotExists(TmpDir());
    }
    public static string CombineAndCheckDirStructure(params string[] dirs)
    {
      var finalPath = "";
      dirs.Where(x => !string.IsNullOrEmpty(x)).ForEach(dir =>
        {
          finalPath = Path.Combine(finalPath, dir);
          CreateDirIfNotExists(finalPath);
        });
      return finalPath;
    }




    public static string UploadsDir()
        => Path.Combine(AppDataFolder(), Config.UploadsWorkingFolder);
    public static string DstDir()
        => Path.Combine(AppDataFolder(), Config.DstWorkingFolder);
    public static string TmpDir()
        => Path.Combine(AppDataFolder(), Config.TmpWorkingFolder);
  }
}
