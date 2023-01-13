using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BOSS : MonoBehaviour
{
    public GameObject _wave1;
    public GameObject _wave2;
    public GameObject _wave3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Wave1Routine());
    }

IEnumerator Wave1Routine()
    {
        Instantiate(_wave1, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(50.0f);
        Instantiate(_wave2, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(50.0f);
        Instantiate(_wave3, transform.position, Quaternion.identity);
    }
    // Update is called once per frame
    void Update()
    {
      
    }
}
