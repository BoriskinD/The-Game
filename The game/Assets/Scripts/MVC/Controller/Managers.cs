using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static AudioManager Audio { get; private set; }

    //Нужно для того чтобы не было куча звуковых менеджеров
    //при переходе между сценами.
    public static Managers instance;

    private List<IGameManager> managers;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Audio = GetComponent<AudioManager>();
        managers = new List<IGameManager> { Audio };
        StartCoroutine(StartupManagers());
    }

    private IEnumerator StartupManagers()
    {
        foreach (IGameManager manager in managers)
            manager.Startup();

        yield return null;

        int numModules = managers.Count;
        int numReady = 0;

        while (numReady < numModules)
        {
            int lastReady = numReady;
            numReady = 0;

            foreach (IGameManager manager in managers)
            {
                if (manager.status == ManagerStatus.Started)
                    numReady++;
            }

            if (numReady > lastReady)
                Debug.Log($"Progress: {numReady}/{numModules}");

            yield return null;
        }
        Debug.Log("All managers started up");
    }
}
