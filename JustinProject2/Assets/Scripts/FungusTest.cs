using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class FungusTest : MonoBehaviour
{
    public Flowchart testFlowchart;
    
    // Start is called before the first frame update
    void Start()
    {
        OnCollectFood();
    }

    public void OnCollectFood()
    {
        Debug.Log("food collected!");
        testFlowchart.SetBooleanVariable("isHaveFood",true);
    }
}
