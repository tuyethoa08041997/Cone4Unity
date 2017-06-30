using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]

public class Cone : MonoBehaviour
{
	public float height = 2.0f;
	public float top_radius = 0.0f;
	public float bottom_radius = 1.0f;
	public int sections = 30;

	public void Rebuild() {
		// check some condition
		if (sections < 3) {
			Debug.LogError("num of sections must be 3 or more");
			return;
		}
		if (top_radius < 0.0f) {
			Debug.LogWarning("top_radius is negative, get absolute value...");
			top_radius = Mathf.Abs(top_radius);
		}
		if (bottom_radius < 0.0f) {
			Debug.LogWarning("bottom_radius is negative, get absolute value...");
			bottom_radius = Mathf.Abs(bottom_radius);
		}
		if (top_radius == bottom_radius) {
			Debug.LogError("top_radius and bottom_radius are the same, please use a Cylinder!");
			return;
		}
		if (top_radius > bottom_radius) {
			Debug.LogWarning("top_radius is bigger than bottom_radius, exchange value...");
			float tmp = top_radius;
			top_radius = bottom_radius;
			bottom_radius = tmp;
		}

		// Create a Mesh
		MeshFilter meshFilter = GetComponent<MeshFilter>();
		if (meshFilter == null) {
			Debug.LogError("MeshFilter not found!");
			return;
		}

		Mesh mesh = meshFilter.sharedMesh;
		if (mesh == null) {
			meshFilter.mesh = new Mesh();
			mesh = meshFilter.sharedMesh;
		}
		mesh.Clear();

		// define some value
		Vector3[] vertices;
		int[] triangles;

		if (top_radius == 0.0f) {
			// it's a full circular cone

			// start calculate verticles
			vertices = new Vector3[sections + 2]; // a circle, circle center and an apex

			// circle
			float step = 2 * Mathf.PI / sections;
			float angle = 0;

			for (int i = 0; i < sections; i++) {
				float circle_x = Mathf.Sin(angle) * bottom_radius;
				float circle_z = Mathf.Cos(angle) * bottom_radius;
				angle += step; // increase angle

				vertices[i] = new Vector3(circle_x, 0, circle_z);
			}
			vertices[sections] = new Vector3(0, height, 0); // apex
			vertices[sections + 1] = new Vector3(0, 0, 0); // circle center

			// start calculate triangles
			triangles = new int[sections * 2 * 3];

			int index_triangles = 0;

			for (int i = 1; i < sections; i++) {
				// side faces
				triangles[index_triangles] = sections; // apex
				triangles[index_triangles + 1] = i - 1;
				triangles[index_triangles + 2] = i;
				index_triangles += 3;
				// base face
				triangles[index_triangles] = sections + 1; // circle center
				triangles[index_triangles + 1] = i;
				triangles[index_triangles + 2] = i - 1;
				index_triangles += 3;
			}
			// side faces
			triangles[index_triangles] = sections; // apex
			triangles[index_triangles + 1] = sections - 1;
			triangles[index_triangles + 2] = 0;
			index_triangles += 3;
			// base face
			triangles[index_triangles] = sections + 1; // circle center
			triangles[index_triangles + 1] = 0;
			triangles[index_triangles + 2] = sections - 1;
			//index_triangles += 3;

		} else {
			// it's a truncated circular cone

			// start calculate verticles
			vertices = new Vector3[sections * 2 + 2]; // two circles and two circle center point

			// circle
			float step = 2 * Mathf.PI / sections;
			float top_angle = 0;
			float bottom_angle = step / 2;

			for (int i = 0; i < sections * 2; i += 2) {
				float top_circle_x = Mathf.Sin(top_angle) * top_radius;
				float top_circle_z = Mathf.Cos(top_angle) * top_radius;
				float bottom_circle_x = Mathf.Sin(bottom_angle) * bottom_radius;
				float bottom_circle_z = Mathf.Cos(bottom_angle) * bottom_radius;
				top_angle += step; // increase angle
				bottom_angle += step;

				vertices[i] = new Vector3(top_circle_x, height, top_circle_z);
				vertices[i + 1] = new Vector3(bottom_circle_x, 0, bottom_circle_z);
			}
			vertices[sections * 2] = new Vector3(0, height, 0); // top circle center
			vertices[sections * 2 + 1] = new Vector3(0, 0, 0); // bottom circle center

			// start calculate triangles
			triangles = new int[sections * 4 * 3];

			int index_triangles = 0;

			for (int i = 2; i < sections * 2; i += 2) {
				// side faces
				triangles[index_triangles] = i - 2;
				triangles[index_triangles + 1] = i - 1;
				triangles[index_triangles + 2] = i;
				index_triangles += 3;
				triangles[index_triangles] = i;
				triangles[index_triangles + 1] = i - 1;
				triangles[index_triangles + 2] = i + 1;
				index_triangles += 3;
				// base face
				triangles[index_triangles] = sections * 2; // top circle center
				triangles[index_triangles + 1] = i - 2;
				triangles[index_triangles + 2] = i;
				index_triangles += 3;
				triangles[index_triangles] = sections * 2 + 1; // bottom circle center
				triangles[index_triangles + 1] = i + 1;
				triangles[index_triangles + 2] = i - 1;
				index_triangles += 3;
			}
			// side faces
			triangles[index_triangles] = sections * 2 - 2;
			triangles[index_triangles + 1] = sections * 2 - 1;
			triangles[index_triangles + 2] = 0;
			index_triangles += 3;
			triangles[index_triangles] = 0;
			triangles[index_triangles + 1] = sections * 2 - 1;
			triangles[index_triangles + 2] = 1;
			index_triangles += 3;
			// base face
			triangles[index_triangles] = sections * 2; // top circle center
			triangles[index_triangles + 1] = sections * 2 - 2;
			triangles[index_triangles + 2] = 0;
			index_triangles += 3;
			triangles[index_triangles] = sections * 2 + 1; // bottom circle center
			triangles[index_triangles + 1] = 1;
			triangles[index_triangles + 2] = sections * 2 - 1;
			//index_triangles += 3;
		}

		mesh.vertices = vertices;
		mesh.triangles = triangles;

		mesh.RecalculateNormals();
		mesh.RecalculateBounds();
	}

}