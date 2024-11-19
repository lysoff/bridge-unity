// using UnityEditor;
// using UnityEngine;
// using System.Linq;
 
// public class ExportUnityPackage
// {
//     [MenuItem("Tools/Export All Assets")]
//     public static void ExportAllAssets()
//     {
//         // Define the file path for the exported package
//         string packagePath = EditorUtility.SaveFilePanel(
//             "Export All Assets",
//             "",
//             "playgama-bridge.unitypackage",
//             "unitypackage");
 
//         if (string.IsNullOrEmpty(packagePath))
//         {
//             // Debug.LogWarning("Export canceled by the user.");
//             return;
//         }
 
//         // Get all asset paths in the Assets folder
//         string[] allAssetPaths = AssetDatabase.GetAllAssetPaths();
 
//         // Filter paths to include only those that are under the "Assets" folder
//        var assetsToExport = allAssetPaths.Where(path => path.StartsWith("Assets/") && !path.Equals("Assets")).ToArray();
 
//         // Export all assets as a Unity package
//         AssetDatabase.ExportPackage(assetsToExport, packagePath, ExportPackageOptions.Interactive | ExportPackageOptions.Recurse);
 
//         // Debug.Log("All assets have been exported to: " + packagePath);
//     }
// }

using System.IO;
using UnityEditor;

namespace ExportPackageExample.Editor {
	public static class ExportUnityPackage {

		// The name of the unitypackage to output.
		const string k_PackageName = "ExportPackageExample";

		// The path to the package under the `Assets/` folder.
		const string k_PackagePath = "ExportPackageExample";

		// Path to export to.
		const string k_ExportPath = "Build";

		public static void Export () {
			ExportPackage($"{k_ExportPath}/{k_PackageName}.unitypackage");
		}

		public static string ExportPackage (string exportPath) {
			// Ensure export path.
			var dir = new FileInfo(exportPath).Directory;
			if (dir != null && !dir.Exists) {
				dir.Create();
			}

			// Export
			AssetDatabase.ExportPackage(
				$"Assets/{k_PackagePath}",
				exportPath,
				ExportPackageOptions.Recurse
			);

			return Path.GetFullPath(exportPath);
		}

	}
}
