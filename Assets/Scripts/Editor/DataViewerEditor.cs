using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataViewer))]
public class DataViewerEditor : Editor
{
    Dictionary<string, bool> states;

    public override void OnInspectorGUI()
    {
        if (DataManager.currentData == null)
            return;
        else
            states = DataManager.GetData().states;

        foreach (string key in PlayerData.statesKeys)
        {
            EditorGUI.BeginChangeCheck();
            bool _bool = EditorGUILayout.Toggle(key, states[key]);
            if (EditorGUI.EndChangeCheck())
            {
                states[key] = _bool;
                InventoryManager.instance.UpdateInventory();
            }
        }
    }
}
