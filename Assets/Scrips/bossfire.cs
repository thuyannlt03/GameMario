using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossfire : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;
    public float speedUp;

   
    // Start is called before the first frame update
    void Start()
    {

        Rotation();
         rigidbody2D = GetComponent<Rigidbody2D>();
         rigidbody2D.velocity = new Vector2 (speedUp, 0);
         Destroy(gameObject, 2);
         
    }

    // Update is called once per frame
   
     private void OnCollisionEnter2D(Collision2D collision) {
         
     }

     public void SetSpeed(float value){
        speedUp = value;
       
     }

     public void Rotation(){
         Vector2 scale = transform.localScale;
        if(speedUp>0){
           
            scale.x *= scale.x > 0 ? 1: -1;
            
        }
        else{
            
            scale.x *= scale.x > 0 ? -1: 1;
            
        }
        transform.localScale = scale;
     }
     
}
