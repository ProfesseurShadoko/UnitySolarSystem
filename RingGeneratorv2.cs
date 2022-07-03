using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingGeneratorv2 : MonoBehaviour
{

    [Range(0.1f,10f)]
    public float width=1;
    [Range(2,256)] //un mesh ne peut pas avoir plus de 256**2 sommets
    public int resolution=10;
    public Material material;

    [SerializeField, HideInInspector]
    private GameObject meshObjectUp;

    [SerializeField, HideInInspector]
    private GameObject meshObjectDown;


    private void Initialize() {
        if (meshObjectUp==null || meshObjectDown == null) {
            meshObjectUp = new GameObject("meshUp");
            meshObjectUp.transform.parent = transform;
            meshObjectUp.AddComponent<MeshRenderer>();
            meshObjectUp.GetComponent<MeshRenderer>().sharedMaterial=material;
            meshObjectUp.AddComponent<MeshFilter>();
            meshObjectUp.GetComponent<MeshFilter>().sharedMesh=new Mesh();
            

            meshObjectDown = new GameObject("meshDown");
            meshObjectDown.transform.position-=new Vector3(0,0,0.01f);
            meshObjectDown.transform.parent = transform;
            meshObjectDown.AddComponent<MeshRenderer>();
            meshObjectDown.GetComponent<MeshRenderer>().sharedMaterial=material;
            meshObjectDown.AddComponent<MeshFilter>();
            meshObjectDown.GetComponent<MeshFilter>().sharedMesh = new Mesh();
            
        }
        createRingShape("up",meshObjectUp.GetComponent<MeshFilter>().sharedMesh);
        createRingShape("down",meshObjectDown.GetComponent<MeshFilter>().sharedMesh);
    }

    private void OnValidate() {
        Initialize();
    }
    

    private void createRingShape(string upOrDown, Mesh mesh) { //pas la peine de gérer la taille du truc ça on peut le gérer avec le meshGenerator
    //width = 1 ça veut dire que le cercle vide au centre a un rayon deux fois moindre de cercle exterieur
        float bigRadius = 1 + width;
        float teta=Mathf.PI*2/(float)resolution;
        bool facingUp = (upOrDown.Equals("up"));


        Vector3[] vertices;
        int[] triangles;

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
            
            if (facingUp) {
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
        
        mesh.Clear();
        mesh.vertices=vertices; //il faut mettre les sommets avant les triangles !!!
        mesh.triangles=triangles;
        mesh.RecalculateNormals();
    }

}
