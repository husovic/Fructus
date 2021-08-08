using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitPart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        float r = Random.value * 2 * Mathf.PI;
        int multiplier = 80;

        if (transform.gameObject.name.Contains("watermelon"))
            multiplier = 4000;
        if (transform.gameObject.name.Contains("coconut"))
            multiplier = 1000;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Cos(r) * multiplier, Mathf.Sin(r) * multiplier));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -multiplier));
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -12)
            Destroy(transform.parent.gameObject);
        
    }

}
