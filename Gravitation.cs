using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravitation {

    public static float G=10f;

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
        if (planet.name.Equals("Earth")) {Debug.Log($"Une force {totalForce/planet.getMass()} s'exerce sur {planet} qui pese {planet.getMass()}");}
        //Debug.Log(distance);
        //Debug.Log($"Mass = {planet.getMass()}");
        return totalForce;

    }
}