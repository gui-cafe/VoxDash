using System.Collections;
using UnityEngine;

public class pointnaoseiScript : MonoBehaviour
{
    [SerializeField] private float velocidadeInicial = 2.5f;
    [SerializeField] private float aceleracao = 0.5f;
    [SerializeField] private float tempodestruir = 15f;

    public float velocidadeAtual;

    void Start()
    {
        velocidadeAtual = velocidadeInicial;
        StartCoroutine(tempodevida());
    }

    void Update()
    {
        velocidadeAtual += aceleracao * Time.deltaTime;
        transform.Translate(Vector3.forward * velocidadeAtual * Time.deltaTime);
    }

    IEnumerator tempodevida()
    {
        //Debug.Log("destruir");
        yield return new WaitForSeconds(tempodestruir);
        Destroy(gameObject);
    }
}
