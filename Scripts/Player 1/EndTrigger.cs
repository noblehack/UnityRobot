using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTrigger : MonoBehaviour
{
  public GameObject EndCanvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //checks if game is finished by checking end collider
      void OnTriggerEnter(Collider col){
        if (col.gameObject.name=="GameEndBlock"){
           Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            AudioListener.pause = true;
        EndCanvas.SetActive(true);
        Debug.Log("gameover");
        }
    }
}
