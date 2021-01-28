using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAbleItem : MonoBehaviour
{

    public GameObject slot;
    public Vector3 VectorP;
    public Vector3 VectorP2;
    private bool picked = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && picked == false)
        {
            PickItem();
            picked = true;
        }
    }

    /// <summary>
    /// Method for picking up item.
    /// </summary>
    private void PickItem()
    {
        Rigidbody Rb = gameObject.GetComponent<Rigidbody>();
        //Vector3 VectorP = new Vector3(0.16F, -0.107F, 0.004F);
        //Vector3 VectorP2 = new Vector3(177.457F, 275.614F, F);

        // Disable rigidbody and reset velocities
        Rb.isKinematic = true;
        Rb.velocity = Vector3.zero;
        Rb.angularVelocity = Vector3.zero;

        // Set Slot as a parent
        gameObject.transform.SetParent(slot.transform);

        // Reset position and rotation
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localEulerAngles = Vector3.zero;

        gameObject.transform.Find("Obj1").GetComponent<MeshCollider>().enabled = false;
        gameObject.transform.Find("Obj1").transform.localPosition = VectorP;
        gameObject.transform.Find("Obj1").transform.localEulerAngles = VectorP2;
    }
}
