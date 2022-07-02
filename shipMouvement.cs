using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shipMouvement : MonoBehaviour
{
    public GameObject ReactorTrust;
    public GameObject Nose;

    public float forwardTrust;
    public float sideTrust;
    public float mass;

    public GameObject UpReactorTrust;
    public GameObject DownReactorTrust;
    public GameObject LeftReactorTrust;
    public GameObject RightReactorTrust;
    public GameObject BackReactorTrustRight;
    public GameObject BackReactorTrustLeft;



    public Vector3 initialPosition;
    private Vector3 angleOrientation = new Vector3(0,0,0);
    private Vector3 currentSpeed=new Vector3(0,0,0);
    public float speed=0;
    private Vector3 trustCorrection=new Vector3(0,0,0);


    // Start is called before the first frame update
    void Start()
    {
        transform.localRotation=Quaternion.Euler(angleOrientation);
        transform.position=initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        //update components
        deactivateTrust();
        trustCorrection=new Vector3(0,0,0);

        if (Input.GetKey("z")) {
            ReactorTrust.SetActive(true);
            trustCorrection=getDirection()*forwardTrust*Time.deltaTime/mass;
            //currentSpeed=new Vector3(1,0,0); //recule
            //currentSpeed=new Vector3(0,1,0); //monte
            //currentSpeed=new Vector3(0,0,1); //droite
            Debug.Log($@"SpaceShip Information :
                - Current Speed : {currentSpeed.magnitude}km/s
                - Current Position : {transform.position}
                - Current Orientation : {transform.localEulerAngles}
                - Current SpeedVector : {currentSpeed}
                _____________________________________________________
                ");
        }

        if (Input.GetKey("space")) {
            Vector3 breakingTrust = currentSpeed.normalized*sideTrust*2*Time.deltaTime/mass;
            if (currentSpeed.magnitude <= breakingTrust.magnitude) {
                currentSpeed=new Vector3(0,0,0);
            }else{
                trustCorrection-=breakingTrust;
            }
        }

        if (Input.GetKey("s")) {
            trustCorrection-=getDirection()*sideTrust*Time.deltaTime/mass;
        }

        if (Input.GetKey("q")) {
            trustCorrection-=getLeftRight()*sideTrust*Time.deltaTime/mass;
        }

        if (Input.GetKey("d")) {
            trustCorrection+=getLeftRight()*sideTrust*Time.deltaTime/mass;
        }

        if (Input.GetKey("a")) {
            trustCorrection+=getDownUp()*sideTrust*Time.deltaTime/mass;
        }

        if (Input.GetKey("e")) {
            trustCorrection-=getDownUp()*sideTrust*Time.deltaTime/mass;
        }
        


        //update trustEffects
        Debug.Log(trustCorrection);
        activateTrust(trustCorrection);
        //update orientation
        angleOrientation+=new Vector3(0,+Input.GetAxis("Mouse X"),-Input.GetAxis("Mouse Y"))*10;
        transform.localRotation=Quaternion.Euler(angleOrientation);

        //update speed
        currentSpeed+=trustCorrection;
        speed=currentSpeed.magnitude/CelestialObject.distanceFactor;
        //update position
        transform.position+=currentSpeed*Time.deltaTime;

        

        

        

    }

    public Vector3 getDirection() {
        return (Nose.transform.position-ReactorTrust.transform.position).normalized;
    }

    public Vector3 getDownUp() {
        return (UpReactorTrust.transform.position-DownReactorTrust.transform.position).normalized;
    }

    public Vector3 getLeftRight() {
        return (RightReactorTrust.transform.position-LeftReactorTrust.transform.position).normalized;
    }

    

    private void deactivateTrust() {
        ReactorTrust.SetActive(false);
        UpReactorTrust.SetActive(false);
        DownReactorTrust.SetActive(false);
        LeftReactorTrust.SetActive(false);
        RightReactorTrust.SetActive(false);
        BackReactorTrustRight.SetActive(false);
        BackReactorTrustLeft.SetActive(false);
    }

    private void activateTrust(Vector3 trustCorrection) {
        float leftOrRight = -Vector3.Dot(trustCorrection,getLeftRight());
        float upOrDown = Vector3.Dot(trustCorrection,getDownUp());
        float forwardOrBackwards = Vector3.Dot(trustCorrection,getDirection());
        float limit = sideTrust*Time.deltaTime/mass/2;
        if (leftOrRight < -limit) {LeftReactorTrust.SetActive(true);}
        if (leftOrRight > limit) {RightReactorTrust.SetActive(true);}
        if (upOrDown < -limit) {UpReactorTrust.SetActive(true);}
        if (upOrDown > limit) {DownReactorTrust.SetActive(true);}
        if (forwardOrBackwards > limit) {ReactorTrust.SetActive(true);}
        if (forwardOrBackwards < -limit) {BackReactorTrustLeft.SetActive(true);BackReactorTrustRight.SetActive(true);}

    }
}
