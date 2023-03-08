using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    public float left, right;
    public GameObject mario;

   
    // Start is called before the first frame update
     void Start() {
       
    }

    // Update is called once per frame
    void Update()
    {
        var marioX = mario.transform.position.x;
        var marioY = mario.transform.position.y;

        var cameraX = transform.position.x;
        var cameraY = transform.position.y;

        if(marioX > left && marioX < right){
            cameraX = marioX;
        }
        else{
            if(cameraX < left) cameraX = left;
            if(cameraX > right) cameraX = right;
        }

        if(marioY > 1){
            cameraY = marioY;
        }
        else{
            cameraY = 1;
        }

        transform.position = new Vector3 (cameraX, cameraY, -10);
    }
    
}
