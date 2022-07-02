using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))] // demande à ce qu'il y ait bien un meshfilter
public class PlanetMeshGenerator : MonoBehaviour
{
    public int resolution;
    private int vNumber;
    private int tNumber;
    public GameObject tmpSphere;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        tmpSphere.SetActive(false);
        mesh = new Mesh();
        //vNumber=
        //tNumber=

        GetComponent<MeshFilter>().mesh=mesh;
        
        createCube();
        inflateCube();
        updateMesh();
    }

    private void updateMesh() {
        mesh.Clear();
        mesh.vertices=vertices;
        mesh.triangles=triangles;
        mesh.RecalculateNormals();
    }

    private void inflateCube() {
        for (int v=0; v<vNumber; v++) {
            vertices[v]=vertices[v].normalized;
        }
    }

    private void createCube() {

    }

    private void createCircle(int resolution) {
        vertices = new Vector3[resolution+1];
        triangles = new int[resolution*3];
        float teta;

        vertices[0]=new Vector3(0,0,0);
        for (int v=0; v<resolution; v++) {
            teta=v*(Mathf.PI*2/(float)resolution);
            vertices[v+1]=new Vector3(Mathf.Cos(teta),0,Mathf.Sin(teta));
        }
        for (int t=0; t<resolution; t++) {
            triangles[3*t]=t+1;
            triangles[3*t+1]=0;
            if (t==resolution-1){
                triangles[3*t+2]=1;
                return;
            }
            triangles[3*t+2]=(t+2);
        }
    }


}

