using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemStat itemStat;

    public float fadeAmount = 0.1f; // Ŭ���� ������ �پ��� ���� ��

    public void Start()
    {
        gameObject.SetActive(false);
        GameManager.Input.keyAction += PutDownItem;
    }

    private void OnEnable()
    {
        if(itemStat != null) this.GetComponent<SpriteRenderer>().sprite = itemStat.Image;
        else gameObject.SetActive(false);
        GameManager.Input.keyAction -= PutDownItem;
        GameManager.Input.keyAction += PutDownItem;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameManager.Input.mousePosUnity);
        transform.position = GameManager.Input.mousePosUnity;
    }

    public void PutDownItem()
    {
        if(Input.GetMouseButtonDown(1)) 
        { 
            gameObject.SetActive(false);
        }
    }

    void Wash()
    {
        GameManager.Stone.growingStone.UpdateLoveGage(10F);
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(itemStat.ItemID);
        switch (itemStat.ItemID)
        {
            case 0:
                Debug.Log("Moss cleand");
                if (other.gameObject.tag.Equals("Moss"))
                {
                    this.Wash();
                    Debug.Log("Moss cleand");
                    other.gameObject.SetActive(false);
                    this.gameObject.SetActive(false);
                }
                break;
            case 1:
                SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

                if (Input.GetMouseButton(0))
                {
                    if (spriteRenderer != null)
                    {
                        // ���� ���� ��������
                        Color color = spriteRenderer.color;

                        // ���� �� ����
                        color.a -= fadeAmount;

                        // ���� ���� 0���� �۾����� 0���� ����
                        if (color.a <= 0)
                        {
                            color.a = 0;
                            spriteRenderer.color = color;
                            // ������Ʈ ��Ȱ��ȭ
                            gameObject.SetActive(false);
                            this.gameObject.SetActive(false);
                        }
                        else
                        {
                            // ���ο� ���� ����
                            spriteRenderer.color = color;
                        }
                    }
                }
                break;
            default:
                break;
        }  
    }
}
