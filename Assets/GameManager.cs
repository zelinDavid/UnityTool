using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    Transform btn;
    // Start is called before the first frame update
    void Start()
    {
    //   Button btnn =   btn.GetComponent<Button>();
   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadLevel(string name){
        Debug.Log("name:"+ name);

    }

    public void sliderChange(float value){
        DataManager.ShareInstance.RotateSpeed = value;
        Debug.LogWarning ("sliderChange" + value);
    }
}
