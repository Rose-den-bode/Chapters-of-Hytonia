using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public float timer = 3f;
    // Start is called before the first frame update

    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer == 1)
        {
            Debug.Log(timer);
        }
        if (timer < 0)
        {
            Debug.Log("Ik werk wel");
            Destroy(gameObject);
        }
    }
}
