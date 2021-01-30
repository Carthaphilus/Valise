using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public int compteurStart;
    public float dureeGameSeconde;
    public int nbJoueur = 0;
    public Transform Joueur;
    public TMP_Text textCompteur;
    public int nbPNJ;
    public int nbValise = 0;
    public Transform PNJhomme;
    public Transform PNJfemme;
    public Transform Valise;
    private bool run = false;

    // Start is called before the first frame update
    void Start()
    {
        /*Dictionary<string, float> config = new Dictionary<string, float>();
        Dictionary<int, Dictionary<string, float>> joueur = new Dictionary<int, Dictionary<string, float>>();
        Dictionary<int, Dictionary<int, Dictionary<string, float>>> nbjoueur = new Dictionary<int, Dictionary<int, Dictionary<string, float>>>();

        //Si il n'a qu'un joueur
        config.Add("x", 0);
        config.Add("Y", 0);
        config.Add("W", 1);
        config.Add("H", 1);
        joueur.Add(0, config);
        nbjoueur.Add(1, joueur);
        config.Clear();
        joueur.Clear();

        //Si il y'en a deux
        config.Add("x", 0);
        config.Add("Y", 0);
        config.Add("W", 0.5F);
        config.Add("H", 1);
        joueur.Add(0, config);
        config.Clear();

        config.Add("x", 0.5F);
        config.Add("Y", 0);
        config.Add("W", 0.5F);
        config.Add("H", 1);
        joueur.Add(1, config);
        nbjoueur.Add(2, joueur);
        config.Clear();
        joueur.Clear();

        //Si il y'en a trois
        config.Add("x", 0);
        config.Add("Y", 0);
        config.Add("W", 0.5F);
        config.Add("H", 0.5F);
        joueur.Add(0, config);
        config.Clear();

        config.Add("x", 0.5F);
        config.Add("Y", 0);
        config.Add("W", 0.5F);
        config.Add("H", 0.5F);
        joueur.Add(1, config);
        config.Clear();

        config.Add("x", 0);
        config.Add("Y", 0);
        config.Add("W", 0.5F);
        config.Add("H", 0.5F);
        joueur.Add(2, config);
        nbjoueur.Add(3, joueur);
        config.Clear();
        joueur.Clear();

        //Si il y'en a quatre
        config.Add("x", 0);
        config.Add("Y", 0);
        config.Add("W", 0.5F);
        config.Add("H", 0.5F);
        joueur.Add(0, config);
        config.Clear();

        config.Add("x", 0.5F);
        config.Add("Y", 0);
        config.Add("W", 0.5F);
        config.Add("H", 0.5F);
        joueur.Add(1, config);
        config.Clear();

        config.Add("x", 0);
        config.Add("Y", 0);
        config.Add("W", 0.5F);
        config.Add("H", 0.5F);
        joueur.Add(2, config);
        config.Clear();

        config.Add("x", 0.5F);
        config.Add("Y", 0.5F);
        config.Add("W", 0.5F);
        config.Add("H", 0.5F);
        joueur.Add(3, config);
        nbjoueur.Add(4, joueur);
        config.Clear();
        joueur.Clear();

        float Jx = -1.011505F;
        float Jz = -1.173645F;

        for (int j = 0; j <= nbJoueur; j++)
        {
            
            float rectX = nbjoueur[nbJoueur][j]["x"];
            float rectY = nbjoueur[nbJoueur][j]["Y"];
            float rectW = nbjoueur[nbJoueur][j]["W"];
            float rectH = nbjoueur[nbJoueur][j]["H"];
            

            Transform gameJoueur;
            Vector3 JoueurVector = new Vector3(Jx, 0.3881412F, Jz);
            gameJoueur = Instantiate(Joueur, JoueurVector, new Quaternion());
            gameJoueur.Find("FreeLookCameraRig").Find("Pivot").Find("Main Camera").GetComponent<Camera>().rect = new Rect(rectX, rectY, rectW, rectH);
            Jx--;
            Jz--;
        }*/

        textCompteur.text = System.Convert.ToString(compteurStart);
    }

    // Update is called once per frame
    void Update()
    {

    }


    IEnumerator startGame()
    {
        yield return new WaitForSeconds(1F);
        compteurStart--;
        textCompteur.text = System.Convert.ToString(compteurStart);

        if (compteurStart == 0)
        {
            textCompteur.text = "C'est partie !";
            yield return new WaitForSeconds(1F);

            for (int x = 0; x <= nbPNJ; x++)
            {
                Transform GeneratePnJ;
                string namePNJ = "PNJ_" + x;
                int genre = Random.Range(1, 3);
                float PNJx = Random.Range(486.21F, 524.92F);
                float PNJz = Random.Range(511.52F, 609.91F);
                Vector3 PNJvector = new Vector3(PNJx, 1F, PNJz);
                NavMeshHit hit;

                //Debug.Log("P:"+x);
                //Debug.Log("Genre:" + genre);
                //Debug.Log("X:" + PNJx);
                //Debug.Log("Z:" + PNJz);

                if (genre == 1)
                {
                    GeneratePnJ = Instantiate(PNJfemme, PNJvector, new Quaternion());
                }
                else
                {
                    GeneratePnJ = Instantiate(PNJhomme, PNJvector, new Quaternion());
                }

                GeneratePnJ.SetParent(GameObject.Find("PNJ").transform);
                GeneratePnJ.name = namePNJ;

                bool walkable = NavMesh.SamplePosition(PNJvector, out hit, 1.0f, NavMesh.AllAreas);

                while (walkable == false)
                {
                    PNJx = Random.Range(486.21F, 524.92F);
                    PNJz = Random.Range(511.52F, 609.91F);
                    PNJvector = new Vector3(PNJx, 1F, PNJz);
                    walkable = NavMesh.SamplePosition(PNJvector, out hit, 1.0f, NavMesh.AllAreas);

                    if (walkable == true)
                    {
                        GeneratePnJ.localPosition = PNJvector;
                    }
                }
                GeneratePnJ.gameObject.SetActive(true);
            }

            if (Valise != null)
            {
                for (int i = 0; i <= nbValise; i++)
                {
                    Transform GenerateValise;
                    string nameValise = "Valise_" + i;
                    float ValiseX = Random.Range(486.21F, 524.92F);
                    float ValiseZ = Random.Range(511.52F, 609.91F);
                    Vector3 ValiseVector = new Vector3(ValiseX, 1F, ValiseZ);
                    NavMeshHit hit;

                    GenerateValise = Instantiate(Valise, ValiseVector, new Quaternion());
                    GenerateValise.SetParent(GameObject.Find("PickAbleItem").transform);
                    GenerateValise.name = nameValise;
                    bool walkable = NavMesh.SamplePosition(ValiseVector, out hit, 1.0f, NavMesh.AllAreas);

                    while (walkable == false)
                    {
                        ValiseX = Random.Range(486.21F, 524.92F);
                        ValiseZ = Random.Range(511.52F, 609.91F);
                        ValiseVector = new Vector3(ValiseX, 1F, ValiseZ);
                        walkable = NavMesh.SamplePosition(ValiseVector, out hit, 1.0f, NavMesh.AllAreas);

                        if (walkable == true)
                        {
                            GenerateValise.localPosition = ValiseVector;
                        }
                    }

                    int PnjNumber = Random.Range(0, nbPNJ);
                    GameObject PNJchoose = GameObject.Find("PNJ_" + PnjNumber);
                    GenerateValise.GetComponent<WaitPnj>().setWaitPnj(PNJchoose.GetInstanceID());
                    GenerateValise.gameObject.SetActive(true);
                }
            }
            StartCoroutine(timeGame());
            //textCompteur.enabled = false;
        }
        else
        {
            StartCoroutine(startGame());
        }
    }

    IEnumerator timeGame()
    {
        yield return new WaitForSeconds(1F);
        dureeGameSeconde --;
        textCompteur.text = System.Convert.ToString(dureeGameSeconde);
        if (dureeGameSeconde <= 0) {
            //textCompteur.enabled = false;
            textCompteur.text = "Partie terminé";
            Time.timeScale = 0f;
        }
        else
        {
            StartCoroutine(timeGame());
        }
            
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && run == false)
        {
            StartCoroutine(startGame());
            run = true;
        }
    }

    void OnGUI()
    {
        
        if (dureeGameSeconde <=0)
        {
            //Rect boxRect = new Rect(425, 100, 300, 300);

            float w = 0.3F; // proportional width (0..1)
            float h = 0.7F; // proportional height (0..1)
            var boxRect = new Rect(0,0,0,0);

            boxRect.x = (Screen.width*(1-w))/2;
            boxRect.y = (Screen.height*(1-h))/2;
            boxRect.width = Screen.width* w;
            boxRect.height = Screen.height* h;

            //GUIStyle style = new GUIStyle();
            //style.fontSize = 32;
            //GUI.Box(boxRect, "Partie Terminé");
            //GUI.Label(new Rect(450, 400, 80, 40),GameObject.Find("Score").GetComponent<TMP_Text>().text);

            // Si on clique sur le bouton alors on quitte l'application relance le temps de jeu et on recharge la scene de jeu
            if (GUI.Button(new Rect(450, 400, 80, 40), "Rejouer"))
            {
                Application.Quit(); // Ferme le jeu
                Time.timeScale = 1f;
                SceneManager.LoadScene("SampleScene");
            }
            // Si on clique sur le bouton alors on ferme completment le jeu ou on charge la scene Menu Principal
            // Dans le cas du bouton Quitter, il faut augmenter sa position Y pour qu'il soit plus bas.
            /*if (GUI.Button(new Rect(630, 400, 80, 40), "Quitter"))
            {
                Application.Quit(); // Ferme le jeu
                SceneManager.LoadScene("Menu Principal");
            }*/
        }
    }
}
