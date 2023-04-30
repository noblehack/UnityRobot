    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class movement : MonoBehaviour
{
    public Rigidbody player;
    public GameObject playerObject;
    public bool isGrounded = false;
    public GameObject dirLight;
    public Camera cam;
    public Color camColor;
    public static bool isSafe = true;
  public float maxSpeed = 10f;
    public AudioSource walk; //ADDED
    public AudioSource walk2;
    public bool justJumped = true;
    public static int health = 4;
    public Slider healthSlider;
      public AudioSource damageSound;
      public Menu menuObject;
      public Slider Stamina;
      public float acceleration = 1500f;
      public GameObject forInterectObject;
      public static int lives = 3;
      public static float staminaAmount;
    // Start is called before the first frame update
    void Start()
    {
        player.freezeRotation = true;
        menuObject = playerObject.GetComponent<Menu>();
        //dirLight.SetActive(false);
        Screen.SetResolution(1280, 720, true);
        camColor = new Color(.1f,.1f,.1f);
        //cam.backgroundColor = camColor;
        //cam.backgroundColor = (new Color(.8f,.8f,.8f));
        cam.backgroundColor = (new Color(.1f,.1f,.1f));
        dirLight.SetActive(false);
         Application.targetFrameRate = 3000;
         QualitySettings.vSyncCount = 1;
         
         
    }

    // Update is called once per frame


    void FixedUpdate(){
    player.velocity = new Vector3(player.velocity.x*.97f,player.velocity.y,player.velocity.z*.97f);
    }


    void Update()
    {
       
        //sprint
        if (Input.GetKey(KeyCode.LeftShift) && Stamina.value > 0){
            maxSpeed = 20f;
            acceleration = 1500f;
            if (!Menu.isAdmin)
            Stamina.value = Stamina.value - (Time.deltaTime*staminaAmount);
        } else{
            maxSpeed = 10f;
            acceleration = 1000f;
            if (player.velocity.magnitude<1) {
            Stamina.value = Stamina.value + (Time.deltaTime*10);
            } else{
                 Stamina.value = Stamina.value + (Time.deltaTime*3);
            }
        }
        //use medkits
        if (Input.GetKeyDown("c")){
            if (Interact.medkitCount > 0){
                Interact.medkitCount--;
                    health =4;
                 healthSlider.value = health;
                 Interact interactobject = forInterectObject.GetComponent<Interact>();
                 interactobject.updateMedkit();
            }
        }


        //controls player walk sounds
       if (player.velocity.magnitude > 1f && isGrounded){
        if (walk.isPlaying == false && walk2.isPlaying == false){
                int randomInt = (Random.Range(0, 2));
                if (randomInt == 1){
                    walk.Play();
                } else{
                    walk2.Play();
                }
           }
       }

       //player.AddForce(new Vector3(0.0f, -9.81f, 0.0f), ForceMode.Acceleration);
        // toggles brightness
        if (Menu.isAdmin){
         if (Input.GetKeyDown("l")){
            dirLight.SetActive(!dirLight.activeSelf);
            if (dirLight.activeSelf){
                cam.backgroundColor = (new Color(.8f,.8f,.8f));
            } else if (dirLight.activeSelf == false){
                cam.backgroundColor = camColor;
            }
         }
        }
         //if sped fast sped now slow
         if (!car.inCar){
          if (player.velocity.magnitude > maxSpeed && isGrounded || player.velocity.magnitude > maxSpeed && justJumped ){
          GetComponent<Rigidbody>().velocity = (GetComponent<Rigidbody>().velocity.normalized * maxSpeed);
          } else{
            if (player.velocity.x > maxSpeed)
            player.velocity = new Vector3(maxSpeed, player.velocity.y, player.velocity.z);
             if (player.velocity.x < -maxSpeed)
            player.velocity = new Vector3(-maxSpeed, player.velocity.y, player.velocity.z);
             if (player.velocity.z > maxSpeed)
            player.velocity = new Vector3(player.velocity.x, player.velocity.y, maxSpeed);
             if (player.velocity.z < -maxSpeed)
            player.velocity = new Vector3(player.velocity.x, player.velocity.y, -maxSpeed);
          }
         

        //playermovement

        if (Input.GetKey("w")){
           player.AddForce(transform.forward*acceleration * Time.deltaTime);
        }
         if (Input.GetKey("s")){
           player.AddForce(transform.forward*-acceleration * Time.deltaTime);
        }
         if (Input.GetKey("a")){
           player.AddForce(transform.right*-acceleration * Time.deltaTime);
        }
         if (Input.GetKey("d")){
           player.AddForce(transform.right*acceleration * Time.deltaTime);
        }
         if (Input.GetKey("space") && isGrounded){
            isGrounded = false;
           player.AddForce(transform.up*200);
            justJumped = true;
            StopCoroutine (jumpCooldown());
            StartCoroutine (jumpCooldown()); 
        }
         }
        //resets player
        if (Input.GetKeyDown("r") ){
        lowerHealth();
        }
        if (Input.GetKeyDown("2")){
            cam.transform.localPosition = new Vector3(1,.86f,-4f);
        }
         if (Input.GetKeyDown("1")){
            cam.transform.localPosition = new Vector3(0,.86f,0);
        }
    }
    //col check
    void OnCollisionEnter(Collision col){
        isGrounded = true;
        
    }
    void OnCollisionStay(Collision col){
         if (col.gameObject.tag == ("SafeZone"))
        isSafe = true;
        if (col.gameObject.tag == ("NoSafeZone"))
        isSafe = false;
    }
     void OnCollisionExit(Collision theCollision)
    {
        if (theCollision.gameObject.name == "Terrain")
            isGrounded = false;
    }

    //checks safe zones
     void OnTriggerEnter(Collider col){
        if (col.gameObject.tag == ("SafeZone"))
        isSafe = true;
        if (col.gameObject.tag == ("NoSafeZone"))
        isSafe = false;
    }
    IEnumerator jumpCooldown(){
         yield return new WaitForSeconds(.5f);
         justJumped = false;
    }
    public void lowerHealth(){
        if (!Menu.isAdmin){
        health -=1;

        damageSound.Play();
        if (health<1){
            lives--;
            health = 4;
            playerObject.transform.position = new Vector3(-820, 22, -240);
            Debug.Log(health);
            isSafe = true;
        }
        if (lives<1){
          menuObject.gameOver();
          lives = 3;
          health = 4;
        }
        healthSlider.value = health;
    }
    }
}
