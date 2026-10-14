using System;
using System.Collections.Generic;

namespace MunicipalServicesApp.Data
{
    public class PriorityQueue<T>
    {
        // min-heap: lower priority value dequeues first
        private List<(int priority, long seq, T item)> _heap = new List<(int, long, T)>();
        private long _seqCounter = 0; // to preserve insertion order

        public int Count => _heap.Count;

        public void Enqueue(T item, int priority)
        {
            _heap.Add((priority, _seqCounter++, item));
            SiftUp(_heap.Count - 1);
        }

        // Dequeue returns default(T) when empty (caller may check)
        public T Dequeue()
        {
            if (_heap.Count == 0) return default(T);
            var root = _heap[0].item;
            var last = _heap[_heap.Count - 1];
            _heap.RemoveAt(_heap.Count - 1);
            if (_heap.Count > 0)
            {
                _heap[0] = last;
                SiftDown(0);
            }
            return root;
        }

        public T Peek()
        {
            if (_heap.Count == 0) return default(T);
            return _heap[0].item;
        }

        private void SiftUp(int i)
        {
            while (i > 0)
            {
                var p = (i - 1) / 2;
                if (Compare(_heap[i], _heap[p]) >= 0) break; // child >= parent -> ok
                Swap(i, p);
                i = p;
            }
        }

        private void SiftDown(int i)
        {
            int n = _heap.Count;
            while (true)
            {
                int l = 2 * i + 1, r = l + 1, smallest = i;
                if (l < n && Compare(_heap[l], _heap[smallest]) < 0) smallest = l;
                if (r < n && Compare(_heap[r], _heap[smallest]) < 0) smallest = r;
                if (smallest == i) break;
                Swap(i, smallest);
                i = smallest;
            }
        }

        private int Compare((int priority, long seq, T item) a, (int priority, long seq, T item) b)
        {
            // lower priority number = higher urgency -> comes first
            if (a.priority != b.priority) return a.priority.CompareTo(b.priority);
            // tie-break by insertion order (earlier seq comes first)
            return a.seq.CompareTo(b.seq);
        }

        private void Swap(int a, int b)
        {
            var tmp = _heap[a];
            _heap[a] = _heap[b];
            _heap[b] = tmp;
        }
    }
}
