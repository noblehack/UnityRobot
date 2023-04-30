using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Workbench : MonoBehaviour
{
    public LayerMask mask;
    public Camera Camera;
    public GameObject car;
     public GameObject seat;
     public CarPartsCollection CarPartsCollectionScript;
     public GameObject CarPartsCollectionObject;
    // Start is called before the first frame update
    void Start()
    {
        CarPartsCollectionScript = CarPartsCollectionObject.GetComponent<CarPartsCollection>();
    }

    // Update is called once per frame
    void Update()
    {
        //checks if user has all parst, will spawn car if all are collected or user is an admin
         if (Input.GetKeyDown("e")){
        if(Physics.Raycast(Camera.transform.position, Camera.transform.forward, out var hit,30f, mask)){
            if (hit.collider.name=="WorkBench"){
                if(CarPartsCollection.allPartsCollected)
                car.SetActive(true);
                for (int i = 0; i <CarPartsCollectionScript.carPartsList.Length;i++){
                    if (CarPartsCollectionScript.carPartsList[i]!=null)
                    CarPartsCollectionScript.carPartsList[i].SetActive(false);
                }
            }
        }       
         }
    }
}
