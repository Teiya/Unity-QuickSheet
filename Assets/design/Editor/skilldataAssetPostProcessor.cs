using UnityEngine;
using UnityEditor;
using System.Collections;
using System.IO;
using UnityQuickSheet;

///
/// !!! Machine generated code !!!
///
public class skilldataAssetPostprocessor : AssetPostprocessor 
{
    private static readonly string filePath = "Assets/design/skill.xlsx";
    private static readonly string assetFilePath = "Assets/database/skilldata.asset";
    private static readonly string sheetName = "skilldata";
    private static readonly string keyListStr = "id$hitLevel";
    
    static void OnPostprocessAllAssets (string[] importedAssets, string[] deletedAssets, string[] movedAssets, string[] movedFromAssetPaths)
    {
        foreach (string asset in importedAssets) 
        {
            if (!filePath.Equals (asset))
                continue;
                
            skilldata data = (skilldata)AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(skilldata));
            if (data == null) {
                data = ScriptableObject.CreateInstance<skilldata> ();
                data.SheetName = filePath;
                data.WorksheetName = sheetName;
                data.KeyListStr = keyListStr;
                AssetDatabase.CreateAsset ((ScriptableObject)data, assetFilePath);
                //data.hideFlags = HideFlags.NotEditable;
            }
            
            //data.dataArray = new ExcelQuery(filePath, sheetName).Deserialize<skilldataData>().ToArray();		

            //ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
            //EditorUtility.SetDirty (obj);

            ExcelQuery query = new ExcelQuery(filePath, sheetName);
            if (query != null && query.IsValid())
            {
                data.dataArray = query.Deserialize<skilldataData>().ToArray();
                ScriptableObject obj = AssetDatabase.LoadAssetAtPath (assetFilePath, typeof(ScriptableObject)) as ScriptableObject;
                EditorUtility.SetDirty (obj);
            }
        }
    }
}
