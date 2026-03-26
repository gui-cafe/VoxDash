using System.Collections;
using UnityEngine;

public class playerTouchScript : MonoBehaviour
{
    [Header("Pontos de Referencia")]
    public Transform pontoA;
    public Transform pontoB;
    public Transform pontoC;

    [Header("Pontos de Escape")]
    public Transform escapeEsquerda;
    public Transform escapeDireita;

    [Header("Configurações de Movimento")]
    public float velocidade = 15f;
    public float distanciaVertical = 3f; 
    public float tempoFade = 0.2f;
    public float pausaInvisivel = 0.5f;
    public float limiteSwipe = 50f;

    private Renderer objetoRenderer;
    private Vector3 destino;
    private Vector2 toqueInicial;
    private string pontoAtual = "B";
    private bool estaEmTransicao = false;

    void Start()
    {
        objetoRenderer = GetComponent<Renderer>();
        transform.position = pontoB.position;
        destino = pontoB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, destino, velocidade * Time.deltaTime);

        if (estaEmTransicao)
        {
            return;
        }
        DetectarSwipe();
    }

    void DetectarSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch toque = Input.GetTouch(0);
            if (toque.phase == TouchPhase.Began)
            {
                toqueInicial = toque.position;
            }
            else if (toque.phase == TouchPhase.Ended)
            {
                Vector2 delta = toque.position - toqueInicial;

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (Mathf.Abs(delta.x) > limiteSwipe)
                    {
                        if (delta.x < 0)
                        {
                            ProcessarEsquerda();
                        }
                        else
                        {
                            ProcessarDireita();
                        }
                    }
                }
                else
                {
                    if (Mathf.Abs(delta.y) > limiteSwipe)
                    {
                        if (delta.y > 0)
                        {
                            StartCoroutine(MovimentoVertical(Vector3.up * distanciaVertical));
                        }
                        else
                        {
                            StartCoroutine(MovimentoVertical(Vector3.down * distanciaVertical));
                        }

                    }
                }
            }
        }
    }
    IEnumerator MovimentoVertical(Vector3 direcao)
    {
        estaEmTransicao = true;

        Vector3 posicaoOriginal = destino; 
        destino = posicaoOriginal + direcao;

        while (Vector3.Distance(transform.position, destino) > 0.1f) 
        { 
            yield return null; 
        }

        yield return new WaitForSeconds(0.1f);

        destino = posicaoOriginal;

        while (Vector3.Distance(transform.position, destino) > 0.1f)
        {
            yield return null;
        }

        estaEmTransicao = false;
    }

    void ProcessarEsquerda()
    {
        if (pontoAtual == "B") 
        { 
            destino = pontoA.position; pontoAtual = "A"; 
        }
        else if (pontoAtual == "A")
        { 
            StartCoroutine(SequenciaSaidaEntrada(escapeEsquerda, escapeDireita, pontoC, "C"));
        }
        else if (pontoAtual == "C") 
        { 
            destino = pontoB.position; pontoAtual = "B"; 
        }
    }

    void ProcessarDireita()
    {
        if (pontoAtual == "B")
        {
            destino = pontoC.position; pontoAtual = "C";
        }
        else if (pontoAtual == "C")
        {
            StartCoroutine(SequenciaSaidaEntrada(escapeDireita, escapeEsquerda, pontoA, "A"));
        }
        else if (pontoAtual == "A")
        {
            destino = pontoB.position; pontoAtual = "B";
        }
    }

    IEnumerator SequenciaSaidaEntrada(Transform saida, Transform entrada, Transform destinoFinal, string novoPonto)
    {
        estaEmTransicao = true;
        destino = saida.position;

        StartCoroutine(Fade(1, 0));
        while (Vector3.Distance(transform.position, saida.position) > 0.1f) yield return null;

        yield return new WaitForSeconds(pausaInvisivel);

        transform.position = entrada.position;
        destino = destinoFinal.position;
        pontoAtual = novoPonto;

        yield return StartCoroutine(Fade(0, 1));
        estaEmTransicao = false;
    }

    IEnumerator Fade(float inicio, float fim)
    {
        if (objetoRenderer == null) yield break;
        float tempo = 0;
        Material mat = objetoRenderer.material;
        Color corInicial = mat.color;

        while (tempo < tempoFade)
        {
            tempo += Time.deltaTime;
            float alpha = Mathf.Lerp(inicio, fim, tempo / tempoFade);
            mat.color = new Color(corInicial.r, corInicial.g, corInicial.b, alpha);
            yield return null;
        }
    }
}

