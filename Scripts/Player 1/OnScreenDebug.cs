using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class OnScreenDebug : MonoBehaviour
{
    public TMP_Text fpsCounter;
    
    // Start is called before the first frame update
    void Start()
    {
          InvokeRepeating("OutputTime", .2f, .5f);    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OutputTime() {
        //updates fps counter
     fpsCounter.text = ("" + Mathf.Round(1.0f / Time.deltaTime));
 }
}
