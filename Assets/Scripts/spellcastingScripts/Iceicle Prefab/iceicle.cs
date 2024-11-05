using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class iceicle : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(IceicleLifeSpan());
    }

    IEnumerator IceicleLifeSpan()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyAI>().DealDamage(25);
            Destroy(gameObject);
        }
    }
}
