using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // Start is called before the first frame update
    //speed variable of 8
    [SerializeField]
    private float _speed = 8.0f;
    //variable at which the height the laser is destroyed at
    [SerializeField]
    private float _height = 8.0f;

    [SerializeField]
    private bool _isEnemyLaser;

    public GameObject _laserPrefab;

    
   

    // Update is called once per frame
    void Update()
    {
        //translate Laser up
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        //destroys laset at a set height

        if(transform.position.y > _height)
        {

            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

         
            Destroy(_laserPrefab);
        }
    }
    public bool IsEnemyLaser()
    {
        return _isEnemyLaser;
    }

}
