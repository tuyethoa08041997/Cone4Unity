using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Cone))] 
public class ConeEditor : Editor
{
	[MenuItem("GameObject/Create Other/Cone")]
	static void Create() {
		GameObject gameObject = new GameObject("Cone");
		Cone cone = gameObject.AddComponent<Cone>();
		MeshFilter meshFilter = gameObject.GetComponent<MeshFilter>();
		meshFilter.mesh = new Mesh();
		cone.Rebuild();
		cone.GetComponent<Renderer>().sharedMaterial = new Material(Shader.Find("Standard"));
		cone.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Color.white);
	}

	public override void OnInspectorGUI() {
		Cone obj = target as Cone;
		if (obj == null) {
			return;
		}

		base.DrawDefaultInspector();
		EditorGUILayout.BeginHorizontal();

		// Rebuild mesh when user click the Rebuild button
		if (GUILayout.Button("Rebuild")) {
			obj.Rebuild();
		}
		EditorGUILayout.EndHorizontal();
	}
}