using UnityEditor;
using System.IO;
using System.Linq;
 
public class ExportUnityPackage
{
    [MenuItem("Tools/Export All Assets")]
    public static void ExportAllAssets()
    {
        var path = "build";
        var packageName = "playgama-bridge";

        // Define the file path for the exported package
        var exportPath = $"{path}/{packageName}.unitypackage";

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
 

        return Path.GetFullPath(exportPath);
        // Debug.Log("All assets have been exported to: " + packagePath);
    }
}
