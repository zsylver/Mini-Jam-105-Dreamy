using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    [SerializeField]
    string[] scenes;

    [SerializeField]
    bool gameNotPause = true;
    void Awake()
    {
        if (instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;

            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            scenes = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            }
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        for (int i = 0; i < scenes.Length; i++)
        {
            GameObject transitobjname = GameObject.Find(scenes[i]);

            if (transitobjname)
            {
                Button transitobj = transitobjname.transform.GetComponentInParent<Button>();
                transitobj.onClick.AddListener(delegate { ChangeState(transitobjname.name); });
            }
        }
    }

    private GameManager()
    {

    }

    public static GameManager Instance 
    {
        get { 
            return instance;
        }
    }

    void Update()
    {
    }

    public void Pause()
    {
        gameNotPause = false;
        Time.timeScale = 0;
    }
    public void UnPause()
    {
        gameNotPause = true;
        Time.timeScale = 1;
    }
    public bool isPause()
    {
        return gameNotPause;
    }
    public void ChangeState(string name)
    {
        SceneManager.LoadScene(name);
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
