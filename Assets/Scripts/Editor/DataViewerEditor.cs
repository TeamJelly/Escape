using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(DataViewer))]
public class DataViewerEditor : Editor
{
/*    DataViewer dataViewer;
    void OnEnable()
    {
        dataViewer = target as DataViewer;
    }*/

    public override void OnInspectorGUI()
    {
        foreach (string key in PlayerData.statesKeys)
        {
//            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.Toggle(key, DataManager.GetData().states[key]);


/*            if (GUILayout.Button("Button"))
            {
                if (DataManager.GetData().states[key] == true)
                    DataManager.GetData().states[key] = false;
                else
                    DataManager.GetData().states[key] = true;
            }
            EditorGUILayout.EndHorizontal();*/
        }
        if (GUILayout.Button("ForceUpdate"))
            OnInspectorGUI();


    }

}
