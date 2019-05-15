# -*- coding: utf-8 -*-

import json

with open('test_GML.geojson') as f: #Ouverture d'un fichier JSON contenant les données sémantiques
    data = json.load(f) #chargement des données JSON dans une variable Python
    

mon_fichier = open("maquette_test.txt", "w") # Ouverture d'un fichier texte. Le chemin est ici en relatif par rapport au script Python
"""
    Le fichier texte sera au format JSON. 
    Les noms des différents objets devront concorder avec les noms des GameObjects dans Unity
    La syntaxe du fichier sera de la forme suivante :
        { "Nom_Unity_1" : {"Attribut_1" : Valeur_1 , "Attribut_2" : "Valeur_2"}
          "Nom_Unity_2" : {"Attribut_1" : Valeur_1 , "Attribut_2" : "Valeur_2"} }
    Les noms des objets Unity s'appelle "Group [numéro impaire]". Exemple : "Group 1", "Group 3", ...
"""
mon_fichier.write("{")  #Initialisation du fichier texte 
compteur = 1            #Compteur qui correspond au numéro impaire du nom de l'objet
for feature in data['features']:    #On parcourt chaque objet
    mon_fichier.write("\"Group " + str(compteur) + "\" : ") #La syntaxe \" permet d'écrire la double quote dans un string
    json_string = str(json.dumps(feature['properties'], ensure_ascii=False)).replace("'", "_") #json.dumps permet une conversion de bytes en string
    mon_fichier.write(json_string.replace("\\n", " ")) #Le fichier GeoJSON comportait la présence de \n qui signifie "retour à la ligne". On les remplace par un espace
    mon_fichier.write("\n") #Retour à la ligne après avoir écrit les attributs de l'objet
    compteur += 2           #Incrémentation de 2 du compteur 
mon_fichier.write("}")  #Conclusion du fichier texte
mon_fichier.close()     #Fermeture du fichier texte
    