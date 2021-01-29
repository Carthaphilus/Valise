using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public int compteurStart;
    public TMP_Text textCompteur;
    public int nbPNJ;
    public Transform PNJhomme;
    public Transform PNJfemme;
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

            for (int x=0; x<=nbPNJ; x++)
            {
                System.Random random = new System.Random();
                Object GeneratePnJ;
                string namePNJ;
                float genre = Random.Range(1, 2);

                Debug.Log("P:"+x);

                if (genre == 1) {
                    GeneratePnJ = Instantiate(PNJfemme, new Vector3(Random.Range(486.21F, 524.92F), 1F, Random.Range(511.52F, 609.91F)), new Quaternion());
                    namePNJ = "PNJ_Femme_" + x;
                }
                else
                {
                    GeneratePnJ = Instantiate(PNJhomme, new Vector3(Random.Range(486.21F, 524.92F), 1F, Random.Range(511.52F, 609.91F)), new Quaternion());
                    namePNJ = "PNJ_Homme_" + x;
                }

                GeneratePnJ.name = namePNJ;


            }

            textCompteur.enabled = false;
        }
        else
        {
            StartCoroutine(startGame());
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
}
