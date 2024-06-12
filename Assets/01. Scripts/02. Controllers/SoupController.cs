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
            GameManager.Stone.growingStone.UpdateLoveGage(15);
        }
    }
}

public enum SOUP_TYPE
{
    Hot, Cold
}
