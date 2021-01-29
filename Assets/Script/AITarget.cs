using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AITarget : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject target;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null) {
            gameObject.GetComponent<NavMeshAgent>().SetDestination(target.transform.position);
        }
        
    }
}
