using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batteries : MonoBehaviour
{
    public GameObject charger;
   public GameObject player;
     flashlight flashlightObject; 
   

    // Start is called before the first frame update
    void Start()
    {
         flashlightObject = player.GetComponent<flashlight>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //checks if should be charging
     void OnTriggerStay(Collider col){
        if (col.tag == ("charger")){
            Debug.Log("charging");
            flashlightObject.charge();
        }
    }
}
//