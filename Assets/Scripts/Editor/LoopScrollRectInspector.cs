using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CustomEditor(typeof(LoopScrollRect), true)]
public class LoopScrollRectInspector : Editor
{
    int index = 0;
    float speed = 1000;
	public override void OnInspectorGUI ()
    {
        LoopScrollRect scroll = (LoopScrollRect)target;

        scroll.CellLoadType = (LoopScrollCellLoadType)EditorGUILayout.EnumPopup("CellLoadType", scroll.CellLoadType);
        if (scroll.CellLoadType == LoopScrollCellLoadType.Static)
        {
            scroll.CellSource = (GameObject)EditorGUILayout.ObjectField("DefaultCellPrefab", scroll.CellSource, typeof(GameObject), true);
        }

        base.OnInspectorGUI();
        EditorGUILayout.Space();

        GUI.enabled = Application.isPlaying;

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("Clear"))
        {
            scroll.ClearCells();
        }
        if (GUILayout.Button("Refresh"))
        {
            scroll.RefreshCells();
		}
		if(GUILayout.Button("Refill"))
		{
			scroll.RefillCells();
		}
		if(GUILayout.Button("RefillFromEnd"))
		{
			scroll.RefillCellsFromEnd();
		}
        EditorGUILayout.EndHorizontal();

        EditorGUIUtility.labelWidth = 45;
        float w = (EditorGUIUtility.currentViewWidth - 100) / 2;
        EditorGUILayout.BeginHorizontal();
        index = EditorGUILayout.IntField("Index", index, GUILayout.Width(w));
        speed = EditorGUILayout.FloatField("Speed", speed, GUILayout.Width(w));
        if(GUILayout.Button("Scroll", GUILayout.Width(45)))
        {
            scroll.SrollToCell(index, speed);
        }
        EditorGUILayout.EndHorizontal();
	}
}