using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Test
{
    public class Class1 : MonoBehaviour
    {
        List<int> list1 = new List<int>();

        private void Start()
        {
            for (int i = 0; i < 10; i++)
            {
                list1.Add(UnityEngine.Random.Range(0, 100));
            }

            SortList();
            for (int i = 0;i < list1.Count;i++)
                Debug.Log(list1[i]);
        }

        public void SortList()
        {
            for(int i = 0;i < list1.Count;i++)
            {
                for (int j = i; j < list1.Count; j++)
                {
                    if (list1[i] > list1[j])
                    {
                        int tam = list1[i];
                        list1[i] = list1[j];
                        list1[j] = tam;
                    }
                }
            }
        }
    }
}
