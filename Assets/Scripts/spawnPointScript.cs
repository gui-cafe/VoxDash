using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class spawnPointScript : MonoBehaviour
{
    [SerializeField] private GameObject pointPrefab;
    [SerializeField] private GameObject highpointPrefab;
    [SerializeField] private GameObject obstaculoPrefab;
    [SerializeField] private GameObject wallPrefab;
    [SerializeField] private Transform[] refSpawn; 

    [SerializeField] private float tempoMinimo = 1f;
    [SerializeField] private float tempoMaximo = 3f;
    [SerializeField] private float intervaloaceleracao = 2f;

    private GameObject objetoPrefab;

    void Start()
    {
        StartCoroutine(Spawn());
        StartCoroutine(AcelerarSpawn());
    }

    private void FixedUpdate()
    {

    }

    IEnumerator AcelerarSpawn()
    {
        yield return new WaitForSeconds(intervaloaceleracao);
        if ((tempoMinimo >= 0.1f) && (tempoMaximo >= 1f))
        {
            tempoMinimo = tempoMinimo - 0.01f;
            tempoMaximo = tempoMaximo - 0.01f;
        }

        StartCoroutine(AcelerarSpawn());
    }

    IEnumerator Spawn()
    {
        while (true) 
        {
            int randomSpawn = Random.Range(0, refSpawn.Length);
            Transform pontoEscolhido = refSpawn[randomSpawn];
            float tempoEspera = Random.Range(tempoMinimo, tempoMaximo);
            int definirobjeto = Random.Range(1, 9);

            Vector3 pontoSpawn = new Vector3(pontoEscolhido.position.x, pontoEscolhido.position.y, transform.position.z);

            if (definirobjeto <= 4)
            {
                objetoPrefab = pointPrefab;
            }
            else if (definirobjeto <= 6)
            {
                objetoPrefab = obstaculoPrefab;
            }
            else if (definirobjeto == 7)
            {
                objetoPrefab = highpointPrefab;
                pontoSpawn = new Vector3(pontoEscolhido.position.x, pontoEscolhido.position.y + 3, transform.position.z);
            }
            else if (definirobjeto == 8)
            {
                objetoPrefab = wallPrefab;
                pontoSpawn = new Vector3(0, 0, transform.position.z);
            }
            

                yield return new WaitForSeconds(tempoEspera);


            Instantiate(objetoPrefab, pontoSpawn, objetoPrefab.transform.rotation);
        }
    }


}
