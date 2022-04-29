using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Department : MonoBehaviour
{
    //每个部门所有的NPC
    private List<NPC> npcsInThisDepartment;
    [SerializeField] private Flowchart flowchart;
    
    public void ChangeInfluenceValue(int delta)
    {
        GameManager.instance.influenceValue += delta;
        //testFlowchart.SetIntegerVariable("Influence",delta);
    }
    
    public void LocalSetIntelligenceValue(int delta)
    {
        GameManager.instance.SetIntelligenceValue(delta);
    }
    
    public void LocalSetDateValue(int delta)
    {
        GameManager.instance.SetDateValue(delta);
    }
    
    public void TryControlMinisters(int baseValue)
    {
        if (GameManager.instance.intelligenceValue > baseValue)
        {
            Debug.Log("control success");
            flowchart.SetBooleanVariable("isControlSucceed",true);
        }
        else
        {
            Debug.Log("control fail");
            flowchart.SetBooleanVariable("isControlSucceed",false);
        }
    }
    
}
