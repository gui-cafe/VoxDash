using UnityEngine;
using UnityEngine.UI;

public class corTextScript : MonoBehaviour
{ 
    [SerializeField] private float velocidadeCores = 0.5f;

    private Text objetoRenderer;

    private float matiz = 0f;

    void Start()
    {
        objetoRenderer = GetComponent<Text>();
    }

    void Update()
    {
        matiz += velocidadeCores * Time.deltaTime;

        if (matiz > 1) matiz -= 1;

        Color novaCor = Color.HSVToRGB(matiz, 1f, 1f);

        objetoRenderer.color = novaCor;
    }
}
