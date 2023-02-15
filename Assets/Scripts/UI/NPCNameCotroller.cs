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
        
    }
}
