using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float left, right;
    public float speed;
    public bool isRight;

    public GameObject bossfire;
    
    private float timeSpawn;
    private float time;
    private bool isAlive;
     private int life;
    // Start is called before the first frame update
    void Start()
    {isAlive = true;
        timeSpawn = 1;
        time = timeSpawn;
          life = 3;
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
        GameObject fb = Instantiate (bossfire);
        fb.transform.position = new Vector2(
            transform.position.x + (isRight ? 0.8f : -0.8f),
            transform.position.y
        );
        fb.GetComponent<bossfire>().SetSpeed(
            isRight ? 2: -2
        );
        }
    }
     private void OnCollisionEnter2D(Collision2D collision){
          if(collision.gameObject.CompareTag("FireBall")){
           var direction = collision.GetContact(0).normal;
           //bị đạp
          if(Mathf.Round(direction.x)==-1||Mathf.Round(direction.x)==1){
            //tắt animation
            GetComponent<Animator>().enabled = false;
            //tắt chuyển động
            isAlive = false;
            //nảy lên, rớt xuống
            //biến mất
             if(life >=1){
                    life--;   
                }else{
                    Destroy(gameObject);
                }
           }
           
        }
     }
      
}
