using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Vector2 originalPosition;
    public Sprite newBlock;
    public float speed, height;
    private bool canChange;
    public GameObject item1, item2, item3;

    // Start is called before the first frame update
    void Start()
    {
     originalPosition = transform.position; 
     canChange = true;  
    }
    private void OnCollisionEnter2D(Collision2D collision){
        if(!canChange) return;
        if(collision.gameObject.CompareTag("Player")){
            canChange = false;
            //chuyển sang hình khác
            GetComponent<SpriteRenderer>().sprite = newBlock;
            GetComponent<Animator>().enabled = false;
            //nảy lên rớt xuống
            StartCoroutine(GoUpAndDown());
            //tạo vật phẩm
            GameObject newItem;
            int random = Random.Range(0, 2);
            if(random==0){
                newItem =  Instantiate<GameObject>(item1);
            }
            else if(random==1){
                newItem =  Instantiate<GameObject>(item2);
            }
            else{
                newItem =  Instantiate<GameObject>(item3);
            }
          
            newItem.transform.position = originalPosition;
             StartCoroutine(ItemGoUp(newItem));

        }
    }
        //tạo vật phẩm
      IEnumerator ItemGoUp (GameObject newItem){
        while(true){
                newItem.transform.position = new Vector2(
                newItem.transform.position.x,
                newItem.transform.position.y + speed * Time.deltaTime);
                if (newItem.transform.position.y > originalPosition.y + 0.5) break;
                yield return null;
            
        }
      }

      IEnumerator GoUpAndDown(){
        while(true){

            transform.position = new Vector2(
                transform.position.x,
                transform.position.y + speed * Time.deltaTime);
                if (transform.position.y > originalPosition.y * height) break;
                yield return null;
            
        }
        while(true) {
            transform.position = new Vector2(
                transform.position.x,
                transform.position.y - speed * Time.deltaTime);
            if(transform.position.y < originalPosition.y){
                transform.position = originalPosition;
                break;
            }
            yield return null;
            
        }
    }
}
