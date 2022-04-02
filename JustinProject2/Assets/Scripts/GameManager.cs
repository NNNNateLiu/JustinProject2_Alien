using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class GameManager : MonoBehaviour
{
    //Core Value 核心数值
    
    //用于提升Skill2和Skill3的值，累加数值，到达一定值解锁对应技能
    public int technologyValue;
    
    //用于提升Skill1的值，累加数值，到达一定值解锁对应技能
    public int intelligenceValue;
    
    //用于进行选举事件检测，类似文明时代点数，可增可减少的数值
    public int influenceValue;

    //游戏进行的第几天
    public int currentDate = 0;
    
    //游戏中的部门
    public List<Department> departmentsInTheCountry;

    public Flowchart testFlowchart;

    private void Start()
    {
        testFlowchart.SetIntegerVariable("DateInt",9);
    }

    public void ChangeInfluenceValue(int delta)
    {
        influenceValue += delta;
        testFlowchart.SetIntegerVariable("Influence",delta);
    }
}
