using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewDetector : MonoBehaviour
{
    [SerializeField] private GameObject target;
    public GameObject Target { get { return target; } }
    [SerializeField] private GameObject atkTarget;
    public GameObject AtkTarget { get { return atkTarget; } }
    [SerializeField] private GameObject subTarget;
    public GameObject SubTarget { get { return subTarget; } }

    [SerializeField] private float radiu;
    [SerializeField] private float angle;
    [SerializeField] private float subAngle;
    [SerializeField] private float atkRadiu;
    [SerializeField] private float subRadiu;

    [SerializeField] private LayerMask layerMask;
    [SerializeField] private LayerMask obstacleMask;


    public void FindTarget()
    {
        Collider[] targets = Physics.OverlapSphere(transform.position, radiu, layerMask);

        for(int i = 0; i < targets.Length; i++)
        {
            Vector3 findTarget = (targets[i].transform.position - transform.position).normalized;
            if(Vector3.Dot(transform.forward, findTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
            {
                continue;
            }
            float findTargetRange = Vector3.Distance(transform.position, targets[i].transform.position);
            if(Physics.Raycast(transform.position, findTarget,findTargetRange, obstacleMask))
            {
                continue;
            }

            Debug.DrawRay(transform.position, findTarget * findTargetRange, Color.green);

            target = targets[i].gameObject;
            return;
        }
        target = null;
    }

    public void FindAtkTarget()
    {
        Collider[] atkTargets = Physics.OverlapSphere(transform.position, atkRadiu, layerMask);

        for (int i = 0; i < atkTargets.Length; i++)
        {
            Vector3 findTarget = (atkTargets[i].transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, findTarget) < Mathf.Cos(angle * 0.5f * Mathf.Deg2Rad))
            {
                continue;
            }
            float findTargetRange = Vector3.Distance(transform.position, atkTargets[i].transform.position);
            if (Physics.Raycast(transform.position, findTarget, findTargetRange, obstacleMask))
            {
                continue;
            }

            Debug.DrawRay(transform.position, findTarget * findTargetRange, Color.green);

            atkTarget = atkTargets[i].gameObject;
            return;
        }
        atkTarget = null;
    }

    public void FindSubTarget()
    {
        Collider[] subTargets = Physics.OverlapSphere(transform.position, subRadiu, layerMask);

        for (int i = 0; i < subTargets.Length; i++)
        {
            Vector3 findTarget = (subTargets[i].transform.position - transform.position).normalized;
            if (Vector3.Dot(transform.forward, findTarget) < Mathf.Cos(subAngle * 0.5f * Mathf.Deg2Rad))
            {
                continue;
            }
            float findTargetRange = Vector3.Distance(transform.position, subTargets[i].transform.position);
            if (Physics.Raycast(transform.position, findTarget, findTargetRange, obstacleMask))
            {
                continue;
            }
            subTarget = subTargets[i].gameObject;
            return;
        }
        subTarget = null;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, radiu);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, atkRadiu);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, subRadiu);

        Vector3 lookDir = AngleToDir(transform.eulerAngles.y);
        Vector3 rightDir = AngleToDir(transform.eulerAngles.y + angle * 0.5f);
        Vector3 leftDir = AngleToDir(transform.eulerAngles.y - angle * 0.5f);

        Debug.DrawRay(transform.position, lookDir * radiu, Color.red);
        Debug.DrawRay(transform.position, rightDir * radiu, Color.red);
        Debug.DrawRay(transform.position, leftDir * radiu, Color.red);
    }

    private Vector3 AngleToDir(float angle)
    {
        float radian = angle * Mathf.Deg2Rad;
        return new Vector3(Mathf.Sin(radian), 0, Mathf.Cos(radian));
    }
}
