using System;
using TMPro;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecondGame : MonoBehaviour
{
    public TextAsset document;
    XmlDocument file = new XmlDocument();
    private XmlNode root;
    private XmlNodeList numberNode;
    private string res;
    private List<int> numberList = new List<int>();
    public TMP_Text target;
    void Start()
    {
        file.LoadXml(document.text);
        root = file.SelectSingleNode("Root");
        numberNode = root.ChildNodes;
        numberList = LoadData(10, 50, 100, 1000, 2023);
        Show(numberList);
        //Show(numberList);
    }
    
    
    
    private List<int> LoadData(params int[] args)
    {
        List<int> datas = new List<int>();
        for (int i = 0; i < args.Length; i++)
        {
            datas.Add(int.Parse(numberNode[args[i] - 1].InnerText));        
        }
        return datas;
    }
    

    public void Restart()
    {
        numberList = LoadData(10, 50, 100, 1000, 2023);
        target.text= " ";
        Show(numberList);
    }

    public void ToMain()
    {
        SceneManager.LoadScene(1);
    }

    public void RemoveMin()
    {
        if (numberList.Count > 0)
        {
            numberList.Remove(numberList.Min());
            Show(numberList);
        }
        else
        {
            target.color = Color.red;
            target.fontSize = 11;
            target.text = "数据已达下限，不可删除";
        }
        
    }

    public void RemoveMax()
    {
        if (numberList.Count > 0)
        {
            numberList.Remove(numberList.Max());
            Show(numberList);
            
        }
        else
        {
            target.color = Color.red;
            target.fontSize = 11;
            target.text = "数据已达下限，不可删除";
        }
        
    }

    public void SortAscending()
    {
        if (numberList.Count > 0)
        {
            numberList.Sort();
            Show(numberList);
            
        }
        else
        {
            target.color = Color.red;
            target.fontSize = 11;
            target.text = "数据已达下限，不可排序";
        }
    }

    public void SortDescending()
    {
        if (numberList.Count > 0)
        {
            numberList.Sort((a,b) => -a.CompareTo(b));
            Show(numberList);
            
        }
        else
        {
            target.color = Color.red;
            target.fontSize = 11;
            target.text = "数据已达下限，不可排序";
        }
    }

    private void Show(List<int> n)
    {
        target.text = null;
        foreach (int i in n)
        {
            res += i + ",";
        }

        try
        {
            target.text = res.Remove(res.Length - 1, 1);
        }
        catch (NullReferenceException)
        {
            
        }
        res = null;
        target.color = Color.black;
    }
    
}
