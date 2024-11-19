using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
 
public class ExportUnityPackage
{
    [MenuItem("Tools/Export All Assets")]
    public static string ExportAllAssets()
    {
        string packageName = "playgama-bridge";

        // Define the file path for the exported package
        string exportPath = Path.Combine(Application.dataPath, "../build/WebGL", $"{packageName}.unitypackage");

        var dir = new FileInfo(exportPath).Directory;
            if (dir != null && !dir.Exists) {
                dir.Create();
            }
 
        // Get all asset paths in the Assets folder
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
 
        // Filter paths to include only those that are under the "Assets" folder
       var assetsToExport = allAssetPaths.Where(path => path.StartsWith("Assets/") && !path.Equals("Assets")).ToArray();

        Debug.Log($"################## AssetDatabase.ExportPackage BEGIN");
        // Export all assets as a Unity package
        AssetDatabase.ExportPackage(
            assetsToExport, 
            exportPath, 
            ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
       Debug.Log($"################## AssetDatabase.ExportPackage END");
 
        var fullpath = Path.GetFullPath(exportPath);

        // FileInfo fileInfo = new FileInfo(exportPath);
        // long fileSize = fileInfo.Length; // File size in bytes
        // Debug.Log($"File: {fullpath}, Size: {fileSize} bytes");

        Debug.Log($"Path is: {fullpath}");

        return fullpath;
    }
}
