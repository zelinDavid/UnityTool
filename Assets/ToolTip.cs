 
using System.Net.Mime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolTip : MonoBehaviour
{
    private Text ToolTipText; 
    private Text ContentText;
    private CanvasGroup CanvasGroup;
    private float taregetAlpha; 
    private Canvas Canvas;
    private Vector2 Offset = new Vector2(10, -10);
    
     public float smoothing = 1;

    private void Start()
    {
        Canvas =   GetComponentInParent<Canvas>();
        ToolTipText = GetComponent<Text>();
        ContentText = transform.Find("Content").GetComponent<Text>();
       
        CanvasGroup = GetComponent<CanvasGroup>();
    }

    private void Update() {
        if (CanvasGroup.alpha != taregetAlpha)
        {
            float alpha  = Mathf.Lerp(CanvasGroup.alpha, taregetAlpha ,smoothing * Time.deltaTime);
            CanvasGroup.alpha = alpha;
        }
        //  CanvasGroup.alpha = 1;
    }

    public void Show(string  textInfo){
        ToolTipText.text = textInfo;
        ContentText.text = textInfo;
        taregetAlpha = 1;
    }
     public void Hide()
    {
        taregetAlpha = 0;
    }

    public void SetLocation(Vector2 localPosition){
        gameObject.transform.localPosition = localPosition  ;
    }
    
}
