using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
 
public class ExportUnityPackage
{  
   // The name of the unitypackage to output.
   const string packageName = "playgama-bridge";
   
   // Path to export to.
   const string exportPath = "Build/package";

   [MenuItem("Tools/Export All Assets")]
   public static void ExportAllAssets () {
    ExportPackage($"{exportPath}/{packageName}.unitypackage");
   }
 
   public static string ExportPackage (string exportPath) {
    string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
 
    // Filter paths to include only those that are under the "Assets" folder
    var assetsToExport = allAssetPaths.Where(path => path.StartsWith("Assets/") && !path.Equals("Assets")).ToArray();
 
    
    // Ensure export path.
    var dir = new FileInfo(exportPath).Directory;
    if (dir != null && !dir.Exists) {
     dir.Create();
    }
 
    // Export
    AssetDatabase.ExportPackage(
     assetsToExport,
     exportPath,
     ExportPackageOptions.Recurse
    );
 
    return Path.GetFullPath(exportPath);
   }
}
