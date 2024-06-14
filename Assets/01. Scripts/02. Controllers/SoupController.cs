using UnityEngine;

public class SoupController : MonoBehaviour
{
    public GameObject targetObject;
    public float duration = 5.0f; // �Ӿ����� �� �ɸ��� �ð� (��)
    private bool startColorChange = false;
    private float elapsedTime = 0f;
    private Color initialColor;
    private Color targetColor = Color.black;
    public SOUP_TYPE type;

    private void Start()
    {
        // �ʱ� ������ �����մϴ�.
        if (targetObject != null)
        {
            initialColor = targetObject.GetComponent<Renderer>().material.color;
        }

        if(type == SOUP_TYPE.Hot)
        {
            targetColor = Color.red;
        }
        else if(type == SOUP_TYPE.Cold)
        {
            targetColor = Color.blue;
        }

        // 10�� �Ŀ� �� ������ �����մϴ�.

        Invoke("UpdateStone", 2.5f);
        Invoke("StartColorChange", 2f);
    }

    private void Update()
    {
        if (startColorChange && targetObject != null)
        {
            // �� ������ ���۵Ǹ� ��� �ð��� ������ŵ�ϴ�.
            elapsedTime += Time.deltaTime;

            // ���� ���������� �����մϴ�.
            float t = Mathf.Clamp01(elapsedTime / duration);
            targetObject.GetComponent<Renderer>().material.color = Color.Lerp(initialColor, targetColor, t);
        }
    }

    private void StartColorChange()
    {
        startColorChange = true;
    }

    private void UpdateStone()
    {
        if(GameManager.Stone.growingStone != null)
        {
            Stone gStone = GameManager.Stone.growingStone;
            switch(gStone.stoneStat.StoneType)
            {
                case STONE_TYPE.LimeStone:
                    if (type == SOUP_TYPE.Cold) gStone.UpdateLoveGage(15);
                    else if (type  == SOUP_TYPE.Hot) gStone.UpdateLoveGage(10);
                    break;
                case STONE_TYPE.Granite:
                    if (type == SOUP_TYPE.Cold) gStone.UpdateLoveGage(10);
                    else if (type == SOUP_TYPE.Hot) gStone.UpdateLoveGage(15);
                    break;
                default:
                    break;
            }
        }
    }
}

public enum SOUP_TYPE
{
    Hot, Cold
}
