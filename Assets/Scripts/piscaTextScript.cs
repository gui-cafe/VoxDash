using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class piscaTextScript : MonoBehaviour
{
    private Text texto;

    void Start()
    {
        texto = GetComponent<Text>();
        StartCoroutine(PiscaTexto());
    }

    IEnumerator PiscaTexto()
    {
        texto.enabled = false;
        yield return new WaitForSeconds(1f);
        texto.enabled = true;
        yield return new WaitForSeconds(1f);
        StartCoroutine(PiscaTexto());
    }
}
