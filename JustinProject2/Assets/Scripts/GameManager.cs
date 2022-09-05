using System;
using System.Collections;
using System.Collections.Generic;using System.Net;
using UnityEngine;
using Fungus;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

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
    //距离上次进入宣传部的日期，如果大于等于3，则Influence减1
    public int dateBetweenLastTimeEnterDoP;

    //游戏进行的第几天
    public int currentDate = 0;
    
    //游戏中的部门
    public List<Department> departmentsInTheCountry;

    //刺杀事件相关
    //从QWER四个键钮随机出来一个
    public int chanceToTriggerAssassination = 80;
    public bool isAssassinTimerStarts;
    public float reactTimeLimits;
    public float currentReactingTime;
    private KeyCode buttonShouldBePressed = KeyCode.Q;

    private void Start()
    {
        ReadData();
    }
    
    
    private void Update()
    {
        if (isAssassinTimerStarts)
        {
            currentReactingTime += Time.deltaTime;
            if (Input.GetKeyDown(buttonShouldBePressed))
            {
                //如果按下正确按钮，刺杀事件结束，成功生还
                isAssassinTimerStarts = false;
                currentReactingTime = 0;
                //关闭UI上的QTEPanel
                UIManager.instance.QTEPanel.SetActive(false);
                GetRandomQTEButton();
            }
            if (currentReactingTime >= reactTimeLimits)
            {
                GameOver();
            }

        }
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
        //当声望值低于2的时候，游戏结束"下台"
        if (influenceValue < 3)
        {
            GameOver();
        }
        else
        {
            Debug.Log("not over yet");
        }
        UIManager.instance.txt_Influence.text = "Influ: " + influenceValue;
        flowchart.SetIntegerVariable("influenceValue",influenceValue);
    }

    public void SetDateValue(int value)
    {
        currentDate += value;
        UIManager.instance.txt_Date.text = "Date: " + currentDate;
        flowchart.SetIntegerVariable("Date",currentDate);
    }
    
    //每离开其他部门的时候调用
    public void SetDateBetweenLastTimeEnterDoPValue(int value)
    {
        dateBetweenLastTimeEnterDoP += value;
        
        //距离上次进入宣传部的日期，如果大于等于3，则Influence减1，计数器归零，重新计数
        if (dateBetweenLastTimeEnterDoP >= 3)
        {
            SetInfluenceValue(-1);
            ResetDateBetweenLastTimeEnterDoPValue();
        }
    }

    //每离开宣传部的时候调用
    public void ResetDateBetweenLastTimeEnterDoPValue()
    {
        dateBetweenLastTimeEnterDoP = 0;
    }

    public void ReadData()
    {
        flowchart.SetIntegerVariable("technology1Value",technology1Value);
        flowchart.SetIntegerVariable("technology2Value",technology2Value);
        flowchart.SetIntegerVariable("technology3Value",technology3Value);
        flowchart.SetIntegerVariable("intelligenceValue",intelligenceValue);
        flowchart.SetIntegerVariable("influenceValue",influenceValue);
        flowchart.SetIntegerVariable("Date",currentDate);
        
        SetDateValue(0);
        SetInfluenceValue(0);
        SetIntelligenceValue(0);
        SetTechnology3Value(0);
        SetTechnology2Value(0);
        SetTechnology1Value(0);
    }
    
    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
        UIManager.instance.QTEPanel.SetActive(false);
        Debug.Log("game over");
    }

    private KeyCode GetRandomQTEButton()
    {
        int keyCodeIndex = Random.Range(0, 4);
        switch (keyCodeIndex)
        {
            case 0:
                buttonShouldBePressed = KeyCode.Q;
                break;
            case 1:
                buttonShouldBePressed = KeyCode.W;
                break;
            case 2:
                buttonShouldBePressed = KeyCode.E;
                break;
            case 3:
                buttonShouldBePressed = KeyCode.R;
                break;
        }
        Debug.Log("You should Press " + buttonShouldBePressed);
        UIManager.instance.txt_KeyShouldBePressed.text = buttonShouldBePressed.ToString();
        return buttonShouldBePressed;
    }

}
