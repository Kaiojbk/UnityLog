using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;
using JetBrains.Annotations;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;

public class Register : MonoBehaviour   
{
    public Toggle playerAccount;
    public Toggle touristAccount;
    public TMP_InputField userName, userPwd;
    public Button log, registration;
    public GameObject tips;                 //提示框后的背景（父物体）
    private TMP_Text tipsText;          //提示框
    private XmlDocument userInfoXml = new XmlDocument();
    private XmlNodeList userInfoNodes;
    private ResNumbers GetInfo;
    private string res;
    private bool isTourist, isAccount;      //游客登录 账户登录 标记
    private Dictionary<string, string> usersInfo = new Dictionary<string, string>();    //加载用户数据
    private XmlElement user,account, password;
    private int userCount = 0;
    
    private void Start()
    {
        Debug.Log(Application.persistentDataPath + "/UsersInfo.xml");
        tipsText = GameObject.FindGameObjectWithTag("TipText").GetComponent<TMP_Text>();
        tips.SetActive(false);
        GetInfo = GetComponent<ResNumbers>();
        userInfoXml.Load(Application.persistentDataPath + "/UsersInfo.xml");
        LoadUserInfo(userInfoXml);

    }
    

    /// <summary>
    /// 用户信息加载
    /// </summary>
    /// <param name="document"></param>
    private void LoadUserInfo(XmlDocument document)
    {
        XmlNodeList nodes = document.SelectSingleNode("Users").ChildNodes;           //(User节点)
        
        foreach (XmlNode item in nodes)
        {
            if (item != null)
            {
                userCount++;
                usersInfo.Add(item.SelectSingleNode("Account").InnerText, item.SelectSingleNode("Password").InnerText);
            }
        }
    }
    

    public void OnSelectPlayerAccount(bool value)
    {
        if (value)
        {
            touristAccount.isOn = false;
        }
        else
        {
            touristAccount.isOn = true;
        }
    }
    
    public void OnSelectTouristAccount(bool value)
    {
        if (value)
        {
            isTourist = true;
            userName.interactable = false;
            userName.text = null;
            userPwd.interactable = false;
            userPwd.text = null;
            registration.interactable = false;
            playerAccount.isOn = false;
        }
        else
        {
            isTourist = false;
            registration.interactable = true;
            userName.interactable = true;
            userPwd.interactable = true;
            playerAccount.isOn = true;
        }
        
    }

    /// <summary>
    /// 响应登录按钮
    /// </summary>
    public void ResLog()
    {
        if (isTourist || isAccount)
        {
            SceneManager.LoadScene(2);
        }
    }

    public void ResRegister()
    {
        int isReInput = 0;
        if (GetInfo.USERNAME != null && GetInfo.USERPWD != null)
        {
            foreach (KeyValuePair<string,string> item in usersInfo)
            {
                if (item.Key == GetInfo.USERNAME)
                {
                    isReInput--;
                    break;
                }
            }
        }
        if (isReInput >= 0)
        {
                
            AddUser(++userCount);
            tips.SetActive(true);
            tipsText.text = "用户已成功注册！";
            isAccount = true;
            StartCoroutine(HideTips());
        }
        else
        {
            tips.SetActive(true);
            tipsText.text = "用户已存在，请重新输入！";
            StartCoroutine(HideTips());
        }
    }

    private void AddUser(int number)
    {
        user =  userInfoXml.CreateElement("User" + number);
        account = userInfoXml.CreateElement("Account");
        password = userInfoXml.CreateElement("Password");
        account.InnerText = GetInfo.USERNAME;
        password.InnerText = GetInfo.USERPWD;
        userInfoXml.SelectSingleNode("Users").AppendChild(user);
        user.AppendChild(account);
        user.AppendChild(password);
        // ?
        userInfoXml.Save(Application.persistentDataPath + "/UsersInfo.xml");
        
    }

    IEnumerator HideTips()
    {
        yield return new WaitForSeconds(1.5f);
        tips.SetActive(false);
        StopCoroutine(HideTips());
    }
}
