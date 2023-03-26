using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretItem : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            GetComponent<AudioSource>().Play();
            StartCoroutine(Delete());
        }
    }
    private IEnumerator Delete()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<CircleCollider2D>().enabled = false;
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
        StopAllCoroutines();
    }
}
