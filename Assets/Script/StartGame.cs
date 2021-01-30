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
            Debug.Log("Partie Finie");
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
            // Si on clique sur le bouton alors isPaused devient faux donc le jeu reprend
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 - 20, 80, 40), "Restart"))
            {
                Application.Quit(); // Ferme le jeu
                Time.timeScale = 1f;
                SceneManager.LoadScene("SampleScene");
            }
            // Si on clique sur le bouton alors on ferme completment le jeu ou on charge la scene Menu Principal
            // Dans le cas du bouton Quitter, il faut augmenter sa position Y pour qu'il soit plus bas.
            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height / 2 + 40, 80, 40), "Quitter"))
            {
                Application.Quit(); // Ferme le jeu
                SceneManager.LoadScene("Menu Principal");
            }
        }
    }
}
