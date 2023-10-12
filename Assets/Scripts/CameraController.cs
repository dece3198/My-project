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
            if (hit.transform.GetComponent<Chest>().monsterType == MonsterType.Chest)
            {
                gKey.gameObject.SetActive(true);
            }
        }
        else
        {
            gKey.gameObject.SetActive(false);
        }

        if(gKey.activeSelf)
        {
            if(Input.GetKeyDown(KeyCode.G))
            {
                hit.transform.GetComponent<Chest>().ChangeState(ChestState.Interaction);
            }
        }
    }

}
