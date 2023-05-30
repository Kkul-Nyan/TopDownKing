using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;

public class OdinDataTable : OdinMenuEditorWindow
{
    [MenuItem("Tools/DataTable")]
    private static void OpenWindow(){
        GetWindow<OdinDataTable>().Show();
    }
    protected override OdinMenuTree BuildMenuTree()
    {
        var tree = new OdinMenuTree();

        tree.Add("Create New Enemy", new CreateNewItemData());
        tree.AddAllAssetsAtPath("Item", "Assets/Resources", typeof(ItemData));

        return tree;
    }

    public class CreateNewItemData{
        public CreateNewItemData(){
            itemData = ScriptableObject.CreateInstance<ItemData>(); 
            itemData.displayName = "New Item Data";
        }
        [InlineEditor(ObjectFieldMode = InlineEditorObjectFieldModes.Hidden)]
        public ItemData itemData;

        [Button("Add New Item")]
        private void CreateNewData(){
            AssetDatabase.CreateAsset(itemData, "Assets/Resources/" + itemData.displayName + ".asset");
            AssetDatabase.SaveAssets();

            itemData = ScriptableObject.CreateInstance<ItemData>(); 
            itemData.displayName = "New Item Data";
        }
    }
}
