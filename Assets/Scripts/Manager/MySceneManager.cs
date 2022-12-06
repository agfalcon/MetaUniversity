using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    private static MySceneManager instance = null;
    public static MySceneManager Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    AsyncOperation asyncLoad;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void LoadScene()
    {
        CharacterUIManager.Instance.SetPlayerMoveAndRotate(false);

        switch (SceneManager.GetActiveScene().buildIndex)
        {
            case 0:
                StartCoroutine(nameof(LoadMyAsyncScene), 1);
                break;
            case 1:
                StartCoroutine(nameof(LoadMyAsyncScene), 0);
                break;
        }
    }

    IEnumerator LoadMyAsyncScene(int index)
    {
        // AsyncOperation�� ���� Scene Load ������ �� �� �ִ�.
        asyncLoad = SceneManager.LoadSceneAsync(index);

        // Scene�� �ҷ����� ���� �Ϸ�Ǹ�, AsyncOperation�� isDone ���°� �ȴ�.
        while (true)
        {
            if (asyncLoad.isDone)
            {
                if (index == 0)
                {
                    PlayerMove.Instance.transform.localScale = new Vector3(2f, 2f, 2f);
                }
                else if (index == 1)
                {
                    PlayerMove.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
                }
                break;
            }

            yield return null;
        }

        CharacterUIManager.Instance.SetPlayerMoveAndRotate(true);
    }
}
