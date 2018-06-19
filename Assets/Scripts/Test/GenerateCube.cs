using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCube : MonoBehaviour {

    MeshFilter mf;

	// Use this for initialization
	void Start () {
        mf = GetComponent<MeshFilter>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BuildCube()
    {
        // This determines the verticies for the
        Vector3[] verticies = new Vector3[8];
        verticies[0] = new Vector3(0, 0, 0);
        verticies[1] = new Vector3(1, 0, 0);
        verticies[2] = new Vector3(1, 1, 0);
        verticies[3] = new Vector3(0, 1, 0);
        verticies[4] = new Vector3(0, 1, 1);
        verticies[5] = new Vector3(1, 1, 1);
        verticies[6] = new Vector3(1, 0, 1);
        verticies[7] = new Vector3(0, 0, 1);

        mf.mesh.vertices = verticies;

        int[] tri = new int[36];

        // Front face

        // Lower left triangle
        tri[0] = 0;
        tri[1] = 2;
        tri[2] = 1;

        // Upper right triangle
        tri[3] = 0;
        tri[4] = 3;
        tri[5] = 2;

        // Top face

        // Lower left triangle
        tri[6] = 2;
        tri[7] = 3;
        tri[8] = 4;

        // Upper right triangle
        tri[9] = 2;
        tri[10] = 4;
        tri[11] = 5;

        // Right face

        // Lower left triangle
        tri[12] = 1;
        tri[13] = 2;
        tri[14] = 5;

        // Upper right triangle
        tri[15] = 1;
        tri[16] = 5;
        tri[17] = 6;

        // Left face

        // Lower left triangle
        tri[18] = 0;
        tri[19] = 7;
        tri[20] = 4;

        // Upper right triangle
        tri[21] = 0;
        tri[22] = 4;
        tri[23] = 3;

        // Back face

        // Lower left triangle
        tri[24] = 5;
        tri[25] = 4;
        tri[26] = 7;

        // Upper right triangle
        tri[27] = 5;
        tri[28] = 7;
        tri[29] = 6;

        // Bottom face

        // Lower left triangle
        tri[30] = 0;
        tri[31] = 6;
        tri[32] = 7;

        // Upper right triangle
        tri[33] = 0;
        tri[34] = 1;
        tri[35] = 6;


        mf.mesh.triangles = tri;

        mf.mesh.RecalculateNormals();

        Vector2[] uv = new Vector2[8];
        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(1, 0);
        uv[2] = new Vector2(0, 1);
        uv[3] = new Vector2(1, 1);
        uv[4] = new Vector2(0, 0);
        uv[5] = new Vector2(1, 0);
        uv[6] = new Vector2(0, 1);
        uv[7] = new Vector2(1, 1);

        mf.mesh.uv = uv;
    }
}
