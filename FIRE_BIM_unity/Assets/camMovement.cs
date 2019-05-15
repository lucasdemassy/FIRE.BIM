using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Classe qui permet de déplacer la caméra. Pour utiliser cette classe, il faut attacher le script camMovement.cs sur un empty GameObject qui servira de centre de rotation. La camera doit être nommé "Main Camera" pour que cela marche. Gère également le déplacement du centre de rotation sur l'objet cliquer sur l'écran.
/// </summary>
/// <param name="zoomMin"> zoom minimum, conseillé à 0 </param>
/// <param name="zoomMax"> zoom maximum, valeur utilisé lors du dèvelopment 20 </param>
/// <param name="SensibiliteZoom"> permet de régler la sensibilité du zoom (multiplication de la valeur de zoom), valeur utilisé 0.005 </param>
/// <param name="SensibiliteRotX"> de même que  que SensibiliteZoom, valeur conseillé 0.1</param>
/// <param name="SensibiliteRotY"> pareille, valeur conseillé 0.1 </param>
public class camMovement : MonoBehaviour
{   

    // Start is called before the first frame update
    float rotX;
    float rotY;

    Vector2 startZoomPos1;
    Vector2 nowZoomPos1;
    Vector2 startZoomPos2;
    Vector2 nowZoomPos2;
    private float distanceStart;
    float zoom;

    public float ZoomMin;
    public float ZoomMax;
    public float SensibiliteZoom;

    public float SensibiliteRotX;
    public float SensibiliteRotY;

    private bool zooming; //permet de savoir si on est en phase de zoom, tentative de corriger un "saut" de zoom

    Vector3 destination;
    float distMaxDestination;
    public GameObject camera;
    private float t;

    public Recherche_Valid R_Valid;
    public GameObject ScriptRecherche;
    /// <summary>
    /// Méthode qui est appelé à chaque lancement de l'application. Permet notament d'initialiser les variables
    /// </summary>
    void Start()
    {
        rotX = 0;
        rotY = 0;

        zoom = 1;
        zooming = false;

        destination = Vector3.positiveInfinity; // on le met infini pour pouvoir utiliser une conditions pour savoir si l'on est en train de deplacer la caméra
        distMaxDestination = 0;

        t = 0;

    }

