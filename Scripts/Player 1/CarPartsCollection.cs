using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarPartsCollection : MonoBehaviour
{
    public LayerMask mask; 
    public GameObject wheel;
    public GameObject wheelUiImage;
    public GameObject scrap;
    public GameObject scrapUiImage;
        public GameObject Engine;
    public GameObject engineUiImage;
        public GameObject Gas;
    public GameObject gasUiImage;
    public GameObject Seat;
    public GameObject SeatUI;
    public GameObject SteeringWheel;
    public GameObject SteeringWheelUI;
    public static bool hasSteeringWheel;
    public static bool hasSeat;
    public static bool hasWheel;
    public static bool hasScrap;
    public static bool hasEngine;
    public static bool hasGas;
    public static bool allPartsCollected = false;
    public GameObject[] carPartsList;
    // Start is called before the first frame update
    void Start()
    {
        carPartsList  = new GameObject[6];
    }
//
    // Update is called once per frame
    void Update()
    {
        //logic to pick up items
        if (Input.GetKeyDown("e")){
        if(Physics.Raycast(transform.position, transform.forward, out var hit,5f, mask)){
                   if (hit.collider.tag == ("wheel")){
                    wheel.SetActive(false);
                    hasWheel=true;
                    carPartsList[0] = wheelUiImage;
                    wheelUiImage.SetActive(true);
                   }
                    if (hit.collider.tag == ("scrap")){
                    scrap.SetActive(false);
                    hasScrap=true;
                    carPartsList[1] = scrapUiImage;
                    scrapUiImage.SetActive(true);
                   }
                   if (hit.collider.tag == ("engine")){
                    Engine.SetActive(false);
                    hasEngine=true;
                    carPartsList[2] = engineUiImage;
                    engineUiImage.SetActive(true);
                   }
                   if (hit.collider.tag == ("gas")){
                    Gas.SetActive(false);
                    hasGas=true;
                    carPartsList[3] = gasUiImage;
                    gasUiImage.SetActive(true);
                   }
                   if (hit.collider.tag == ("seat")){
                    Seat.SetActive(false);
                    hasSeat=true;
                    carPartsList[4] = SeatUI;
                    SeatUI.SetActive(true);
                   }
                    if (hit.collider.tag == ("steeringWheel")){
                    SteeringWheel.SetActive(false);
                    hasSteeringWheel=true;
                    carPartsList[5] = SteeringWheelUI;
                    SteeringWheelUI.SetActive(true);
                   }
                   if (hasEngine && hasGas && hasScrap && hasWheel&&hasSeat&&hasSteeringWheel)
                   allPartsCollected = true;
                   
        }
      }
      if (Menu.isAdmin)
                   allPartsCollected = true;
    }
}
