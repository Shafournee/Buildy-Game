using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GenerateCube))]
public class GenerateCubeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        DrawDefaultInspector();

        GenerateCube myScript = (GenerateCube)target;
        if(GUILayout.Button("Generate Cube"))
        {
            myScript.BuildCube();
        }
    }
}
