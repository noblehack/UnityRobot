using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    public Rigidbody player;
    public GameObject elevator;
    public Animator elevatorAnimator;
    public LayerMask mask;
    public bool elevatorAtEnd = false;
     public Camera Camera;
     public GameObject elevatorEndObject;
     public GameObject elevatorStartObject;
    // Start is called before the first frame update
    void Start()
    {
        elevatorAnimator.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
            //gets on elevator and starts animation
        if (Input.GetKeyDown("e")){
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out var hit,30f, mask)){
            if (hit.collider.name == "Elevator" && !elevatorAtEnd){
                elevatorAnimator.enabled = true;
                elevatorEndObject.SetActive(true);
            elevatorAnimator.SetBool("ElevatorUp",true);
        }
        if (hit.collider.name == "Elevator" && elevatorAtEnd){
                elevatorAnimator.enabled = true;
            elevatorAnimator.SetBool("ElevatorDown",true);
             StartCoroutine(spawnStartBlock());
        }
        }
        }



        if (elevatorAnimator.enabled){
                player.transform.position = new Vector3(elevator.transform.position.x, elevator.transform.position.y+.5f,elevator.transform.position.z);
        }
        
    }
    //checks end trigger position for elevator, stops when there
   void OnTriggerEnter(Collider collision){
    if (collision.gameObject.name == "elevatorEnd" && elevatorAnimator.GetCurrentAnimatorStateInfo(0).IsName("ElevatorUp")){
        Debug.Log("end");
        elevatorAnimator.SetBool("ElevatorUp",false);
        elevatorAnimator.enabled = false;
        elevatorAtEnd = true;
        elevatorEndObject.SetActive(false);
    }
    if (collision.gameObject.name == "elevatorStart"){
        elevatorStartObject.SetActive(false);
        elevatorAnimator.SetBool("ElevatorDown", false);
        elevatorAnimator.enabled = false;
        elevatorAtEnd = false;
    }

   }
   //timer for elevator end check
    IEnumerator spawnStartBlock(){
        yield return new WaitForSeconds(2f);
     elevatorStartObject.SetActive(true);
    }
}
