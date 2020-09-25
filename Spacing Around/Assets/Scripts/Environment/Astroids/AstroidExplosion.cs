using System.Collections;
using UnityEngine;

public class AstroidExplosion : MonoBehaviour
{
    public void OnExplosion(float size)
    {
        transform.localScale = new Vector3(transform.localScale.x * size, transform.localScale.y * size, 1);
        StartCoroutine(ExplosionHandler());
    }
    IEnumerator ExplosionHandler()
    {
        GetComponent<ParticleSystem>().Play();
        yield return new WaitForSeconds(GetComponent<ParticleSystem>().main.duration);
        Destroy(gameObject);
    }
}
