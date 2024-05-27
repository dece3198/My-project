using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField] private GameObject destination;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Guest>() != null)
        {
            if (destination != null)
            {
                other.GetComponent<Guest>().agent.SetDestination(destination.transform.position);
            }
            switch (transform.tag)
            {
                case "Bell" : Bell.instance.Add(-1); break;
                case "Exit": GuestGenerator.instance.GuestEnterPool(other.gameObject);break;
            }
        }

        if(other.GetComponent<GuestController>() != null)
        {
            if(other.GetComponent<GuestController>().transform.tag == "Event")
            {
                Destroy(other.GetComponent<GuestController>().gameObject);
            }
            else
            {
                Generator.instance.GuestEnterPool(other.GetComponent<GuestController>().gameObject);
            }
            if(other.GetComponent<GuestController>().buyItem != null)
            {
                other.GetComponent<GuestController>().buyItem.buyType = BuyType.None;
            }
        }
    }
}
