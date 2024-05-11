using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayC : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float range;
    [SerializeField] private GameObject gKey;
    [SerializeField] private GameObject reforgeButton;
    [SerializeField] private CinemachineVirtualCamera vcam1;
    [SerializeField] private CinemachineVirtualCamera vcam2;
    

    private RaycastHit hit;
    GameObject outLine;

    public bool isObj = true;

    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * range, Color.red);
        if(Physics.Raycast(transform.position, transform.forward, out hit,range , layerMask ))
        {
            if(isObj)
            {
                gKey.gameObject.SetActive(true);
            }
            if (hit.transform.childCount > 0)
            {
                for (int i = 0; i < hit.transform.childCount; i++)
                {
                    if (hit.transform.GetChild(i).tag == "Outline")
                    {
                        outLine = hit.transform.GetChild(i).gameObject;
                        if(isObj)
                        {
                            outLine.SetActive(true);
                        }
                    }
                    else
                    {
                        continue;
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.G))
            {
                switch (hit.transform.tag)
                {
                    case "Door": hit.transform.GetComponent<Door>().OnOff(); break;
                    case "Anvil": Anvil(); break;
                    case "Enhancement": hit.transform.GetComponent<Shop>().OnOff(); break;
                    case "Cauldron": hit.transform.GetComponent<MixManager>().OnOff(); break;
                    case "AmountSetting": hit.transform.GetComponent<CraftingTable>().OnOff(); break;
                    case "Teleport": hit.transform.GetComponent<Mannequin>().OnOff(); break;
                }
            }
        }
        else
        {
            if(outLine != null)
            {
                outLine.SetActive(false);
                outLine = null;
            }
            gKey.gameObject.SetActive(false);
        }
    }

    private void Anvil()
    {
        vcam2.Priority = 11;
        vcam1.Priority = 10;
        isObj = false;
        outLine.SetActive(false);
        reforgeButton.SetActive(true);
        transform.parent.transform.position = hit.transform.GetComponent<Anvil>().point.transform.position;
        transform.parent.transform.LookAt(hit.transform.GetComponent<Anvil>().transform);
        transform.parent.transform.GetComponent<PlayerControllerA>().enabled = false;
        transform.parent.transform.GetComponent<PlayerControllerA>().animator.SetBool("Walk", false);
        gKey.gameObject.SetActive(false);
    }

    public void BackButton()
    {
        isObj = true;
        vcam2.Priority = 10;
        vcam1.Priority = 11;
        transform.parent.transform.GetComponent<PlayerControllerA>().enabled = true;
        reforgeButton.SetActive(false);
    }
}
