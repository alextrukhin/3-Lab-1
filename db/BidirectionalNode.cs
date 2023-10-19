namespace lab1db
{
       public class BidirectionalNode<T>
        {
            public T data;
            public BidirectionalNode<T>? prev = null;
            public BidirectionalNode<T>? next = null;
            public BidirectionalNode(T input) => data = input;
            public BidirectionalNode(BidirectionalNode<T> input) => data = input.data;
        }
}
