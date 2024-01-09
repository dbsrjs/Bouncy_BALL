using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public bool test;
    private float time = 0;

    // Update is called once per frame
    void Update()
    {
        if (test == true)
        {
            Debug.Log("true");
            time += Time.deltaTime;
        }


        if (time > 3f)
        {
            test = false;
            time = 0;
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            test = true;
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
}
