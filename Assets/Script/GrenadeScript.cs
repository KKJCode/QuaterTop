using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeScript : MonoBehaviour
{
    public GameObject grenades;
    public LayerMask layermask;
    public GameObject[] GtemBox;

    Rigidbody rb;
    //public Enemy enemyH;


    private void Awake()
    {
        rb = FindObjectOfType<Enemy>().GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject hitEffect = Instantiate(grenades, transform.position, transform.rotation);
        Destroy(hitEffect,1f);
        Collider[] collider = Physics.OverlapSphere(transform.position, 2.5f, layermask);
        for (int i = 0; i < collider.Length; i++)
        {
            if (collider[i].tag == "ItemBox")
            {
                Instantiate(GtemBox[0], collider[i].transform.position, Quaternion.identity);
                Destroy(collider[i].transform.gameObject, 1f);
            }
            Vector3 ground = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            ground.y -= 2f;
            Rigidbody enemyRid = collider[i].GetComponent<Rigidbody>();
            enemyRid.AddExplosionForce(50, ground, 10);
            if (collider[i].GetComponent<Enemy>())
            {
                collider[i].GetComponent<Enemy>().OnDamage(100, transform.position, transform.position);
                //StartCoroutine(BoongBoong());
                
            }
        }
        Destroy(gameObject);
    }

    //private IEnumerator BoongBoong()
    //{
    //    rb.GetComponent<Rigidbody>().useGravity = false;
    //    yield return new WaitForSeconds(0.5f);
    //    rb.GetComponent<Rigidbody>().useGravity = true;
    //}
}
