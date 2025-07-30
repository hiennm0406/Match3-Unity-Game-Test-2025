using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public static Pooling Instance;
    public Stack<SpriteRenderer> ItemViewStack = new();

    private void Awake()
    {
        // Dùng singleton để tránh việc phải setup quá nhiều
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PushItem(SpriteRenderer _sr)
    {
        ItemViewStack.Push(_sr);
        _sr.transform.parent = transform;
        _sr.gameObject.SetActive(false);
    }

    public SpriteRenderer PullItem()
    {
        if(ItemViewStack.Count > 0)
        {
            SpriteRenderer Item = ItemViewStack.Pop();
            Item.gameObject.SetActive(true);
            return Item;
        }
        else
        {
            GameObject prefab = Resources.Load<GameObject>(Constants.PREFAB_TYPE);
            if (prefab)
            {
                SpriteRenderer Item = GameObject.Instantiate(prefab).GetComponent<SpriteRenderer>();
                return Item;
            }
        }
        return null;
    }



    void OnDestroy()
    {
        // Giải phóng bộ nhớ
        if (Instance == this) Instance = null;
    }
}
