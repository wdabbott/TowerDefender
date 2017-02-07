using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Path))]
class PathEditor : Editor
{
    public override void OnInspectorGUI() 
    {
        var path = target as Path;

        path.sprite = (Sprite)EditorGUILayout.ObjectField(path.sprite, typeof (Sprite), true);

        for (int i = 0; i < path.Waypoints.Count; i++)
        {
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("+", GUILayout.Width(25)))
            {
                path.AddWaypoint();
            }

            if (GUILayout.Button("-", GUILayout.Width(25)))
            {
                path.RemoveWaypoint(i);
            }
            path.Waypoints[i] = (GameObject)EditorGUILayout.ObjectField(path.Waypoints[i], typeof(GameObject), true);
            GUILayout.EndHorizontal();
        }

        if (path.Waypoints.Count == 0)
        {
            if (GUILayout.Button("+", GUILayout.Width(25)))
            {
                path.AddWaypoint();
            }
        }
    }
}