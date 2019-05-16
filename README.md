# FIRE BIM

Ce projet Unity d'un viewer BIM sur appareil Android s'inscrit dans le cadre d'un projet d'étudiants ingénieurs en deuxième année de l'École Nationale des Sciences Géographiques.
Cette application Android permet de naviguer au sein d'un modèle 3D BIM, d'accéder à la description des objets du bâtiment, et de faire des requêtes attributaires à l'aide d'une barre de recherche.

## Contenu

Le dépôt contient quatres dossiers:
<ul>
<li> un dossier "FIRE_BIM_Unity" qui contient le projet modifiable dans l'éditeur Unity version 2018.3.</li>
<li> un dossier "Documentation" qui recense les documentations à destination des utilisateurs et des développeurs. </li>
<li> un dossier "Données" contenant le fichier texte "maquette.txt" contenant les attributs de la maquette, ainsi qu'un modèle 3D au format OBJ. Ce dernier nous a servi pour charger la maquette en runtime grâce au projet Unity de ce dépôt Github : https://github.com/gpvigano/AsImpL </li>
<li> un dossier "Executables" qui contient un fichier .Exe, qui affiche la maquette en rotation, et le script Python de conversion des données GeoJSON en Texte avec seulement les informations sémantiques. </li>
</ul>

### Prérequis

L'application fonctionne via un fichier SketchUp et un fichier texte attributaire . 
La seule obligation est que les noms des objets SketchUp et le nom des objets en JSON soient les mêmes.

Notre méthode est expliquée dans la documentation Développeur.


### Installation
<ul>
<li> Étape 0: Lancer le logiciel Unity Hub que téléchargeable à cette adresse : https://store.unity.com/fr, et choisir la version 2018.3. </li>
<li> Étape 1: Pour ouvir le projet Unity, cliquer sur le bouton "Open" dans Unity Hub puis cibler le dossier "FIRE_BIM_Unity". </li>
<li> Étape 2: Allez dans l'onglet File > Build Settings. Choisir la version Android, puis cliquer sur Build pour générer le fichier APK</li> 
<li> Étape 3: Créer un dossier à la racine de la mémoire interne de l'appareil Android qui s'appellera "unityTest" ( !!! sensible à la casse). Dans ce dossier, glisser le fichier attributaire "maquette.txt" présent dans le dossier "Données" du dépôt GitHub.</li> 
<li> Étape 4: Installer le fichier APK, transféré au préalable sur le téléphone </li>
</ul>



## Auteurs

* **Bres Lucas** - Étudiant à l'ENSG - Chef de Projet - lucas.bres@ensg.eu - lucas.bres96@gmail.com
* **Hue Valentin** - Étudiant à l'ENSG - valentin.hue@ensg.eu
* **Labourg Priscillia** - Étudiante à l'ENSG - priscillia.labourg@ensg.eu
* **Peng Yanzhuo** - Étudiante à l'ENSG - yanzhuo.peng@ensg.eu


