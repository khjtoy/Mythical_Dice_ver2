using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Show2DArray : EditorWindow
{
    Object cube;
    int[][] debug2d;

    [MenuItem("Tools/Map Cost Checker")]
    static void init()
    {
        Debug.Log("init");

        EditorWindow wnd = GetWindow<Show2DArray>();
        wnd.titleContent = new GUIContent("Map Cost Checker");
    }

    void OnInspectorUpdate()
    {
        Repaint();
    }

    void OnGUI()
    {
        GUILayout.BeginHorizontal();
        cube = EditorGUILayout.ObjectField(cube, typeof(Object), true);
        GUILayout.EndHorizontal();
        if (!Application.isPlaying)
            return;
        if (cube == null) return;
        GameObject go = (GameObject)cube;
        MapController cu = go.GetComponent<MapController>();

        //Cube cu = GameObject.Find("Cube").GetComponent<Cube>();
        debug2d = new int[1000][];
        debug2d = cu.MapNum.Clone() as int[][];
        for (int i = GameManager.Instance.Size - 1; i >= 0 ; i--)
        {
            GUILayout.BeginHorizontal();
            for (int k = 0; k < GameManager.Instance.Size; k++)
                debug2d[i][k] = EditorGUILayout.IntField(debug2d[i][k]);
            GUILayout.EndHorizontal();
        }
    }
}