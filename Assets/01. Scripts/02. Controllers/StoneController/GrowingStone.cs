using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GrowingStone : MonoBehaviour
{
    [Serialize]
    StoneController controller;
    [SerializeField] GameObject moss;
    public SpriteRenderer spriteRenderer; // ��������Ʈ ������
    public float interval = 10.0f; // ���� �ð� (��)

    // Start is called before the first frame update
    void Start()
    {
        if(GameManager.Stone.growingStone == null)
        {
            Debug.Log("No Stone Choosed");
            this.gameObject.SetActive(false);
        }
        else if(GameManager.Stone.growingStone != null)
        {
            switch (GameManager.Stone.growingStone.stoneStat.StoneType)
            {
                case STONE_TYPE.LimeStone:
                    this.AddComponent<LimeStoneController>();
                    controller = this.GetComponent<LimeStoneController>();
                    controller.stone = GameManager.Stone.growingStone;
                    break;
                case STONE_TYPE.Granite:
                    this.AddComponent<GraniteController>();
                    controller = this.GetComponent<GraniteController>();
                    controller.stone = GameManager.Stone.growingStone;
                    break;
                default:
                    Debug.Log(GameManager.Stone.growingStone);
                    break;
            }
        }
        if(gameObject.GetComponent<SpriteRenderer>() != null)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            if(moss != null) StartCoroutine(Moss());
        }
    }

    IEnumerator Moss()
    {
        while (true)
        {
            // ��������Ʈ�� ũ�� ��������
            Bounds bounds = spriteRenderer.bounds;

            // ��������Ʈ�� ���� ��ġ ���
            Vector3 randomPosition = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                Random.Range(bounds.min.y, bounds.max.y),
                bounds.center.z
            );

            // ���ο� ������Ʈ ����
            GameObject mossObj = Instantiate(moss, randomPosition, Quaternion.identity);
            mossObj.transform.SetParent(transform, false);

            // ���� �ð� ���
            yield return new WaitForSeconds(interval);
        }
    }
}
