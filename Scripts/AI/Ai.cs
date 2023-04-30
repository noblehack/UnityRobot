using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ai : MonoBehaviour
{
    
     public Rigidbody PlayerObject;
     public GameObject pObject;
    public Transform Player;
   public UnityEngine.AI.NavMeshAgent agent;
   public Transform theAI;
   public Transform theAIHead;
   public static bool autoMove = true;
   public static Vector3 moveTo;
   public GameObject home;
   public bool goingHome = false;
   public AudioSource growl;
   public movement movementObject;
 
   
    // Start is called before the first frame update
    void Start()
    {
        //ai movement location updates every .25 seconds
         InvokeRepeating("aiMove", .25f, .25f);  //1s delay, repeat every 1s
        movementObject = pObject.GetComponent<movement>();
       agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
      InvokeRepeating("growlCooldown", .2f, 1f);   
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }
//growl cooldown
  void growlCooldown(){
        float random = Random.Range(0f,10f);
       if (random < 2)
        growl.Play();
       
  }
//checks if ai collides with player
    void OnTriggerEnter(Collider col){
        
        if (col.name == "Player"){
            PlayerObject.velocity = new Vector3(PlayerObject.velocity.x,PlayerObject.velocity.y+20f,PlayerObject.velocity.z);
           movementObject.lowerHealth();
        }
    }
    void aiMove(){
 //growl
       
        //check if should be chasing player
        if (autoMove && (PlayerObject.velocity.x !=0f || PlayerObject.velocity.z != 0f) )
            agent.destination = Player.position;
        agent.speed = Menu.aiSpeed;
        //rotation of head & body
        Vector3 rot = Quaternion.LookRotation(Player.position - theAI.position).eulerAngles;
        rot.x = rot.z = 0;
        theAIHead.rotation = Quaternion.Euler(rot);
        if (agent.velocity.x != 0f || agent.velocity.x != 0f)
         theAI.rotation = Quaternion.Euler(rot);



        //checks if player is safe and if ai should move
         if (movement.isSafe == false && goingHome == true){
            goingHome = false;
            autoMove = true;
         } 
         if (movement.isSafe == true && goingHome == false){
            goingHome = true;
            autoMove = false;
            moveTo = home.transform.position;
            agent.destination = moveTo;
            Debug.Log(home.transform.position);
         }
             //Debug.Log(autoMove);
    }
     
}
