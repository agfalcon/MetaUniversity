using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionManager : MonoBehaviour
{
    private static InteractionManager instance = null;
    public static InteractionManager Instance
    {
        get
        {
            if (instance == null)
                return null;

            return instance;
        }
    }

    public GameObject talkInter;
    public GameObject teleportInter;

    public bool inTalkArea = false;
    public bool inTelePortArea = false;

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

    public void SetInteractionImg()
    {
        if (inTalkArea)
            talkInter.SetActive(true);
        else
            talkInter.SetActive(false);

        if (inTelePortArea)
            teleportInter.SetActive(true);
        else
            teleportInter.SetActive(false);
    }
}
