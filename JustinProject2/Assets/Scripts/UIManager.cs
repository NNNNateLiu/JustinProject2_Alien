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
}
