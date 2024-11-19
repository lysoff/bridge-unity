using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
 
public class ExportUnityPackage
{
    [MenuItem("Tools/Export All Assets")]
    public static string ExportAllAssets()
    {
        string path = "build";
        string packageName = "playgama-bridge";

        // Define the file path for the exported package
        string exportPath = $"/{path}/{packageName}.unitypackage";

        var dir = new FileInfo(exportPath).Directory;
            if (dir != null && !dir.Exists) {
                dir.Create();
            }
 
        // Get all asset paths in the Assets folder
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
 
        // Filter paths to include only those that are under the "Assets" folder
       var assetsToExport = allAssetPaths.Where(path => path.StartsWith("Assets/") && !path.Equals("Assets")).ToArray();
 
        // Export all assets as a Unity package
        AssetDatabase.ExportPackage(
            assetsToExport, 
            exportPath, 
            ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
 
        var fullpath = Path.GetFullPath(exportPath);

        Debug.Log($"Path is: {fullpath}");

        return fullpath;
    }
}
