using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using URandom = UnityEngine.Random;

public class Utils
{
    public static NormalItem.eNormalType GetRandomNormalType()
    {
        Array values = Enum.GetValues(typeof(NormalItem.eNormalType));
        NormalItem.eNormalType result = (NormalItem.eNormalType)values.GetValue(URandom.Range(0, values.Length));

        return result;
    }

    public static NormalItem.eNormalType GetRandomNormalTypeExcept(NormalItem.eNormalType[] types)
    {
        List<NormalItem.eNormalType> list = Enum.GetValues(typeof(NormalItem.eNormalType)).Cast<NormalItem.eNormalType>().Except(types).ToList();

        int rnd = URandom.Range(0, list.Count);
        NormalItem.eNormalType result = list[rnd];

        return result;
    }

    public static NormalItem.eNormalType GetRandomNormalTypeExceptSmt(NormalItem.eNormalType[] types)
    {
        List<int> ItemCount = GameManager.Instance.ItemCount;
        List<int> indices = Enumerable.Range(0, ItemCount.Count).ToList();

        indices.Sort((i, j) => ItemCount[i].CompareTo(ItemCount[j]));
        // Kết quả của chuỗi trên là 1 chuỗi int tương ứng với NormalItem.eNormalType tăng dần, có 7 giá trị
        // Ví dụ : 2, 1,5,3,6,0,4 => TYPE_THREE có ít nhất, TYPE_FIVE có nhiều nhất
        List<int> value = new();
        int _c = indices.Count;
        // Duyệt qua chuỗi trên để tạo trọng số tăng dần 
        for(int i = 0; i< indices.Count; i++)
        {
            // nếu chứa trong chuỗi except thì bỏ qua
            if (!types.Contains((NormalItem.eNormalType)indices[i]))
            {
                for(int j = 0; j < _c; j++)
                {
                    value.Add(indices[i]);
                }
                // giảm trọng số
                _c--; 
            }
        }
        // kết quả là 1 chuỗi với giá trị nhỏ nhất tồn tại nhiều nhất 
        int rnd = URandom.Range(0, value.Count);
        NormalItem.eNormalType result = (NormalItem.eNormalType)value[rnd];

        return result;
    }
}
