using UnityEditor;
using UnityEngine;
using System.Linq;
 
public class ExportUnityPackage
{
    [MenuItem("Tools/Export All Assets")]
    public static void ExportAllAssets()
    {
        // Define the file path for the exported package
        string packagePath = EditorUtility.SaveFilePanel(
            "Export All Assets",
            "",
            "AllAssetsPackage.unitypackage",
            "unitypackage");
 
        if (string.IsNullOrEmpty(packagePath))
        {
            Debug.LogWarning("Export canceled by the user.");
            return;
        }
 
        // Get all asset paths in the Assets folder
        string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
 
        // Filter paths to include only those that are under the "Assets" folder
       var assetsToExport = allAssetPaths.Where(path => path.StartsWith("Assets/") && !path.Equals("Assets")).ToArray();
 
        // Export all assets as a Unity package
        AssetDatabase.ExportPackage(assetsToExport, packagePath, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
 
        // Debug.Log("All assets have been exported to: " + packagePath);
    }
}