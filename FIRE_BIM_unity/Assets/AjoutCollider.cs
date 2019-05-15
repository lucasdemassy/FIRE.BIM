using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System;
using System.IO;
using System.Text;
using System.Xml.Serialization;
using SimpleJSON;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AjoutCollider : MonoBehaviour
{

    
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject batiment = GameObject.Find("MAQUETTE_pompier_fromGML");
        for (int i = 1; i < 412 * 2; i += 2)
        {
            string nameObject = "Group " + i.ToString();
            string nameScript = "Group_" + i.ToString();
            string copyPath = "Assets/" + nameScript + ".cs";
            GameObject obj = GameObject.Find(nameObject);
            obj.tag = "BATI";
            obj.AddComponent<MeshCollider>();
        }

        string fileName = "/storage/emulated/0/unityTest/maquette.txt";
        //Text affichage = GameObject.Find("Text").GetComponent<Text>();
        if (File.Exists(fileName))
        {
            string data;

            using (var sr = new StreamReader(fileName))
            {
                data = sr.ReadToEnd();
            }
            

            //affichage.text = data;
            //affichage.text = "True";

        }
        else
        {
            //affichage.text = "False";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
