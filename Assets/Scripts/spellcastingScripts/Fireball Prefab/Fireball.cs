using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FireballLifeSpan());
    }

    IEnumerator FireballLifeSpan()
    {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }
}
