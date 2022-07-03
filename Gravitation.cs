using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitation {

    public static float G=1f;
    public static float planetShipFactor=40f; //allows the ship to land on the planet by increasing gravity

    public static Vector3 externalForce(CelestialObject planet) {
        Vector3 totalForce = new Vector3(0,0,0);
        Vector3 force;
        Vector3 distance;
        CelestialObject other;

        for (int i=0; i<CelestialObject.all().length(); i++) {
            other=CelestialObject.all().element(i);
            distance=(other.transform.position-planet.transform.position)/CelestialObject.distanceFactor;
            if (!(Vector3.Dot(distance,distance)==0)) {
                force=G*planet.getMass()*other.getMass()/Vector3.Dot(distance,distance)*distance.normalized;
                totalForce=totalForce+force;
            }
        }
        if (planet.name.Equals("Earth")) {
            //Debug.Log($"Une acceleration {totalForce.magnitude/planet.getMass()} s'exerce sur {planet} qui pese {planet.getMass()}");
        }
        //Debug.Log(distance);
        //Debug.Log($"Mass = {planet.getMass()}");
        return totalForce;

    }

    public static Vector3 externalForce(shipMouvement spaceShip) {
        Vector3 totalForce = new Vector3(0,0,0);
        Vector3 force;
        Vector3 distance;
        CelestialObject other;
        //CelestialObject jupiter = CelestialObject.findPlanet("Jupiter"); //on aligne toutes les forces de gravité sur Jupiter

        for (int i=0; i<CelestialObject.all().length(); i++) {
            other=CelestialObject.all().element(i);
            distance=(other.transform.position-spaceShip.transform.position)/CelestialObject.distanceFactor;
            force=G*spaceShip.getMass()*other.getMass()/Vector3.Dot(distance,distance)*distance.normalized;
            if (!other.name.Equals("Sun")) {force=force*planetShipFactor;}
            totalForce=totalForce+force;
        }
        //Debug.Log($"Une acceleration {totalForce.magnitude/spaceShip.getMass()} s'exerce sur {spaceShip} qui pese {spaceShip.getMass()}");
        //Debug.Log($"v={spaceShip.speed}");
        return totalForce;
    }
}