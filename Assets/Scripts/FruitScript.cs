using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitScript : MonoBehaviour
{

    private bool invokeStarted;

    [SerializeField]
    private AudioSource cutAudio;

    // Start is called before the first frame update
    void Start()
    {
        invokeStarted = false;
        transform.position = new Vector3(Random.value * 17f - 8.5f, Random.value * 2 + 4f, transform.position.z);
        float r = Random.value * 2 * Mathf.PI;
        int multiplier = 700;

        if (transform.gameObject.name.Contains("watermelon"))
            multiplier = 7000;
        if (transform.gameObject.name.Contains("coconut"))
            multiplier = 1750;
        GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sin(r) * multiplier, 0));
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0,1000));
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.name == "knife")
        {
            var spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
                Destroy(spriteRenderer);

            var coll = GetComponentInChildren<Collider2D>();
            if (coll != null)
                Destroy(coll);

            var rigBody = GetComponentInChildren<Rigidbody2D>();
            if (rigBody != null)
                Destroy(rigBody);

            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(true);

            cutAudio.Play();

            FindObjectOfType<GameManager>().LastCutFruitLocation = transform.position;
            FindObjectOfType<GameManager>().IncrementScore(5);

        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.collider.name == "Background")
        {
            if (!invokeStarted)
            {
                Invoke("RemoveFruit", 0.5f);
                if(FindObjectOfType<GameManager>())
                    FindObjectOfType<GameManager>().DecrementScore(5);
            }
           
        }
    }

    void RemoveFruit()
    {
        Destroy(gameObject);
    }

}