using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBullet : MonoBehaviour
{
   // public GameObject gos;

    

    // Start is called before the first frame update
    void Start()
    {
        FindClosestEnemy();
    }
    public void FindClosestEnemy()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
                var step = 10 * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, closest.transform.position, step);
                transform.LookAt(closest.transform.position);
            }
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
        FindClosestEnemy();

    }

    

}
