using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecordsScript : MonoBehaviour
{
    XDocument doc;
    int[] recordsArray;
    bool needToWrite;
    private void Start()
    {
        GameObject.Find("CurrentScore").GetComponent<Text>().text = "Вы набрали " + SpaceInvaders.score + " очков";

        if (File.Exists(Application.dataPath + "/records.xml"))
        {
            FileStream stream = File.Open(Application.dataPath + "/records.xml", FileMode.Open);
            doc = XDocument.Load(stream);
            stream.Close();
        }
        else
        {
            doc = new XDocument(new XElement("records"));
        }
        int counter = 0;
        recordsArray = new int[10];
        var recordsCollection = doc.Element("records").Elements("record");
        foreach (XElement a in recordsCollection)
        {
            recordsArray[counter] = Int32.Parse(a.Attribute("score").Value);
            counter++;
        }

        for (int i = 0; i < 10; i++)
        {
            if (SpaceInvaders.score > recordsArray[i])
            {
                GameObject.Find("CurrentScore").GetComponent<Text>().text += " и заняли " + (i + 1).ToString() + "-е место в таблице рекордов";
                needToWrite = true;
                break;
            }
            if (i == 9)
            {
                GameObject.Find("InputField").SetActive(false);
                needToWrite = false;
            }
        }
    }

    public void WriteRecordToXml()
    {
        if (needToWrite)
        {
            XNode node = doc.Element("records").FirstNode;

            for (int i = 0; i < 10; i++)
            {
                if (SpaceInvaders.score > recordsArray[i] && recordsArray[i] > 0)
                {
                    MoveElements(i+1);
                    node.ReplaceWith(new XElement("record", new XAttribute("name", GameObject.Find("NameField").GetComponent<Text>().text), new XAttribute("score", SpaceInvaders.score)));
                    break;
                }

                if (node == null && i < 9)
                {
                    doc.Element("records").Add(new XElement("record", new XAttribute("name", GameObject.Find("NameField").GetComponent<Text>().text), new XAttribute("score", SpaceInvaders.score)));
                    break;
                }
                node = node.NextNode;
            }
            doc.Save(Application.dataPath + "/records.xml");
        }
    }

    void MoveElements(int index)
    {
        XNode previous = null;
        XNode node = doc.Element("records").LastNode;

        int nodesAmount = 0;
        foreach(var a in doc.Element("records").Nodes())
        {
            nodesAmount++;
        }
        
        if(nodesAmount < 10)
        {
            doc.Element("records").Add(node);
        }

        for(int i = nodesAmount; i > index; i--)
        {
            if (node != null)
            {
                if (node.PreviousNode == null)
                    break;
                previous = node.PreviousNode;
                node.ReplaceWith(node.PreviousNode);
            }
            node = previous;
        }
    }
}
