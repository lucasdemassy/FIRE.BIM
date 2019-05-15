using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using UnityEditor;
using System;
using System.Text;
using System.Xml.Serialization;
using SimpleJSON;
using UnityEngine.SceneManagement;
using System.Text.RegularExpressions;


/// <summary>
/// Classe qui gère le comportement lors de la sélection d'un objet, d'une recherche dans la barre de recherche
/// </summary>
/// <param name=" search"> Fait référence au bouton "Rechercher" de l'interface</param>
/// <param name="jsonPath">Adresse du fichier texte contenant les attributs</param> 
/// <param name="SimpleJSON.JSONNode">Dictionnaire contenant les attributs</param>
/// <param name="input">Référence à la barre de recherche</param>
/// <param name="opt">Référence à la fenêtre déroulant affichant les différents types d'attributs</param>
/// <param name="data">String contenant les attributs</param> 
/// <param name="selected_material">Aspect des objets sélectionné par la recherche</param>
/// <param name="unselected_material">Aspect des objets non-sélectionné par la recherche</param>
public class Recherche_Valid : MonoBehaviour
{
    public Button search;
    public string jsonPath = "/storage/emulated/0/unityTest/maquette.txt";
    public SimpleJSON.JSONNode N;
    public Text input;
    public Text opt;
    public string data;
    public Text Info;
    public Material selected_material;
    public Material unselected_material;

    // Start is called before the first frame update
    /// <summary>
    /// Fonction est appellé avant la première frame Update. Elle lit le fichier texte externe pour charger les attributs dans une variables C#
    /// </summary>
    public void Start()
    {
        Button search = GameObject.Find("Search_Button").GetComponent<Button>();
        Text input = GameObject.Find("Text_Input").GetComponent<Text>();
        Text opt = GameObject.Find("Label").GetComponent<Text>();
        Text Info = GameObject.Find("Text_Panel").GetComponent<Text>();


        if (File.Exists(jsonPath))
        {
            string data;

            using (var sr = new StreamReader(jsonPath))
            {
                data = sr.ReadToEnd();
            }

            
        }

        N = JSON.Parse(data);

    }


    /// <summary>
    /// Fonction est appelé à chaque fois que le bouton Recherche est pressé 
    /// </summary>
    /// <remarks>Requête attributaire sur le fichier texte</remarks>
    /// <remarks>Puis réinitialisation de l'ascpect de toutes les géométries</remarks>
    /// <remarks>Enfin changement de couleur pour les objets associés au résultat de la requête</remarks>
    public void Rechercher()
    {

        Text info = GameObject.Find("Text_Panel").GetComponent<Text>();
        Text Input = GameObject.Find("Text_Input").GetComponent<Text>();
        Text Opt = GameObject.Find("Label").GetComponent<Text>();

        string data2 = "Pas de fichier";
        if (File.Exists("/storage/emulated/0/unityTest/maquette.txt"))
        {
            using (var sr = new StreamReader("/storage/emulated/0/unityTest/maquette.txt"))
            {
                data2 = sr.ReadToEnd();
                
            }
        }
        var N = JSON.Parse(data2);
        


        Retour();
        for (int i = 1; i < N.Count * 2; i += 2)
        {
           
            string nameObject = "Group " + i.ToString();
            var util = new Regex(@"^(?i)" + Input.text + @".*(?-i)");
            info.text += nameObject;
            string typ = Opt.text;
            info.text = "Recherche effectuée";
;        
            
            if (util.IsMatch(N[nameObject][typ].Value) & N[nameObject][typ] != null)
            {
                
                Selectionner(GameObject.Find(nameObject), false);
                
                
            }
            
        }

    }

    // Permet de remettre
    /// <summary>
    /// Permet de réinitialiser l'aspect de tout les objets
    /// </summary>
    /// <remarks>Si l'aspect de l'objet est celui d'une résultat de recherche, condition supplémentaire pour réinitialiser le Shader</remarks>
    public void Retour()
    {
        GameObject[] liste = GameObject.FindGameObjectsWithTag("BATI");
        foreach (GameObject go in liste)
        {
            Material m = go.transform.GetComponent<Renderer>().material;
            Color couleur = m.color;
            if (m = selected_material)
            {
                couleur.a = 0f;
                go.transform.GetComponent<Renderer>().material.SetColor("_OutlineColor", couleur);
            }
            
            m = unselected_material;
            couleur = Color.white;            
            go.transform.GetComponent<Renderer>().material.SetColor("_Color", couleur);
            
            


        }
    }


    /// <summary>
    /// Change l'aspect des objets afin de mettre en évidence qu'ils sont sélectionnés
    /// </summary>
    /// <param name="objet">GameObject à changer d'aspect</param>
    /// <param name="single">Booléen pour déterminer si l'objet doit avoir un aspect de sélection par clic ou par recherche</param>
    public void Selectionner(GameObject objet, bool single=true)
    {
        Text info = GameObject.Find("Text_Panel").GetComponent<Text>();
        info.text = "Clic";
        if (single)
        {
            string name = objet.name;
            string data2 = "Pas de fichier";
            if (File.Exists("/storage/emulated/0/unityTest/maquette.txt"))
            {
                using (var sr = new StreamReader("/storage/emulated/0/unityTest/maquette.txt"))
                {
                    data2 = sr.ReadToEnd();
                }
            }
            var N = JSON.Parse(data2);
            info.text = N[name].ToString();
            string attributs = "Name :" + N[name]["Name"].ToString() + "\n";
            attributs += "ObjectType :" + N[name]["ObjectType"].ToString() +"\n";
            attributs += "Tag :" + N[name]["Tag"].ToString() + "\n";
            attributs += "OBJECTID :" + N[name]["OBJECTID"].ToString() + "\n";
            attributs += "Description :" + N[name]["Description"].ToString();
            //info.text = attributs;
            objet.GetComponent<Renderer>().material.color = Color.red;
        }
        else
        {
            info.text = "Recherche effectuée";
            Material outlined_material = Resources.Load("Outlined_material", typeof(Material)) as Material;
            //Material outlined_material = new Material(Shader.Find("Outlined/Silhouetted Bumped Diffuse"));
            objet.GetComponent<Renderer>().material = selected_material;
            objet.GetComponent<Renderer>().material.color = Color.green;
        }
        
         
        
        
    }

    





}
