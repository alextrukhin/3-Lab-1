using System.Text;

namespace lab1db
{
    public class EntitiesBidirectionalList<T>
    {
        private BidirectionalNode<T> list;
        private BidirectionalNode<T> lastNode;
        private int size = 0;
        public int Length => size;
        public EntitiesBidirectionalList() { }
        public EntitiesBidirectionalList(T input) => Push(input);
        public T First => GetValue(0);
        public T End => lastNode.data;
        public void Push(T input) => Insert(size, input);
        public void Insert(int index, T input)
        {
            CheckIndexValid(index, size + 1);
            BidirectionalNode<T> newNode = new(input);
            if (size == 0)
            {
                list = newNode;
                lastNode = newNode;
                size++;
                return;
            }
            if (index == 0)
            {
                newNode.next = list;
                list.prev = newNode;
                list = newNode;
                size++;
                return;
            }
            if (index == size)
            {
                newNode.prev = lastNode;
                lastNode.next = newNode;
                lastNode = newNode;
                size++;
                return;
            }
            BidirectionalNode<T> cur = list;
            int currentIndex = 0;
            while (cur.next != null && currentIndex + 1 != index)
            {
                cur = cur.next;
                currentIndex++;
            }
            newNode.prev = cur;
            newNode.next = cur.next;
            if (cur.next != null) cur.next.prev = newNode;
            cur.next = newNode;
            if (index == size) lastNode = newNode;
            size++;
        }

        public T Delete(int index)
        {
            CheckIndexValid(index, size);
            BidirectionalNode<T> cur = list;
            if (index == 0)
            {
                list = list.next;
                size -= 1;
                return cur.data;
            }
            int currentIndex = 0;
            while (cur.next != null && currentIndex != index)
            {
                cur = cur.next;
                currentIndex++;
            }
            if (cur.prev != null) cur.prev.next = cur.next;
            if (cur.next != null) cur.next.prev = cur.prev;
            size -= 1;
            return cur.data;
        }

        public T Pop() => Delete(size - 1);

        public T GetValue(int index)
        {
            CheckIndexValid(index, size);
            BidirectionalNode<T>? cur;
            if (size / 2 >= index)
            {
                cur = list;
                int currentIndex = 0;
                while (cur.next != null && currentIndex != index)
                {
                    cur = cur.next;
                    currentIndex++;
                }
            }
            else
            {
                cur = lastNode;
                int currentIndex = size - 1;
                while (cur.prev != null && currentIndex != index)
                {
                    cur = cur.prev;
                    currentIndex--;
                }
            }
            return cur.data;
        }

        public void SetValue(int index, T input)
        {
            CheckIndexValid(index, size);
            BidirectionalNode<T> cur = list;
            int currentIndex = 0;
            while (cur.next != null && currentIndex != index)
            {
                cur = cur.next;
                currentIndex++;
            }
            cur.data = input;
        }

        public static void CheckIndexValid(int index, int size)
        {
            if (index < 0 || index >= size) throw new ArgumentOutOfRangeException("Index out of range");
        }

        public override String ToString()
        {
            StringBuilder toReturn = new("Total entities: " + size);

            BidirectionalNode<T> cur = list;
            int currentIndex = 0;
            while (cur != null)
            {
                toReturn.Append("\n" + (currentIndex + 1) + ". " + cur.data.ToString());
                cur = cur.next;
                currentIndex++;
            }

            return toReturn.ToString();
        }

        public T this[int position]
        {
            get => GetValue(position);
            set => SetValue(position, value);
        }
    }
}