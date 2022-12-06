using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCNameCotroller : MonoBehaviour
{
    public RectTransform nameBox;
    public RectTransform nameText;

    float[] originSize = new float[4];

    void Awake()
    {
        originSize[0] = nameBox.rect.width;
        originSize[1] = nameBox.rect.height;
        originSize[2] = nameText.anchoredPosition.x;
        originSize[3] = nameText.anchoredPosition.y;
    }

    void Update()
    {
        if(TalkManager.Instance.npc.npcName == "이예림[회장(진)]")
        {
            nameBox.sizeDelta = new Vector2(455, 100);
            nameText.anchoredPosition = new Vector2(245, -68);
        }
        else
        {
            nameBox.sizeDelta = new Vector2(originSize[0], originSize[1]);
            nameText.anchoredPosition = new Vector2(originSize[2], originSize[3]);
        }
    }
}
