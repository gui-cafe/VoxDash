using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class playerPointScript : MonoBehaviour
{

    [Header("Placar Config")]
    [SerializeField] Text pontosUI;
    [SerializeField] Text streakUI;
    [SerializeField] Text timeUI;
    [SerializeField] GameObject particlesPrefab;
    [SerializeField] GameObject goodparticlesPrefab;
    [SerializeField] GameObject badparticlesPrefab;
    public int pontos;
    public int streak;

    [Header("Shake Config")]
    [SerializeField] RectTransform textTransform;
    [SerializeField] Transform cameraTransform;
    public float durationShake = 0.15f;
    public float tamanhoShake = 5.0f;


    private Vector3 textoriginalPosition;
    private Vector3 cameraoriginalPosition;

    private float minutes = 00;
    private float seconds = 00;
    private float miliseconds = 00;

    private bool pegouhpoint = false;

    void Start()
    {
        textoriginalPosition = textTransform.localPosition;
        cameraoriginalPosition = cameraTransform.localPosition;
    }

    void FixedUpdate()
    {
        miliseconds++;
        if (miliseconds >= 60)
        {
            miliseconds = 0;
            seconds++;
        }
        else if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }

        //timeUI.text = (minutes + ":" + seconds + ":" + miliseconds).ToString();
        timeUI.text = ($"{minutes:00}:{seconds:00}:{miliseconds:00}");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ponto"))
        {
            streak++;
            pontos = pontos + 10 * streak;
            //Debug.Log("pontuou");
            Pontuou(particlesPrefab);
            AtualizarStreak();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("pontoalto"))
        {
            streak++;
            pontos = pontos + 50 * streak;
            pegouhpoint = true;
            Pontuou(goodparticlesPrefab);
            AtualizarStreak();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("obstaculo"))
        {
            streak = 0;
            AtualizarStreak();
            Instantiate(badparticlesPrefab, gameObject.transform);
            StartCoroutine(ShakeEffectCamera());
            Destroy(other.gameObject);
        }
    }

    void Pontuou(GameObject particles)
    {
        pontosUI.text = (pontos).ToString();
        Instantiate(particles, gameObject.transform);
        StartCoroutine(ShakeEffect());

    }

    void AtualizarStreak()
    {
        if (streak >= 100)
        {
            streakUI.text = ("STREAK: " + streak + "!!!").ToString();
        }
        else if (streak >= 50)
        {
            streakUI.text = ("STREAK: " + streak + "!!").ToString();
        }
        else if (streak >= 10)
        {
            streakUI.text = ("STREAK: " + streak + "!").ToString();
        }
        else if (streak > 0)
        {
            streakUI.text = ("STREAK: " + streak).ToString();
        }
        else 
        {
            streakUI.text = (":[").ToString();
        }
    }

    private IEnumerator ShakeEffect()
    {
        float rangeTremida = 1.5f;

        if (pegouhpoint)
        {
            rangeTremida = 3.5f;
            pegouhpoint = false;
        }

        float tremidas = 0.0f;

        while (tremidas < durationShake)
        {
            float x = Random.Range(rangeTremida * -1, rangeTremida) * tamanhoShake;
            float y = Random.Range(rangeTremida * -1, rangeTremida) * tamanhoShake;

            textTransform.localPosition = new Vector3(textoriginalPosition.x + x, textoriginalPosition.y + y, textoriginalPosition.z);

            tremidas += Time.deltaTime;
            yield return null;
        }

        textTransform.localPosition = textoriginalPosition;
    }

    private IEnumerator ShakeEffectCamera()
    {
        float rangeTremida = 0.1f;

        float tremidas = 0.0f;

        while (tremidas < durationShake)
        {
            float x = Random.Range(rangeTremida * -1, rangeTremida) * tamanhoShake;
            float y = Random.Range(rangeTremida * -1, rangeTremida) * tamanhoShake;

            cameraTransform.localPosition = new Vector3(cameraoriginalPosition.x + x, cameraoriginalPosition.y + y, cameraoriginalPosition.z);

            tremidas += Time.deltaTime;
            yield return null;
        }

        cameraTransform.localPosition = cameraoriginalPosition;
    }

}
