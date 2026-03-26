using Unity.VisualScripting;
using UnityEngine;

public class corPointScript : MonoBehaviour
{
    [SerializeField] private float velocidadeCores = 0.5f;
    [SerializeField] bool VaiGirar = false;
    [SerializeField] float velocidadeRotacao = 100f;
    //[SerializeField] private Renderer trailRenderer;
    private Renderer objetoRenderer;
    
    private float matiz = 0f;

    private Vector3 eixosDeRotacao;

    void Start()
    {

        objetoRenderer = GetComponent<Renderer>();
        //trailRenderer = GetComponent<Renderer>(); ;

        eixosDeRotacao = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f)) * velocidadeRotacao;
    }

    void Update()
    {
        matiz += velocidadeCores * Time.deltaTime;

        if (matiz > 1) matiz -= 1;

        // rodrigo aqui eu tive que ir pelo google mesmo, queria muito fazer esse efeito
        Color novaCor = Color.HSVToRGB(matiz, 1f, 1f);

        objetoRenderer.material.color = novaCor;
        //trailRenderer.material.color = novaCor;

        if (VaiGirar == true)
        {
            transform.Rotate(eixosDeRotacao * Time.deltaTime);
        }
    }
}
