using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public Text txt_Tech1;
    public Text txt_Tech2;
    public Text txt_Tech3;
    public Text txt_Intelligence;
    public Text txt_Influence;

    public Text txt_Date;

    public GameObject QTEPanel;
    public GameObject Img_QTETimer;
    private Vector3 tempQTETimerScale = Vector3.one;
    public Text txt_KeyShouldBePressed;
    
    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);

            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (GameManager.instance.isAssassinTimerStarts)
        {
            tempQTETimerScale.x = 1 - GameManager.instance.currentReactingTime / GameManager.instance.reactTimeLimits;
            Img_QTETimer.transform.localScale = tempQTETimerScale;
        }
    }
}
