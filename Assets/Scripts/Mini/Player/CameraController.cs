using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float radius;
    [SerializeField] LayerMask layerMask;
    [SerializeField] private GameObject gKey;
    private RaycastHit hit;
    GameObject outline;
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * range, Color.red);
        if(Physics.SphereCast(transform.position, radius, transform.TransformDirection(Vector3.forward),out hit, range,layerMask))
        {
            gKey.gameObject.SetActive(true);
            if(hit.transform.childCount > 0)
            {
                for(int i = 0; i < hit.transform.childCount; i++)
                {
                    if(hit.transform.GetChild(i).tag == "Outline")
                    {
                        outline = hit.transform.GetChild(i).gameObject;
                        outline.SetActive(true);
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if(Input.GetKeyDown(KeyCode.G))
            {
                switch (hit.transform.tag)
                {
                    case "Item": ItemInteraction(); break;
                    case "Chest": ChestInteraction(); break;
                    case "ChestMonster": hit.transform.GetComponent<Chest>().ChangeState(ChestState.Interaction); break;
                    case "Door": DoorInteraction(); break;
                    case "Cauldron": hit.transform.GetComponent<MixManager>().OnOff(); break;
                    case "Enhancement": hit.transform.GetComponent<Enhancement>().OnOff(); break;
                    case "AmountSetting": hit.transform.GetComponent<AmountSetting>().OnOff(); break;
                    case "Teleport": hit.transform.GetComponent<Teleport>().OnOff(); break;
                    case "Guest": hit.transform.GetComponent<Guest>().ChangeState(GuestState.Out); break;
                    case "Bell": hit.transform.GetComponent<Bell>().OnOff(); break;
                }
            }
        }
        else
        {

            if(outline != null)
            {
                outline.SetActive(false);
                outline = null;
            }
            
            gKey.gameObject.SetActive(false);
        }
    }


    private void ChestInteraction()
    {
        if (hit.transform.GetComponent<ChestController>().state == ChestOnOffState.Close)
        {
            hit.transform.GetComponent<ChestController>().ChangeState(ChestOnOffState.Open);
        }
        else
        {
            hit.transform.GetComponent<ChestController>().ChangeState(ChestOnOffState.Close);
        }
    }

    private void ItemInteraction()
    {
        Inventory.instance.AcquireItem(hit.transform.GetComponent<ItemPickUp>().item);
        Destroy(hit.transform.gameObject);
    }

    private void DoorInteraction()
    {
        if (hit.transform.GetComponent<Door>().state == DoorOnOffState.Close)
        {
            hit.transform.GetComponent<Door>().ChangeState(DoorOnOffState.Open);
        }
        else
        {
            hit.transform.GetComponent<Door>().ChangeState(DoorOnOffState.Close);
        }
    }
}
