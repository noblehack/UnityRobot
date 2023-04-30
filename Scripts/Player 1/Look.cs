using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Look : MonoBehaviour
{
    public static float mouseSensitivity = 300f;
    public Transform playerBody;
    public GameObject player;
    public Slider sensitivity;

    float xRotation = 0f;
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
       
        //mouse movement logic
        
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            //hip.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        
    }
//zoom logic
    public void ZoomIn(bool zoom) {
        if(zoom) {
            Camera.main.fieldOfView = 50f;
        } else {
            Camera.main.fieldOfView = 60f;
        }
    }
   public void onDPIchange(){
        mouseSensitivity = (sensitivity.value);
    }
}