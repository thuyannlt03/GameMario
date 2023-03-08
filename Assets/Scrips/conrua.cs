using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class conrua : MonoBehaviour
{
    public float left, right;
    public float speed;
    private bool isRight;
public float speedUp,height;
 private Vector2 originalPosition;
public Sprite newSprite;
    private bool isAlive;

    public GameObject bua;
   private float timeSpawn;
    private float time;
    
    // Start is called before the first frame update
    void Start()
    {
        isAlive = true;
        timeSpawn = 1;
        time = timeSpawn;
    }

    // Update is called once per frame
    void Update()
    {
          if(!isAlive) return;
          float positionX = transform.position.x;
         if(positionX < left){
            //quay lại phải
            isRight = true;
         }
         else if(positionX > right) {
            //quay lại trái
            isRight = false;
            
         }

         Vector3 vector3;
         if(isRight){

            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? -1 : 1;
            transform.localScale = scale;
            vector3 = new Vector3(1, 0, 0);
         }
         else{
            Vector2 scale = transform.localScale;
            scale.x *= scale.x > 0 ? 1 : -1;
            transform.localScale = scale;
            vector3 = new Vector3(-1, 0, 0);
         }
         transform.Translate(vector3 * speed * Time.deltaTime);
           time -= Time.deltaTime;
        if (time<=0){
            time = timeSpawn;
        GameObject fb = Instantiate (bua);
        fb.transform.position = new Vector2(
            transform.position.x + (isRight ? 1f : -1f),
            transform.position.y
        );
        fb.GetComponent<bua>().SetSpeed(
            isRight ? 3: -3
        );
        }
    
    }
     private void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.gameObject.CompareTag("Player")){
           var direction = collision.GetContact(0).normal;

           //bị đạp
           if(Mathf.Round(direction.y)==-1){
            //đổi hình
            GetComponent<SpriteRenderer>().sprite = newSprite;
            //tắt animation
            GetComponent<Animator>().enabled = false;
            //tắt chuyển động
            isAlive = false;
            //nảy lên, rớt xuống
            originalPosition = transform.position;
            GetComponent<BoxCollider2D>().isTrigger = true;
            StartCoroutine(GoUp());
            //biến mất
            Destroy(gameObject, 2);
           }
           
        }
         
        if(collision.gameObject.CompareTag("FireBall")){
           var direction = collision.GetContact(0).normal;

           //bị đạp
           if(Mathf.Round(direction.x)==-1||Mathf.Round(direction.x)==1){
            //đổi hình
            GetComponent<SpriteRenderer>().sprite = newSprite;
            //tắt animation
            GetComponent<Animator>().enabled = false;
            //tắt chuyển động
            isAlive = false;
            //nảy lên, rớt xuống
            originalPosition = transform.position;
            GetComponent<BoxCollider2D>().isTrigger = true;
            StartCoroutine(GoUp());
            //biến mất
            Destroy(gameObject, 2);
           }
           
        }
    }
            IEnumerator GoUp (){
        while(true){
                transform.position = new Vector2(
                transform.position.x,
                transform.position.y + speedUp * Time.deltaTime);
                if (transform.position.y > originalPosition.y + height) break;
                yield return null;
            
        }
      }  
        
}
