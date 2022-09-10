using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

public static class DuplicateMaterialsMerger
{
    [MenuItem("Assets/EtAlii/Merge duplicate materials", false, -1)]
    public static void Run()
    {
        var knownMaterials = new Dictionary<string, List<Material>>();
        
        var allMaterials = Selection.GetFiltered<Material>(SelectionMode.DeepAssets);
        if (!allMaterials.Any())
        {
            Debug.LogError("No materials selected");
        }
        for(var i = 0; i < allMaterials.Length; i++)
        {
            var material = allMaterials[i];
            EditorUtility.DisplayProgressBar("Merging duplicate materials", $"Hashing {material.name}", (float)allMaterials.Length / i);
            var hash = CreateIdentifier(material);
            if (!knownMaterials.TryGetValue(hash, out var matchingMaterials))
            {
                matchingMaterials = knownMaterials[hash] = new List<Material>();
            }

            matchingMaterials.Add(material);
        }

        EditorUtility.DisplayProgressBar("Merging duplicate materials", $"Finding duplicates", 0.1f);

        var groupsToMerge = knownMaterials.Values.Where(l => l.Count > 1).ToArray();
        var totalDuplicates = groupsToMerge.Sum(l => l.Count);
        Debug.Log($"Found {groupsToMerge.Length} groups of duplicates, will merge {totalDuplicates - groupsToMerge.Length} materials");

        var projectFolder = Path.Combine( Application.dataPath, "../" );

        EditorUtility.DisplayProgressBar("Merging duplicate materials", $"Loading files to search", 0.2f);
        var supportedExtensions = new []
        {
            ".meta",
            ".unity",
            ".prefab",
            ".scene"
        };
        var files = Directory
            .GetFiles(projectFolder, "*.*", SearchOption.AllDirectories)
            .Where(s => supportedExtensions.Contains(Path.GetExtension(s).ToLower()))
            .ToArray();

        for (var i = 0; i < groupsToMerge.Length; i++)
        {
            var groupToMerge = groupsToMerge[i];

            var materialToKeep = groupToMerge[0];
            var materialIdToKeep = ToId(materialToKeep);
            var materialsToRemove = groupToMerge.Skip(1).ToArray();
            var materialIdsToRemove = materialsToRemove.Select(ToId).ToArray();
            Debug.Log($"Replacing {materialIdToKeep} for: {string.Join(", ", materialIdsToRemove)}");

            EditorUtility.DisplayProgressBar("Merging duplicate materials", $"Merging group {i}", (float)groupsToMerge.Length / i);

            foreach (var file in files)
            {
                ReplaceInFiles(materialIdToKeep, materialIdsToRemove, file);
            }

            foreach (var materialToRemove in materialsToRemove)
            {
                var assetPath = AssetDatabase.GetAssetPath(materialToRemove);
                AssetDatabase.DeleteAsset(assetPath);
            }
        }

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private static void ReplaceInFiles(string idToKeep, string[] idsToRemove, string file)
    {
        var content = File.ReadAllText(file);

        foreach (var idToRemove in idsToRemove)
        {
            content = content.Replace(idToRemove, idToKeep);
        }
        
        File.WriteAllText(file, content);
    }
    
    private static string ToId(Material material)
    {
        // AssetDatabase.TryGetGUIDAndLocalFileIdentifier(material, out string _, out long id);
        // return id.ToString();
        var assetPath = AssetDatabase.GetAssetPath(material);
        var guid = AssetDatabase.AssetPathToGUID(assetPath);
        return guid;
    }

    private static string CreateIdentifier(Material material)
    {
        var sb = new StringBuilder();
        sb.AppendLine(material.shader.ToString());
        sb.AppendLine(material.color.ToString());
        sb.AppendLine(material.enableInstancing.ToString());
        sb.AppendLine(material.globalIlluminationFlags.ToString());
        if (material.mainTexture != null)
        {
            sb.AppendLine(material.mainTexture.GetInstanceID().ToString());
        }
        sb.AppendLine(material.mainTextureOffset.ToString());
        sb.AppendLine(material.passCount.ToString());
        sb.AppendLine(material.renderQueue.ToString());
        sb.Append(material.shaderKeywords);
        sb.AppendLine(material.mainTextureScale.ToString());
        sb.AppendLine(material.doubleSidedGI.ToString());
        return sb.ToString();
    }
}
