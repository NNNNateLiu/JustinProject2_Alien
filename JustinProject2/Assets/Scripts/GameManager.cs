using System;
using System.Collections;
using System.Collections.Generic;using System.Net;
using UnityEngine;
using Fungus;
using Unity.VisualScripting;

public enum DepartmentType {DoD, DoT, DoP}
public class GameManager : MonoBehaviour
{
    //Core Value 核心数值
    public static GameManager instance;

    public Flowchart flowchart;

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

    //科技值（1，2，3，）：用于提升Skill2和Skill3的值，累加数值，到达一定值解锁对应技能
    public int technology1Value = 0;
    public int technology2Value = 0;
    public int technology3Value = 0;
    
    //信息值：用于提升Skill1的值，累加数值，到达一定值解锁对应技能
    public int intelligenceValue = 0;
    
    //声望值：用于进行选举事件检测，类似文明时代点数，可增可减少的数值
    public int influenceValue = 0;

    //游戏进行的第几天
    public int currentDate = 0;
    
    //游戏中的部门
    public List<Department> departmentsInTheCountry;

    private void Start()
    {
        ReadData();
    }

    public void SetTechnology1Value(int value)
    {
        technology1Value += value;
        UIManager.instance.txt_Tech1.text = "Tech1: " + technology1Value;
        flowchart.SetIntegerVariable("technology1Value",technology1Value);
    }
    public void SetTechnology2Value(int value)
    {
        technology2Value += value;
        UIManager.instance.txt_Tech2.text = "Tech2: " + technology2Value;
        flowchart.SetIntegerVariable("technology2Value",technology2Value);
    }
    public void SetTechnology3Value(int value)
    {
        technology3Value += value;
        UIManager.instance.txt_Tech3.text = "Tech3: " + technology3Value;
        flowchart.SetIntegerVariable("technology3Value",technology3Value);
    }
    public void SetIntelligenceValue(int value)
    {
        intelligenceValue += value;
        if (intelligenceValue >= 10)
        {
            intelligenceValue = 10;
        }
        UIManager.instance.txt_Intelligence.text = "Intel: " + intelligenceValue;
        flowchart.SetIntegerVariable("intelligenceValue",intelligenceValue);
    }
    public void SetInfluenceValue(int value)
    {
        influenceValue += value;
        UIManager.instance.txt_Influence.text = "Influ: " + influenceValue;
        flowchart.SetIntegerVariable("influenceValue",influenceValue);
    }

    public void SetDateValue(int value)
    {
        currentDate += value;
        UIManager.instance.txt_Date.text = "Date: " + currentDate;
        flowchart.SetIntegerVariable("Date",currentDate);
    }

    public void ReadData()
    {
        SetTechnology1Value(technology1Value);
        SetTechnology2Value(technology2Value);
        SetTechnology3Value(technology3Value);
        SetInfluenceValue(influenceValue);
        SetIntelligenceValue(intelligenceValue);
        SetDateValue(currentDate);
    }

}
