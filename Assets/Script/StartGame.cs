using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    public int compteurStart;
    public TMP_Text textCompteur;
    public int nbPNJ;
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
                //y bouge pas

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


    public float generateRandomFloat(System.Random random, int min, int max)
    {
        int val = random.Next(min, max);
        float offset = (float)val;
        return offset;


    }
}
