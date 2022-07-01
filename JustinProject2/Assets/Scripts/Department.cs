using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class Department : MonoBehaviour
{
    //每个部门所有的NPC
    private List<NPC> npcsInThisDepartment;
    [SerializeField] private Flowchart flowchart;

    public void LocalSetFlowchart()
    {
        GameManager.instance.flowchart = GameObject.Find("CurrentDepartmentFlowchart").GetComponent<Flowchart>();
        GameManager.instance.ReadData();
        OnEnterDepartment();
    }
    
    public void LocalSetInfluenceValue(int delta)
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
    
    public void LocalSetTechnology1Value(int delta)
    {
        GameManager.instance.SetTechnology1Value(delta);
    }
    
    public void LocalSetTechnology2Value(int delta)
    {
        GameManager.instance.SetTechnology2Value(delta);
    }
    
    public void LocalSetTechnology3Value(int delta)
    {
        GameManager.instance.SetTechnology3Value(delta);
    }

    public void LocalSetDateBetweenLastTimeEnterDoPValue(int delta)
    {
        GameManager.instance.SetDateBetweenLastTimeEnterDoPValue(delta);
    }

    public void LocalResetDateBetweenLastTimeEnterDoPValue()
    {
        GameManager.instance.ResetDateBetweenLastTimeEnterDoPValue();
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

    private void OnEnterDepartment()
    {
        //如果声望高于10，会触发刺杀事件（QTE躲避刺杀）
        if (GameManager.instance.influenceValue >= 10)
        {
            AssassinationEventCheck();
        }
    }

    private void AssassinationEventCheck()
    {
        int random = Random.Range(0, 100);
        if (random <= GameManager.instance.chanceToTriggerAssassination)
        {
            Debug.Log("Assassin Appear! " + random);
            //如果发生刺杀事件，首先让GameManager开始倒计时，UIManager的QTEPanel显示出来
            GameManager.instance.isAssassinTimerStarts = true;
            UIManager.instance.QTEPanel.SetActive(true);
        }
        else
        {
            Debug.Log("Safe Day " + random);
        }
    }
}
