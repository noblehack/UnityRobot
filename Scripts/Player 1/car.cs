using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class car : MonoBehaviour
{
    public LayerMask mask;
    public Camera Camera;
    public static bool inCar = false;
    public Rigidbody player;
    public GameObject playerObject;
    public GameObject seat;
    public GameObject carCollider;
    public Rigidbody carRigid;
    public int acceleration = 10000;
    public AudioSource engineStart;
    public AudioSource engineIdle;
    public AudioSource honk;
    public float steeringSensitivity = 0f;
    public GameObject boundries;
    public GameObject steeringWheel;
     private int rotation = -3;
    private float turnSpeed = 3;
    public TMP_Text speedometer;
    public WheelCollider[] wheels;
    public int power = 100;

    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        //get in car
        if (Input.GetKeyDown("e")){
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out var hit,30f, mask)){
                if (hit.collider.tag == "car" &&inCar == false){
                     playerObject.GetComponent<Collider>().enabled = false;
                    inCar = !inCar;
                    engineStart.Play();
                    carCollider.gameObject.GetComponent<Collider>().enabled =!carCollider.gameObject.GetComponent<Collider>().enabled;
                    if (inCar){
                    player.freezeRotation = true;
                    boundries.SetActive(false);
                }
                }
        }
        }

        if (inCar){
            player.transform.position = seat.transform.position;
            ;
        }
        //get out of car
        if (Input.GetKeyDown("q") &&inCar){
            engineIdle.Stop();
            player.freezeRotation = true;
            inCar = false;
            playerObject.GetComponent<Collider>().enabled = true;
            player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y+5, player.transform.position.z);
            carCollider.gameObject.GetComponent<Collider>().enabled = true;
             player.transform.eulerAngles = new Vector3(0,90,0);

        }
        if (!Input.GetMouseButton(1) && inCar){
            player.transform.eulerAngles = new Vector3(0,carRigid.transform.eulerAngles.y,0);
        }
           
        
        if (Input.GetKeyDown("h") &&inCar){
            honk.Play();
        }
    }
    //car physics and driving
    void FixedUpdate(){
        speedometer.SetText(Mathf.Round(carRigid.velocity.magnitude) + " mph");
            if (inCar){
                steeringSensitivity = carRigid.velocity.magnitude/10;
                if (steeringSensitivity > 1)
                steeringSensitivity=1;
                if (!engineIdle.isPlaying)
                engineIdle.Play();
                 float enginePitch =  (carRigid.velocity.magnitude/3)+1;
                 if (enginePitch > 10)
                enginePitch=10;
                engineIdle.pitch = enginePitch;
                
            if (Input.GetKey("w")){
                wheels[2].motorTorque = power;
                wheels[3].motorTorque = power;
        } if (Input.GetKey("s")){
                wheels[2].motorTorque = -power;
                wheels[3].motorTorque = -power;
        } 
        if (Input.GetKey("a")){
             if (carRigid.velocity.magnitude < 5){
            wheels[0].steerAngle =-50;
           wheels[1].steerAngle =-50f;  
             } else {
                wheels[0].steerAngle =-25;
           wheels[1].steerAngle =-25f; 
             }  
          if (rotation <-15)
             rotation++;
        }
        if (Input.GetKey("d")){
             if (carRigid.velocity.magnitude < 5){
            wheels[0].steerAngle =50;
           wheels[1].steerAngle =50f;  
             } else {
                wheels[0].steerAngle =25;
           wheels[1].steerAngle =25f; 
             }
             if (rotation > -45)
             rotation--;
            
        }
        if (!Input.GetKey("w") && !Input.GetKey("s")){
                wheels[2].motorTorque = 0;
                wheels[3].motorTorque = 0;
        }   
        if (!Input.GetKey("a") && !Input.GetKey("d")){

             wheels[0].steerAngle = 0;
           wheels[1].steerAngle = 0f;
        if (rotation > -30)
        rotation--;
        if (rotation < -30)
        rotation++;
        }
     
        steeringWheel.transform.localEulerAngles = new Vector3(rotation*turnSpeed,180,0);
        }
        Debug.Log(steeringWheel.transform.localEulerAngles.x);
        }
    }
  
