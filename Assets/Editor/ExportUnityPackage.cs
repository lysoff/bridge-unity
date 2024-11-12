using UnityEditor;

public class ExportUnityPackage
{
    public static void RunExport()
    {
        // Get the export path from the environment variable
        string exportPath = System.Environment.GetEnvironmentVariable("exportPath");
        if (string.IsNullOrEmpty(exportPath))
        {
            exportPath = "MyFolder"; // Default folder if no input is provided
        }

        // Define the path within Assets and the output file
        string folderPath = $"Assets/{exportPath}";
        string outputFileName = "exported-package.unitypackage";

        // Export the package
        AssetDatabase.ExportPackage(folderPath, outputFileName, ExportPackageOptions.Recurse);
        Debug.Log($"Exported {folderPath} to {outputFileName}");
    }
}
