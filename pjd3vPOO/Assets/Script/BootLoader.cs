using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BootLoader : MonoBehaviour
{
    private static string initialSceneName;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void Initialize()
    {
        // Armazena o nome da cena que estava aberta
        Scene activeScene = SceneManager.GetActiveScene();
        initialSceneName = activeScene.name;

        // Inicia o processo de carregamento de cenas
        SceneManager.LoadScene("_Boot", LoadSceneMode.Single);
        
        // Aguarda um frame e depois carrega a cena inicial e descarrega o boot
        MonoBehaviour temp = new GameObject("BootLoaderTemp").AddComponent<BootLoaderBehaviour>();
    }

    public static string GetInitialSceneName()
    {
        return initialSceneName;
    }
}

public class BootLoaderBehaviour : MonoBehaviour
{
    private int frameCount = 0;

    private void Update()
    {
        frameCount++;
        
        // Espera 2 frames para garantir que _Boot foi completamente carregada
        if (frameCount == 2)
        {
            // Carrega a cena original de forma aditiva
            SceneManager.LoadScene(BootLoader.GetInitialSceneName(), LoadSceneMode.Additive);
            frameCount++;
        }
        // Espera mais frames para garantir que a cena original foi carregada
        else if (frameCount == 4)
        {
            // Descarrega a cena _Boot
            SceneManager.UnloadSceneAsync("_Boot");
            Destroy(gameObject);
        }
    }
}


