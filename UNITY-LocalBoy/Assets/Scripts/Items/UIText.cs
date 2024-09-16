using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIText : MonoBehaviour
{
    public static UIText instance;
    private TextMeshProUGUI title;
    private TextMeshProUGUI description;

    [SerializeField]
    private Vector2 enterExit;

    [SerializeField]
    private float enterSpeed;
    [SerializeField]
    private LeanTweenType enter;
    [SerializeField]
    private float exitSpeed;
    [SerializeField]
    private LeanTweenType exit;

    private void Awake()
    {
        /*if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(instance);
        }*/

        title = gameObject.transform.Find("Title").GetComponent<TextMeshProUGUI>();
        description = gameObject.transform.Find("Description").GetComponent<TextMeshProUGUI>();
    }

    public void Show(ItemSO item)
    {
        title.text = item.Name;
        description.text = item.Description;

        transform.localPosition = new Vector3(enterExit.x, 0, 0);

        LTSeq sequence = LeanTween.sequence();


        sequence.append(LeanTween.moveLocalX(gameObject, 0, enterSpeed).setEase(enter));
        sequence.append(LeanTween.moveLocalX(gameObject, enterExit.y,exitSpeed).setEase(exit));

        
    }
}
