using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hoa : MonoBehaviour
{
    private Vector2 originalPosition;
    public float speed, height;
    public new Rigidbody2D rigidbody2D;
    public bool stop;
    
   
    // Start is called before the first frame update
    void Start()
    {
        
        originalPosition = transform.position; 

        StartCoroutine(GoUp());
        
    }

    // Update is called once per frame
    void Update()
    {
         
    }
     //nảy lên
      
      IEnumerator GoUp (){
        while(true){
                transform.position = new Vector2(
                transform.position.x,
                transform.position.y + speed * Time.deltaTime);
                if (transform.position.y > originalPosition.y + height) break;
                yield return null;
        }
        StartCoroutine(GoDown());
      }
    //xuống
      IEnumerator GoDown(){
         bool stop = false;
            while(!stop){
                stop = true;
                 yield return new WaitForSeconds(2);
            }
        while(true){
                transform.position = new Vector2(
                transform.position.x,
                transform.position.y - speed * Time.deltaTime);
                if (transform.position.y < originalPosition.y) break;
                yield return null;
            
        }
       StartCoroutine(GoUp());
    }
    private void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.CompareTag("Player")){
           var direction = collision.GetContact(0).normal;

           //bị đạp
           if(Mathf.Round(direction.y)==-1){
             
              Destroy(gameObject);
            
           }
        }
         
          
            
           }
      
    

}
