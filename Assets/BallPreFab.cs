using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallPreFab : MonoBehaviour
{
    static float ttl = 3;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayDestroy());
    }

    private IEnumerator DelayDestroy()
    {
        yield return new WaitForSeconds(ttl);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
