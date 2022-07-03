using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class CubeFace {

    public Mesh mesh;
    private int resolution;
    private Vector3 localUp; //vers où la face pointe
    private Vector3 localX; //comment elle est tournée
    private Vector3 localY; 

    public CubeFace(int res, Vector3 up,Mesh emptyMesh) { //axis is in ["x","-x","y","-y","z","-z"]
        
        localUp = up;
        localX=new Vector3(localUp.y,localUp.z,localUp.x);
        localY=Vector3.Cross(localUp,localX);
        resolution = res;
        mesh = emptyMesh;
        }
    
    public void CreateMesh() {
        int [] triangles = new int[(resolution-1)*(resolution-1)*2*3];
        Vector3[] vertices = new Vector3[resolution*resolution];
        Vector3 xStep = localX/(resolution-1);
        Vector3 yStep = localY/(resolution-1);
        Vector3 currentPoint = -(localX/2+localY/2)+localUp/2; //on construit la face autour de l'origine

        //creating vertices
        for (int x=0; x<resolution; x++) {
            for (int y=0; y<resolution; y++) {
                vertices[x*resolution+y]=new Vector3 (currentPoint.x, currentPoint.y, currentPoint.z);
                currentPoint=currentPoint+yStep;
            }
            currentPoint=currentPoint+xStep;
        }
        Debug.Log(vertices.Length+1);

        //creating triangles
        int t;
        int top;
        int left;
        int topLeft;
        for (int x=0; x<resolution-1; x++) {
            for (int y=0; y<resolution-1; y++) { //on itère sur les carrés, le carré 0,0 c'est celui tout en bas à droite
                t=x*(resolution-1)+y;
                top=t+1;
                left=t+resolution;
                topLeft=t+1+resolution;

                triangles[6*t]=t;
                triangles[6*t+1]=top;
                triangles[6*t+2]=left;

                triangles[6*t+3]=topLeft;
                triangles[6*t+4]=left;
                triangles[6*t+5]=top;
            }
        }
        mesh.Clear();
        mesh.triangles=triangles;
        mesh.vertices=vertices;
        mesh.RecalculateNormals();       
    }
}


//[RequireComponent(typeof(MeshFilter))] // demande à ce qu'il y ait bien un meshfilter
public class PlanetMeshGenerator : MonoBehaviour
{
    [SerializeField, HideInInspector] //serialize permet d'enregistrer, HideInInspector êrmet de pas permettre d'y toucher depuis l'éditeur
    MeshFilter[] meshFilters;
    CubeFace[] cubeFaces= new CubeFace[6];

    [Range(2,256)] //un mesh ne peut pas avoir plus de 256**2 sommets
    public int resolution=10;

    private void Initialize() {
        
        Vector3[] axis = new Vector3[]{
            Vector3.up,Vector3.down,Vector3.left,Vector3.right,Vector3.forward,Vector3.back
        };

        //initilaiser les mesh (seulement si on l'a pas déjà fait avant)
        if (meshFilters==null || meshFilters.Length==0) {
            meshFilters= new MeshFilter[6];
        }
        
            for (int i=0; i<6; i++) {
                if(meshFilters[i]==null) {
                    GameObject meshObject = new GameObject("mesh");
                    meshObject.transform.parent = transform; //je lui dis qu'il est associé à moi !
                    meshObject.AddComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
                    meshFilters[i] = meshObject.AddComponent<MeshFilter>();
                    meshFilters[i].sharedMesh = new Mesh();
                }      
            }

        for (int i=0; i<6; i++) {
            cubeFaces[i]= new CubeFace(resolution,axis[i],meshFilters[i].sharedMesh);
        }
    }

    private void generateMesh() {
        foreach (CubeFace face in cubeFaces) {
            face.CreateMesh();
        }
    }

    private void OnValidate() { //à chaque fois qu'on change un truc dans l'éditeur ça va changer
        Initialize(); //même pas besoin de mettre this. !
        generateMesh();
    }

    

}*/


