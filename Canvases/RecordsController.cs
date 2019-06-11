using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RecordsController : MonoBehaviour {

    public GameObject Number, Name, Score, Line;

	void Start ()
    {
        XDocument doc;
        if (File.Exists(Application.dataPath + "/records.xml"))
        {
            FileStream stream = File.Open(Application.dataPath + "/records.xml", FileMode.Open);
            doc = XDocument.Load(stream);
            stream.Close();
        }
        else
        {
            doc = new XDocument(new XElement("records"));
            doc.Save(Application.dataPath + "/records.xml");
        }
        int counter = 1;

        var recordsCollection = doc.Element("records").Elements("record");
        foreach(XElement a in recordsCollection)
        {
            string name = a.Attribute("name").Value;
            string score = a.Attribute("score").Value;

            if (name == "")
                name = "<не указано>";

            GameObject numberObject = Instantiate(Number, Number.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, counter * -70), Quaternion.identity);
            numberObject.transform.SetParent(GameObject.Find("Records Menu").GetComponent<RectTransform>(), false);
            numberObject.GetComponent<Text>().text = counter.ToString();

            GameObject nameObject = Instantiate(Name, Name.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, counter * -70), Quaternion.identity);
            nameObject.transform.SetParent(GameObject.Find("Records Menu").GetComponent<RectTransform>(), false);
            nameObject.GetComponent<Text>().text = name;

            GameObject scoreObject = Instantiate(Score, Score.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, counter * -70), Quaternion.identity);
            scoreObject.transform.SetParent(GameObject.Find("Records Menu").GetComponent<RectTransform>(), false);
            scoreObject.GetComponent<Text>().text = score;

            GameObject lineObject = Instantiate(Line, Line.GetComponent<RectTransform>().anchoredPosition + new Vector2(0, counter * -70), Quaternion.identity);
            lineObject.transform.SetParent(GameObject.Find("Records Menu").GetComponent<RectTransform>(), false);

            counter++;
        }
	}
}
