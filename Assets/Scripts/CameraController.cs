using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float radius;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private GameObject gKey;
    private RaycastHit hit;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);
        if(Physics.SphereCast(transform.position, radius, transform.TransformDirection(Vector3.forward),out hit, range,layerMask))
        {
            if (hit.transform.GetComponent<Chest>() != null)
            {
                gKey.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.G))
                {
                    hit.transform.GetComponent<Chest>().ChangeState(ChestState.Interaction);
                }
            }

            if(hit.transform.GetComponent<ChestController>() != null)
            {
                gKey.gameObject.SetActive(true);
                if (Input.GetKeyDown(KeyCode.G))
                {
                    if(hit.transform.GetComponent<ChestController>().state == ChestOnOffState.Close)
                    {
                        hit.transform.GetComponent<ChestController>().ChangeState(ChestOnOffState.Open);
                    }
                    else
                    {
                        hit.transform.GetComponent<ChestController>().ChangeState(ChestOnOffState.Close);
                    }
                }
            }
        }
        else
        {
            gKey.gameObject.SetActive(false);
        }
    }

}
