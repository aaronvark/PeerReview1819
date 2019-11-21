using System.Collections.Generic;
using System.Linq;

namespace Extensions
{
    public static class LinkedListExtensions
    {
        public static IEnumerable<LinkedListNode<T>> AddRangeFirst<T>(this LinkedList<T> source,
                                            IEnumerable<T> items)
        {
            List<LinkedListNode<T>> nodes = new List<LinkedListNode<T>>();
            foreach(T item in items)
            {
                nodes.Add(source.AddFirst(item));
            }
            return nodes;
        }

        public static void AddRangeFirst<T>(this LinkedList<T> source,
                                            IEnumerable<LinkedListNode<T>> items)
        {
            foreach(LinkedListNode<T> item in items)
            {
                source.AddFirst(item);
            }
        }

        public static IEnumerable<LinkedListNode<T>> AddRangeLast<T>(this LinkedList<T> source,
                                            IEnumerable<T> items)
        {
            List<LinkedListNode<T>> nodes = new List<LinkedListNode<T>>();
            foreach(T item in items)
            {
                nodes.Add(source.AddLast(item));
            }
            return nodes;
        }

        public static void AddRangeLast<T>(this LinkedList<T> source,
                                            IEnumerable<LinkedListNode<T>> items)
        {
            foreach(LinkedListNode<T> item in items)
            {
                source.AddLast(item);
            }
        }

        public static IEnumerable<LinkedListNode<T>> AddRangeAfter<T>(this LinkedList<T> source,
                                            LinkedListNode<T> node,
                                            IEnumerable<T> items)
        {
            List<LinkedListNode<T>> nodes = new List<LinkedListNode<T>>();
            foreach(T item in items.Reverse())
            {
                nodes.Add(source.AddAfter(node, item));
            }
            return nodes;
        }

        public static void AddRangeAfter<T>(this LinkedList<T> source,
                                            LinkedListNode<T> node,
                                            IEnumerable<LinkedListNode<T>> items)
        {
            foreach(LinkedListNode<T> item in items.Reverse())
            {
                source.AddAfter(node, item);
            }
        }

        public static IEnumerable<LinkedListNode<T>> AddRangeBefore<T>(this LinkedList<T> source,
                                            LinkedListNode<T> node,
                                            IEnumerable<T> items)
        {
            List<LinkedListNode<T>> nodes = new List<LinkedListNode<T>>();
            foreach(T item in items)
            {
                nodes.Add(source.AddBefore(node, item));
            }
            return nodes;
        }

        public static void AddRangeBefore<T>(this LinkedList<T> source,
                                            LinkedListNode<T> node,
                                            IEnumerable<LinkedListNode<T>> items)
        {
            foreach(LinkedListNode<T> item in items)
            {
                source.AddBefore(node, item);
            }
        }
    }
}
