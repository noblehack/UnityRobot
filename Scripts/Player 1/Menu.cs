using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    public GameObject menu;
    public TMP_Dropdown resolutionDropDown;
    public TMP_Dropdown qualityDropdown;
    public Slider fovSlider;
    public TMP_Text fovText;
    public Camera cam;
    public float fov;
    public GameObject startMenu;
    public bool gameStarted = false;
    public GameObject howToPlayMenu;
   public TMP_InputField adminInput;
   public static bool isAdmin = false;
   public GameObject endMenu;
   public GameObject difficultyMenu;
   public static int aiSpeed;
   
    // Start is called before the first frame update
    void Start()
    {
      //changes graphics settings
        Terrain.activeTerrain.detailObjectDensity = .2f;
         Terrain.activeTerrain.treeDistance =100f;
          Time.timeScale = 0;
            AudioListener.pause = true;
            Cursor.lockState = CursorLockMode.None;
            howToPlayMenu.SetActive(false);
            startMenu.SetActive(true);
            menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        //pauses game
        if (Input.GetKeyDown(KeyCode.Tab) && gameStarted){
            if (startMenu.activeSelf == false){
           disableAllMenus();
           startMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            AudioListener.pause = true;
         } else if(gameStarted){
            disableAllMenus();
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            AudioListener.pause = false;
         }
       }
       
    }
//changes fov
    public void onFovChange(){

      fov = fovSlider.value;
      cam.fieldOfView = fov;
      fovText.SetText("Fov: " + fov);
      
    }
    //other quality settings
    public void OnQualityChanged(){
      if (qualityDropdown.value == 0){

         Terrain.activeTerrain.detailObjectDensity =1f;
         Terrain.activeTerrain.treeDistance =2000f;
        QualitySettings.SetQualityLevel(4);
      }  if (qualityDropdown.value == 1){
         Terrain.activeTerrain.detailObjectDensity = 0.5f;
         Terrain.activeTerrain.treeDistance =500f;
        QualitySettings.SetQualityLevel(2);
      }  if ( (qualityDropdown.value == 2)){
         Terrain.activeTerrain.detailObjectDensity = .2f;
         Terrain.activeTerrain.treeDistance =100f;
         QualitySettings.SetQualityLevel(0);
      }
    }
    //changes resolution
       public void OnValueChanged(){
        int val = resolutionDropDown.value;
        Debug.Log(val);
       if (val == 0){
       Screen.SetResolution(3840, 2160, true);
       } else if ( val == 1){
        Screen.SetResolution(2560, 1440, true);
       } else if ( val == 2){
        Screen.SetResolution(1920, 1080, true);
       }else if ( val == 3){
        Screen.SetResolution(1280, 720, true);
       }else if ( val == 4){
        Screen.SetResolution(848, 480, true);
       }
       }
       //quits game
       public void onQuit(){
        Application.Quit();
       }
       //closes menus
       public void onBackFromOptions(){
         menu.SetActive(false);
         howToPlayMenu.SetActive(false);
         startMenu.SetActive(true);
       }
       //opens menus
       public void openOptions(){
         menu.SetActive(true);
         startMenu.SetActive(false);
       }
       //starts game
       public void onPlay(){
         startMenu.SetActive(false);
         if (!gameStarted){
         difficultyMenu.SetActive(true);
         } else{
          startGame();
         }
        
       }
       //opens how to play photos
       public void onHowToPlay(){
         startMenu.SetActive(false);
         howToPlayMenu.SetActive(true);
       }
       //checks admin password
       public void onPasswordChange(){
         string temp = adminInput.text;
         if (temp == "asd"){
            isAdmin = true;

         }
       }
       //resets scene
       public void onReset(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

       }
       //game over scene menus
       public  void gameOver(){
         disableAllMenus();
         endMenu.SetActive(true);
          Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0;
            AudioListener.pause = true;
         
       }
       //disables all menus
       public void disableAllMenus(){
        startMenu.SetActive(false);
        menu.SetActive(false);
        endMenu.SetActive(false);
        howToPlayMenu.SetActive(false);
        difficultyMenu.SetActive(false);
       }
       //stars game
       void startGame(){
        disableAllMenus();
         gameStarted = true;
         Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
            AudioListener.pause = false;
       }
       //difficulty options
       public void onEasyDifficulty(){
          aiSpeed = 7;
          flashlight.flashLightDrainSpeed = 75;
          movement.lives = 3;
          movement.staminaAmount = 10;
          startGame();
       }
      public void onMediumDifficulty(){
        aiSpeed = 10;
        flashlight.flashLightDrainSpeed = 50;
        movement.lives = 2;
        movement.staminaAmount = 20;
      startGame();
       }
       public void onHardDifficulty(){
        aiSpeed = 13;
        flashlight.flashLightDrainSpeed = 25;
        movement.lives = 1;
        movement.staminaAmount = 30;
      startGame();
       }
}   
