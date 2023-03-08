using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall2 : MonoBehaviour
{
   private new Rigidbody2D rigidbody2D;
   public float speedUp;
    // Start is called before the first frame update
    void Start()
    {
         rigidbody2D = GetComponent<Rigidbody2D>();
         rigidbody2D.velocity = new Vector2 (speedUp, 0);
         Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     private void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.gameObject.CompareTag("SanGach")){
            speedUp = speedUp* 0.99f;
            rigidbody2D.velocity = new Vector2 (speedUp, Mathf.Abs (speedUp));
        }
     }

     public void SetSpeed(float value){
        speedUp = value;
     }
}
