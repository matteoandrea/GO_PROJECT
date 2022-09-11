﻿using System.Collections;
using UnityEngine;
using static UnityEditor.Progress;

namespace Assets.Script.A.GridLogic
{
    public class Heap<T> where T : IHeapItem<T>
    {
        T[] items;
        int currentItemCount;

        public Heap(int maxHeapSize)
        {
            items = new T[maxHeapSize];
        }

        public void Add(T item)
        {
            item.HeapIndex = currentItemCount;
            items[currentItemCount] = item;
            SortUp(item);
            currentItemCount++;
        }

        public T RemoveFirst()
        {
            var firstItem = items[0];
            currentItemCount--;

            items[0] = items[currentItemCount];
            items[0].HeapIndex = 0;

            SortDown(items[0]);
            return firstItem;
        }

        public void UpdateItem(T item)
        {
            SortUp(item);

        }

        public int Count { get { return currentItemCount; } }

        public bool Contains(T item) { return Equals(items[item.HeapIndex], item); }

        public void SortDown(T item)
        {
            while (true)
            {
                var childIndexLeft = item.HeapIndex * 2 + 1;
                var childIndexRight = item.HeapIndex * 2 + 2;
                var swapIndex = 0;

                if (childIndexLeft < currentItemCount)
                {
                    swapIndex = childIndexLeft;

                    if (childIndexRight < currentItemCount)
                        if (items[childIndexLeft].CompareTo(items[childIndexRight]) < 0) swapIndex = childIndexRight;

                    if (item.CompareTo(items[swapIndex]) < 0) Swap(item, items[swapIndex]);
                    else return;
                }
                else return;
            }
        }

        public void SortUp(T item)
        {
            var parentIndex = (item.HeapIndex - 1) / 2;

            while (true)
            {
                var parentItem = items[parentIndex];

                if (item.CompareTo(parentItem) > 0) Swap(item, parentItem);
                else break;
            }
        }

        public void Swap(T itemA, T itemB)
        {
            items[itemA.HeapIndex] = itemB;
            items[itemB.HeapIndex] = itemA;

            var itemAIndex = itemA.HeapIndex;

            itemA.HeapIndex = itemB.HeapIndex;
            itemB.HeapIndex = itemAIndex;
        }
    }
}