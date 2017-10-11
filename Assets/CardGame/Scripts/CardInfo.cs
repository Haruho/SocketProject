using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.IO;
using UnityEngine.UI;
public class CardInfo:MonoBehaviour
{
    public static CardInfo instance;
    //这些是卡牌的信息
    public Text cName;
    public Text cFunction;
    public Text cDescribe;


    private XmlDocument xml;
    private XmlNodeList xnl;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        xml = new XmlDocument();
        xml.Load(Application.dataPath + "/CardGame/CardProperts.xml");

        xnl = xml.SelectSingleNode("Cards").ChildNodes;

    }
    public void ShowInfoOnWindow(string number)
    {
        foreach (XmlElement x in xnl)
        {
            if (x.GetAttribute("id") == number)
            {
                XmlNodeList nxnl = x.ChildNodes;
                //XmlNodeList nxnl = x.SelectSingleNode("id").ChildNodes;
                cName.text = nxnl[0].InnerText;
                cFunction.text = nxnl[1].InnerText;
                cDescribe.text = nxnl[2].InnerText;
            }
        }
        //for (int i = 0;i<xnl.Count;i++)
        //{
        //    XmlElement x = (XmlElement)xnl[i];
        //    if (x.GetAttribute("id") == number)
        //    {
        //        XmlNodeList nxnl = xnl[i].SelectSingleNode("id").ChildNodes;
        //        cName.text = nxnl[0].InnerText;
        //        cFunction.text = nxnl[1].InnerText;
        //        cDescribe.text = nxnl[2].InnerText;
        //    }
        //}
    }
    
}

