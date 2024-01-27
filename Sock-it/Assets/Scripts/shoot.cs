using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public Transform shootingPoint;
    public GameObject bulletPrefab;


    void Start()
    {

        StartCoroutine(bultdie());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Instantiate(bulletPrefab, shootingPoint.position, transform.rotation);
        }
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }
    IEnumerator bultdie()
    {
        yield return new WaitForSeconds(4);
        Destroy(bulletPrefab);
    }
}
