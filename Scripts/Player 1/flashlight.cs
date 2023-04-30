using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashlight : MonoBehaviour
{
    public Light flashLight;
    bool canFlash = true;
    public float flashlightIntensity = 3f;
    public Slider batteryLife;
    public Animator flashlightBobble;
    public GameObject cam;
    public GameObject flashLightObject;
    public GameObject playerObject;
    public static float flashLightDrainSpeed = 50;
    
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
      //checks if user is admin
        if (Menu.isAdmin){
        flashLight.intensity = 3f;
      flashlightIntensity = 3f;
      batteryLife.value = 3f;
        }

      //toggles ultrabright
      if(Input.GetMouseButton(1)){
        
        flashLight.range = 1000f;
        flashlightIntensity = batteryLife.value-(Time.deltaTime / (flashLightDrainSpeed / 10));
      } else{
        flashLight.range = 30f;
        flashLight.intensity = batteryLife.value;
      }

        if (Input.GetKey ("a") ||Input.GetKey ("w") ||Input.GetKey ("d")||Input.GetKey ("s")){
          flashlightBobble.SetTrigger("isBobble");
        } else {
            flashlightBobble.ResetTrigger("isBobble");
        }
        //changes battery slider
        batteryLife.value = flashlightIntensity;

        //counts battery life
        if (flashLight.enabled == true){
           // Debug.Log(flashlightIntensity);
            flashlightIntensity = flashlightIntensity-(Time.deltaTime/flashLightDrainSpeed);
        if (flashlightIntensity<= 1)
        flashLight.intensity = flashlightIntensity;
        }
        //flash logic
        if (flashLight.intensity < 1 && canFlash && flashLight.enabled){
          canFlash = false;
          float range = Random.Range(.05f,.15f);
          flashLight.enabled = (false);
        StartCoroutine (flash(range)); 
        StartCoroutine (flashCooldown()); 
        }
        if (flashLight.intensity>1){
          canFlash = false;
          
        }
          //toggles flashlight
        if (Input.GetKeyDown("f")){
            flashLight.enabled = !flashLight.enabled;
        }
      
      flashLightObject.transform.rotation  = Quaternion.Euler(cam.transform.localEulerAngles.x,playerObject.transform.localEulerAngles.y, playerObject.transform.localEulerAngles.z);

    }
      IEnumerator flash(float range){
        Debug.Log(range);
        yield return new WaitForSeconds(range);
        
        flashLight.enabled = (true); 
        
    }
    //flashlight flashing cooldown
    IEnumerator flashCooldown(){
        if (flashLight.intensity>.5){
         yield return new WaitForSeconds( Random.Range(1f,10f));
        } else {
             yield return new WaitForSeconds( Random.Range(1f,3f));
        }
        canFlash = true;
    }
    public void charge(){
        if (flashlightIntensity < 3)
        flashlightIntensity = flashlightIntensity+.01f;
    }
}
