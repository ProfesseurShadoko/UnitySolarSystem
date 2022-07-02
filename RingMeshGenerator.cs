using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))] // demande à ce qu'il y ait bien un meshfilter
public class RingMeshGenerator : MonoBehaviour
{
    public bool facingDown;
    public float width;
    public int resolution;
    public GameObject tmpRing;
    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    // Start is called before the first frame update
    void Start()
    {
        tmpRing.SetActive(false);
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh=mesh;
        //createTriangle();
        //createCircle(10);
        createRingShape();
        updateMesh();
    }

    private void createTriangle() {
        vertices = new Vector3[]
        {
            new Vector3(0,0,0),
            new Vector3(-1,0,1),
            new Vector3(1,0,0)
        };

        triangles = new int[]
        {
            0,1,2
        };
    }

    private void updateMesh() {
        mesh.Clear();
        mesh.vertices=vertices;
        mesh.triangles=triangles;
        mesh.RecalculateNormals();
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

    private void createRingShape() { //pas la peine de gérer la taille du truc ça on peut le gérer avec le meshGenerator
    //width = 1 ça veut dire que le cercle vide au centre a un rayon deux fois moindre de cercle exterieur
        float bigRadius = 1 + width;
        float teta=Mathf.PI*2/(float)resolution;

        vertices = new Vector3[resolution*2];
        triangles = new int[resolution*2*3*2]; //deux triangles * deux côtés par segment, 3 sommets par triangle

        int zero;
        int one;
        int two;
        int three;

        for (int sector=0; sector<resolution;sector++) { //
            vertices[sector*2]=new Vector3(Mathf.Cos(teta*sector),0,Mathf.Sin(teta*sector));
            vertices[sector*2+1]=vertices[sector*2]*bigRadius;
            
            zero=2*sector;
            one=2*sector+1;
            two=2*((sector+1)%resolution);
            three=2*((sector+1)%resolution)+1;
            
            if (!facingDown) {
            triangles[sector*12]=two;//up
            triangles[sector*12+1]=three;
            triangles[sector*12+2]=one;
            triangles[sector*12+6]=zero;//up
            triangles[sector*12+7]=two;
            triangles[sector*12+8]=one;
            }else{
                triangles[sector*12+9]=zero;//down
                triangles[sector*12+10]=three;
                triangles[sector*12+11]=two;
                triangles[sector*12+3]=zero;//down
                triangles[sector*12+4]=one;
                triangles[sector*12+5]=three; 
            }
        }


    }


}
