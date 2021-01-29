using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GiveItem : MonoBehaviour
{

    public GameObject slot;

    // Start is called before the first frame update
    void Start()
    {
        
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
                Debug.Log("ok");
            }else
            {
                Debug.Log("false");
            }
        }
    }

}
