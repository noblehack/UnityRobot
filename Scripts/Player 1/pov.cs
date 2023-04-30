using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pov : MonoBehaviour
{
    //script used for testing


    
    public LayerMask mask; 
    //moveTo from ai script


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //sets ai to move to looking point
        // if (Input.GetKeyDown(KeyCode.Mouse0)){
        //     Ai.autoMove = false;
        //     if(Physics.Raycast(transform.position, transform.forward, out var hit, Mathf.Infinity, mask)){
        //        Ai.moveTo  =new Vector3( hit.point.x,hit.point.y, hit.point.z);
        // }
        // }


        //sets auto move to player when right clicked
        // if (Input.GetKeyDown(KeyCode.Mouse1)){
        //     Ai.autoMove = true;
        // }
    
    }
}
