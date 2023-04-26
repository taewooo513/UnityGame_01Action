using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CAttackSlash : MonoBehaviour
{
    public GameObject particleEffect;
    public CPlayerMove cPlayerMove;
    // Start is called before the first frame update
    private void Start()
    {
        cPlayerMove = GameObject.Find("Player")?.GetComponent<CPlayerMove>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            Vector3 dir = other.transform.position - cPlayerMove.transform.position;
            dir = dir.normalized;
            dir.y = 2f;
            other.GetComponent<Rigidbody>()?.AddForce(dir * 50);
        }
    }
}