    // Update is called once per frame    
    /// <summary>
    /// Est appelee à chaque Frame (mise a jour de l'application et donc de l'ecran. Dans ce, script camMovement.cs, on y trouve les controles de la camera. Parmi ces controles on trouve: la pression courte pour se deplacer sur l'objet, la rotation de la caméra par passage du doigts vers la direction souhaitee ainsi que le "pincage" pour zoomer/dezoomer.
    /// </summary>
    /// <remarks> 
    /// Le deplacement de la camera est réaliser en détectant la collision entre un <c>MeshCollider</c> et un <c>RayCast</c>. Le <c>MeshCollider</c> n'est pas pas present de base sur les mesh, il faut le rajouter avec un autre script <see cref="AjoutCollider"/>.
    /// </remarks>
    void Update()
    {
        t += Time.deltaTime; // utilise pour determiner si l'appui a un doigt est court ou long (deltaTime contient le temps en s depuis la dernière iteration
        if (distMaxDestination != 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, destination, distMaxDestination); // donne une nouvelle position qui se trouve à une distance distMaxDestination, on utilise MoveTowards pour eviter d'avoir un déplacement instantane qui serait déboussolant.
            if (destination == transform.position)
            {
                destination = Vector3.positiveInfinity; //ce qui se rapproche le plus du null, n'est pas nécessaire
                distMaxDestination = 0;
            }
        }
        if (Input.touchCount == 1)
        {

            zooming = false; // on ne zoom pas avec un doigt, cf ligne 
            Touch touch = Input.GetTouch(0);

            // Move the cube if the screen has the finger moving.
            if (touch.phase == TouchPhase.Moved)
            {
                Vector2 deltaPos = touch.deltaPosition;
                rotX += deltaPos.x;
                rotY += deltaPos.y;

                // utiliser les angles d'Euler pour les rotations donne des résultats étranges si on le fait sur des 
                transform.rotation = Quaternion.Euler(-SensibiliteRotY * rotY, SensibiliteRotX * rotX, 0);
            }
            if (touch.phase == TouchPhase.Began)
            {
                t = 0;
            }
            if (touch.phase == TouchPhase.Ended & t <= Time.deltaTime * 3)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(touch.position.x, touch.position.y, 0));
                if (Physics.Raycast(ray.origin, ray.direction, out hit))
                {
                    Vector3 centre = hit.collider.gameObject.GetComponent<Renderer>().bounds.center;
                    deplacerCentreRotation(centre);
                    //hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
                    GameObject target = hit.collider.gameObject;
                    GameObject canvas = GameObject.Find("Canvas");
                    R_Valid = canvas.GetComponent<Recherche_Valid>();
                    if (R_Valid == null)
                    {
                        R_Valid = gameObject.AddComponent<Recherche_Valid>();

                    }
                    R_Valid.Retour();
                    R_Valid.Selectionner(target);
                    //ScriptRecherche.GetComponent<Recherche_Valid>().Selectionner(hit.collider.gameObject);
                }
            }
        }

        if (Input.touchCount == 2)
        {
            Touch touch1 = Input.GetTouch(0);
            Touch touch2 = Input.GetTouch(1);

            if (zooming == false) //initialise le zoom, ne doit pas etre fait a chaque fois 
            {
                startZoomPos1 = touch1.position;
                startZoomPos2 = touch2.position;
                distanceStart = Vector2.Distance(startZoomPos1, startZoomPos2);
                zooming = true;
            }
            
            // le zoom est proportionelle à la différence de distance entre les deux doigts entre deux update consecutif.
            nowZoomPos1 = touch1.position;
            nowZoomPos2 = touch2.position;
            distanceStart = Vector2.Distance(startZoomPos1, startZoomPos2);
            float distanceNow = Vector2.Distance(nowZoomPos1, nowZoomPos2);
            startZoomPos1 = nowZoomPos1;
            startZoomPos2 = nowZoomPos2;
            zoom = (distanceNow - distanceStart) * SensibiliteZoom;// on applique la sensibilite pour reduire l'intensite du zoom

            zoomer(zoom);
            
        }
    }
    /// <summary>
    /// Deplace le centre de rotation (le GameObject au-quel ce script est attache) vers la position donnee en entré
    /// </summary>
    /// <param name="nouveauCentre">le nouveau centre de rotation</param>
    /// <typeparam name="nouveauCentre"> Vector3 </typeparam>
    public void deplacerCentreRotation(Vector3 nouveauCentre){
        this.destination = nouveauCentre;
        this.distMaxDestination = Vector3.Distance(transform.position,destination)/4;
        }
    /// <summary>
    /// effectue un zoom ( déplacement sur l'axe perpendiculaire à l'ecran ) vers le centre de rotation d'une valeur <c>valeurZoom</c>. Si le zoom resultant dépasse les valeurs <c>zoomMin</c> et <c>zoomMax</c>, le deplacement n'est pas effectue.
    /// </summary>
    /// <param name="valeurZoom">La valeur a ajoute au zoom.</param>
    /// <remarks>
    /// probleme rencontre lors du passage coordonne local/coordonne global car la transformation <c>Translate</c> se fait en coordonne locale alors que camera.transform.z est exprime en coordonne globale.
    /// </remarks>
    public void zoomer_2(float valeurZoom)
    {
        camera.transform.Translate(new Vector3(0, 0, valeurZoom));
        Vector3 posLocal = Quaternion.Inverse(Quaternion.Euler(-SensibiliteRotY * rotY, SensibiliteRotX * rotX, 0)) * camera.transform.position; // permet d'affecter la rotation nécessaire pour avoir les coordonnees locales.
        
        // ci dessous le code s'assurant que l'on ne dépasse pas, permet d'eviter de rester bloquer si on sort de la plage de valeur et de simplifier le calcul de la nouvelle coordonee
        
        if (posLocal.z < ZoomMin)
        {
            camera.transform.Translate(new Vector3(0, 0, -valeurZoom));
        }
        else if (posLocal.z > ZoomMax)
        {
            camera.transform.Translate(new Vector3(0, 0, -valeurZoom));
        }
    }

    public void zoomer_3(float valeurZoom)
    {
        camera.transform.Translate(new Vector3(0, 0, valeurZoom));
        Vector3 nouveauZoom = transform.localScale + new Vector3(valeurZoom, valeurZoom, valeurZoom);
        if (nouveauZoom.x < ZoomMax)
        {
            if (nouveauZoom.x > ZoomMin)
            {
                transform.localScale = nouveauZoom;
            }
        }
    }

    public void zoomer(float valeurZoom)
    {
        camera.transform.Translate(new Vector3(0, 0, valeurZoom));
        Vector3 posLocal = Quaternion.Inverse(Quaternion.Euler(-SensibiliteRotY * rotY, SensibiliteRotX * rotX, 0)) * camera.transform.position; // permet d'affecter la rotation nécessaire pour avoir les coordonne local.

        // ci dessous le code s'assurant que l'on ne dépasse pas, permet d'eviter de rester bloquer si on sort de la plage de valeur et de simplifier le calcul de la nouvelle coordonee

        if (posLocal.z < ZoomMin)
        {
            camera.transform.Translate(new Vector3(0, 0, -valeurZoom));
        }
        else if (posLocal.z > ZoomMax)
        {
            camera.transform.Translate(new Vector3(0, 0, -valeurZoom));
        }
    }

}
