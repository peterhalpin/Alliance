using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boxes : MonoBehaviour
{
    private List<Collider2D> colliders = new List<Collider2D>();
    public BoxCollider2D[] greenBoxes;
    public GameObject green;
    public AreaEffector2D a;

    void Start(){
        green = GameObject.FindWithTag("VisionPlayer");
        greenBoxes = green.GetComponents<BoxCollider2D>();
        a = green.GetComponent<AreaEffector2D>();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col == greenBoxes[0]){
            a.forceAngle = 0;
        }
        if(col == greenBoxes[1]){
            a.forceAngle = 180;
        }
        if(col == greenBoxes[2]){
            a.forceAngle = 270;
        }
        if(col == greenBoxes[3]){
            a.forceAngle = 90;
        }
    }
}
