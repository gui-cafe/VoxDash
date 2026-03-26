using UnityEngine;
using UnityEngine.SceneManagement;

public class changeSceneScript : MonoBehaviour
{
    public void CarregarCena(string nomeDaCena)
    {
        SceneManager.LoadScene(nomeDaCena);
    }
}
