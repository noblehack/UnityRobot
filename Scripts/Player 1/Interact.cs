using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Interact : MonoBehaviour
{
       public GameObject houseDoor;
       public LayerMask mask;
       public bool isClosed = true;
      public Canvas passwordCanvas; 
      public AudioSource keyPress;
      public AudioSource keyError;
      public AudioSource keySuccess;
      public int password = 0;
      private int correctPassword = 29;
      public GameObject finalDoor;
      public AudioSource finalDoorSound;
      public static int medkitCount = 0;
      public GameObject medkitUI;
      public TMP_Text medkitUICount;
     
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {  
        if (Input.GetKeyDown("e")){
        if(Physics.Raycast(transform.position, transform.forward, out var hit,30f, mask)){
   //bunker door
               if (int.TryParse(hit.collider.name, out int number)){
                keyPress.Play();
               Debug.Log(number);
               password += number;
               Debug.Log(password);
               }
               if (hit.collider.name == "enter"){
                checkPassword();
               }
               //medkit

               if (hit.collider.name == ("medkit")){
                    hit.collider.gameObject.SetActive(false);
                    medkitCount++;
                    medkitUICount.SetText ("x"+medkitCount);
               }


  //normal doors
               if (hit.collider.tag == ("door")){
               Debug.Log( hit.collider.gameObject.name);
            Animator doorAnimator = hit.collider.gameObject.GetComponent<Animator>();
            AudioSource doorAudio = hit.collider.gameObject.GetComponent<AudioSource>();
            doorAudio.Play();
              Debug.Log( doorAnimator);
             if (doorAnimator.GetBool("OpenDoor")){
                    doorAnimator.SetBool("OpenDoor", false);
             } else {
                doorAnimator.SetBool("OpenDoor", true);
             }
            }

            //house door
            if (hit.collider.name == "HouseDoor"){
                  AudioSource doorAudio = hit.collider.gameObject.GetComponent<AudioSource>();
            doorAudio.Play();
            StartCoroutine(openHouseDoor());
            }
          
        }
    }
    }
    //checks bunkers passcode
    public void checkPassword(){
     if (password == correctPassword){
        Debug.Log("correct");
        keySuccess.Play();
        password = 0;
        finalDoorSound.Play();
        StartCoroutine(openDoor());
     } else {
        password = 0;
        keyError.Play();
     }
    }
    //bunker door animation
    IEnumerator openDoor(){
        for (int i = 0; i<30; i++){
            finalDoor.transform.eulerAngles = new Vector3(0f,-i*3f,-90f);
            finalDoor.transform.position = new Vector3(finalDoor.transform.position.x,finalDoor.transform.position.y, finalDoor.transform.position.z-.005f*i);
            yield return new WaitForSeconds(.1f);
        }
    }
    //house door animation
        IEnumerator openHouseDoor(){
        if (isClosed){
            isClosed = false;
        for (int i = 0; i<30; i++){
            houseDoor.transform.eulerAngles = new Vector3(90f,-i*3.1f,180f);
            houseDoor.transform.position = new Vector3(houseDoor.transform.position.x+.0025f*i,houseDoor.transform.position.y, houseDoor.transform.position.z+.002f*i);
            yield return new WaitForSeconds(.05f);
        }
        
    }
       }
       //updates medkit counter
       public  void updateMedkit(){
        medkitUICount.SetText ("x"+medkitCount);
       }
}
