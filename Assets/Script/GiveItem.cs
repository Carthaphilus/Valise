using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GiveItem : MonoBehaviour
{

    public GameObject slot;
    public TMP_Text point;
    private int compteur;

    // Start is called before the first frame update
    void Start()
    {
        compteur = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider colliderPNJ)
    {
        if (colliderPNJ.gameObject.tag == "PNJ" && Input.GetButtonDown("Fire1") && slot.transform.childCount != 0)
        {
            if (colliderPNJ.gameObject.GetInstanceID() == slot.transform.GetChild(0).GetComponent<WaitPnj>().getWaitPnj())
            {
                compteur++;
                Debug.Log("ok");
                point.text = System.Convert.ToString(compteur);
                Destroy(slot.transform.GetChild(0).gameObject);
            }
            else
            {
                Debug.Log("false");
            }
        }
    }

}
