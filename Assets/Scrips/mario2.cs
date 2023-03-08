using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class mario2 : MonoBehaviour
{
    private Vector2 originalPosition;
    public float speed;
    private float  height;
    public new Rigidbody2D rigidbody2D;
    private bool isRight;

    private Animator animator;
    public float IsRunning;
    public bool IsJump;

    //nhạc
    private AudioSource audioSource;
    public AudioClip coinClip;

    //điểm coin, thời gian
    private int coin;
    public Text coinText;

    //điểm thời gian chơi

    private int time;     //thời gian
    public Text timeText; //hiển thị thời gian
    private bool isAlive; //kiểm tra nv còn sống

    //viên đạn
    public GameObject FireBall;
    //life
    private int life;
    public Text lifeText;
    // Start is called before the first frame update
    void Start()
    {
         rigidbody2D = GetComponent<Rigidbody2D>();
        isRight = true;

        animator = GetComponent<Animator>();
        IsRunning =0;
        IsJump = false;

        audioSource = GetComponent<AudioSource>();

        coin = 0;

        isAlive = true;
        time = 0;
        timeText.text = time + " s ";
        StartCoroutine(Updatetime());

        PlaySound("Sounds/yarblat_smb_techno");

          life = 3;
        lifeText.text = life + " ";
         originalPosition = transform.localPosition;
    }
     IEnumerator Updatetime(){
        while (isAlive)
        {
            time++;
            timeText.text = time + " s ";
            yield return new WaitForSeconds(1);
        }
    }


  void Update()
    {

        animator.SetFloat("IsRunning", IsRunning);
        animator.SetBool("IsJump", IsJump);
        //IsRunning = 0;

        //bắt đạn
        if(Input.GetKeyDown(KeyCode.D)){
            //tạo đạn
            GameObject fire = Instantiate(FireBall);
            fire.transform.position = new Vector3(
                transform.position.x + (isRight ? 0.7f : - 0.7f),
                transform.position.y,
                transform.position.z
            );
            
            fire.GetComponent<FireBall>().SetSpeed(isRight ? 10 : -10);
        }
        Vector2 velocity = new Vector2();
        if(Input.GetKeyDown(KeyCode.RightArrow)){
            if(isRight==false){
                Vector2 scale = transform.localScale;
                scale.x *= scale.x < 0 ? -1 : 1;
                transform.localScale = scale;
                isRight = true;
            }

            rigidbody2D.velocity = new Vector2 (speed, 0);

            
        }
        else if(Input.GetKeyDown(KeyCode.LeftArrow)){
            if(isRight==true){
                Vector2 scale = transform.localScale;
                scale.x *= scale.x > 0 ? -1 : 1;
                transform.localScale = scale;
                isRight = false;
            }

            rigidbody2D.velocity = new Vector2 (-speed, 0);
            
        }
        else if(Input.GetKeyDown(KeyCode.Space)) {
            rigidbody2D.AddForce(new Vector2(0,300));
            IsJump = true;
        }
        
        IsRunning = Mathf.Abs(rigidbody2D.velocity.x);
    }

    //2 box collider va chạm vào nhau
    private void OnCollisionEnter2D(Collision2D collision) {
        var name = collision.gameObject.tag;
        if(name.Equals("SanGach")){
            IsJump = false;
        }
        if(name.Equals("BossFrie")){
            if(life >=1){
                    life--;
                     lifeText.text = life + " ";
                      transform.localPosition = originalPosition;
                      
                }else{
                    //Game over
                }
        }
         if(name.Equals("bua")){
            if(life >=1){
                    life--;
                     lifeText.text = life + " ";
                      transform.localPosition = originalPosition;
                     
                }else{
                    //Game over
                }
        }
         
       
        else if(name.Equals("Hoa")){
            Vector2 direction = collision.GetContact(0).normal;
            float directionX = direction.x;
            float directionY = direction.y;
            if(directionX>0){
                if(life >=1){
                    life--;
                    lifeText.text = life + " ";
                     transform.localPosition = originalPosition;
                }else{
                    //Game over
                }
                 
            }
            else if(directionX<0) {
               if(life >=1){
                    life--;
                     lifeText.text = life + " ";
                      transform.localPosition = originalPosition;
                }else{
                    //Game over
                }
                
            }
             else if(directionY>0) {
                //Destroy(collision.gameObject);
               
            }
            else{
               Destroy(gameObject);
                if(life >=1){
                    life--;
                     lifeText.text = life + " ";
                      transform.localPosition = originalPosition;
                }else{
                    //Game over
                }
                
            }
        }
    }
     private void OnTriggerEnter2D(Collider2D collision) {
        var name = collision.gameObject.tag;
        if(name.Equals("DongXu")){
            
            //phát nhạc
            //PlaySound("Sounds/smb3_coin");
            PlayClip(coinClip);
            //biến mất
            Destroy(collision.gameObject);
            //tăng điểm
            coin++;
            coinText.text = coin + " x ";
        }
    }

    //phát nhạc
    private void PlaySound(string name){
        audioSource.PlayOneShot(Resources.Load<AudioClip>(name));
    }

    //phát clip
     private void PlayClip(AudioClip clip){
        audioSource.PlayOneShot(clip);
    }

    
}


