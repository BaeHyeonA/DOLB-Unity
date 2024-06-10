using UnityEngine;
using UnityEngine.UI;

public class ShowStoneInfo : MonoBehaviour
{
    public Text objectNameText;  // ������Ʈ �̸��� ǥ���� UI �ؽ�Ʈ
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
        objectNameText.gameObject.SetActive(false);  // ���� �� �ؽ�Ʈ ��Ȱ��ȭ
    }

    void Update()
    {
        Vector2 mousePosition = GameManager.Input.mousePosUnity;
        RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero);

        if (hit.collider != null)
        {
            GameObject hitObject = hit.collider.gameObject;
            if ((StoneController)hitObject.GetComponent<StoneController>() != null)
            {
               StoneController hitStone = (StoneController)hitObject.GetComponent<StoneController>();
               objectNameText.text = 
                    "TYPE: " + hitStone.stone.nickName + "\r\n" /*+
                    "HP: " + hitStone.stone.HP + "\r\n" +
                    "Love:" + hitStone.stone.loveGage + "\r\n" +
                    "Evloution: " + hitStone.stone.nextEvolutionPercentage + "\r\n" +
                    "INFO: " + hitStone.stone.stoneInfo + "\r\n"*/;      
                objectNameText.gameObject.SetActive(true);
                objectNameText.transform.position = Input.mousePosition;  // ���콺 ��ġ �������� �ؽ�Ʈ ��ġ ����
            } else {
                objectNameText.text = hitObject.name;
                objectNameText.gameObject.SetActive(true);
                objectNameText.transform.position = Input.mousePosition;  
            }

        }
        else
        {
            objectNameText.gameObject.SetActive(false);  // �ƹ� ������Ʈ�� �������� ������ �ؽ�Ʈ ��Ȱ��ȭ
        }
    }
}
