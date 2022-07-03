using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CelestialObject : MonoBehaviour
{
    public static PythonList objects = new PythonList();
    public static float distanceFactor=1/500000f; //transform km in gameSize
    public static float radiusFactor=1/2000f; //transform km in gameSize

    public bool positionFixed;
    public Rigidbody rb;

    private float mass;
    public float density; //on reste en kg/m**3 donc en mg/m**3
    public float radius;
    public float semiMajorAxis;
    private float avgSpeed;
    private Vector3 speed; 

    // Start is called before the first frame update
    void Start()
    {
        if (this.name.Equals("Sun")) {density=density*1000;}//on a corrigé la taille du soleil en la divisant par 10 mais on veut que la masse reste la même
        float sunMass = 4f*1408000f*70000f*70000f*70000f/3f*Mathf.PI;
        if (!(semiMajorAxis==0)){avgSpeed=Mathf.Sqrt((float)(sunMass*Gravitation.G/semiMajorAxis));}else{avgSpeed=0;}
        speed=new Vector3(0,0,1)*avgSpeed;

        transform.localScale = new Vector3(1,1,1)*radius*radiusFactor;
        transform.position = new Vector3(1,0,0)*distanceFactor*semiMajorAxis;
        this.mass=radius*radius*radius*Mathf.PI*density*4/3;
        objects.append(this);

        if (positionFixed) {
            rb.constraints=RigidbodyConstraints.FreezePosition;
        }

        GetComponent<Rigidbody>().mass=mass;
    }

    // Update is called once per frame
    void Update()
    {
        updateSpeed();
        updatePosition();

        //Debug.Log(CelestialObject.all().ToString());
        //Debug.Log($"Current speed : {this.speed}");
        
    }

    public float getMass() {return this.mass;}

    public Vector3 getPosition() {return transform.position;}

    public Vector3 getSpeed() {return this.speed;}

    public Vector3 getDirection() {return this.speed.normalized;}
    
    private void updatePosition() {if (!positionFixed) {transform.position+=this.speed*Time.deltaTime*distanceFactor;}} //vitesse calculée pour les vraie distances
    //private void updatePosition() {return;}
    private void updateSpeed() {this.speed=this.speed+Gravitation.externalForce(this)/this.mass*Time.deltaTime;} //Force calculée avec les mauvaises distances

    public static PythonList all() {
        return CelestialObject.objects;
    }

    public override string ToString() {
        return $"<{this.name} (x={transform.position.x},y={transform.position.y},z={transform.position.z}) at speed ({speed.magnitude})>\n";
    }

    public static CelestialObject findPlanet(string name) {
        CelestialObject planet;
        for (int i=0; i<CelestialObject.all().length(); i++) {
            planet=CelestialObject.all().element(i);
            if (planet.name.Equals(name)) {return planet;}
        }
        return CelestialObject.all().element(0);
    }


}
