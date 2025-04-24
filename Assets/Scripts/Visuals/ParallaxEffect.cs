using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxEffect : MonoBehaviour
{
    //Parallax Effect -Beim Scrollen bewegen sich verschiedene Elemente in unterschiedlicher Geschwindigkeit
    // Start is called before the first frame update

    public Camera cam;
    public Transform followTarget; //Trabsform = Position, Rotation usw eines GameObjects

    //Startposition des GameObjects
    Vector2 startingPosition;
    float startingZ; //Wird sperat genommen. Ist quasi der Abstand zu den hinteren Objekten. Der soll bei uns ja gleich bleiben

    //==>sorgt dafür das es für jedem Frame neu ausgeführt wird. Ähnlich wie die Update methode
    Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPosition; //Zieht den Startpunkz ab um festzulegen, wieviel sich die Kamera seit Start bewegt hat.
    float ZdistanceFromTarget => transform.position.z - followTarget.position.z;

    //Nächste Zeile ist eine tenäre Bedingung
    float clippingPlane => (cam.transform.position.z +(ZdistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));
    float ParallaxFactor => Mathf.Abs(ZdistanceFromTarget) / clippingPlane;
    void Start()
    {
        startingPosition = transform.position;
        startingZ = transform.position.z;
    }

    // Update is called once per frame
    void Update()
    {
       
        Vector2 newPosition = startingPosition + camMoveSinceStart * ParallaxFactor;
        transform.position = new Vector3 (newPosition.x, newPosition.y,startingZ);
    }
}
