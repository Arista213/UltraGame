using System.Collections.Generic;
using System.Numerics;

namespace General
{
    public class SinglyLinkedList<T>
    {
        public SinglyLinkedList(T value, SinglyLinkedList<T> previous)
        {
            Value = value;
            Previous = previous;
            Length += 1 + previous?.Length ?? 1;
        }

        public SinglyLinkedList<T> Previous { get; }
        public T Value { get; set; }
        public int Length { get; set; }
    }
}