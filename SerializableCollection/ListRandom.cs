namespace SerializableCollection
{
    public class ListRandom
    {

        /// <summary>
        /// Хранит первый элемент списка.
        /// </summary>
        public ListNode Head;

        /// <summary>
        /// Хранит последний элемент списка.
        /// </summary>
        public ListNode Tail;

        /// <summary>
        /// Хранит число элементов списка.
        /// </summary>
        public int Count;

        /// <summary>
        /// Производит сериализацию коллекции в поток.
        /// </summary>
        /// <param name="s">Поток в который происходит запись.</param>
        public void Serialize(Stream s)
        {
            var dictionary = new Dictionary<ListNode, int>();
            var id = 0;
            for (ListNode currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                dictionary.Add(currentNode, id);
                id++;
            }
            using (BinaryWriter writer = new BinaryWriter(s))
            {
                for (ListNode currentNode = Head; currentNode != null; currentNode = currentNode.Next)
                {
                    writer.Write(currentNode.Data);
                    writer.Write(dictionary[currentNode.Random]);
                }
            }

        }

        /// <summary>
        /// Производит десериализацию коллекции из потока.
        /// </summary>
        /// <param name="s">Поток, хранящий сериализованную коллекцию.</param>
        public void Deserialize(Stream s)
        {
            var nodesDataDictionary = new Dictionary<int, Tuple<String, int>>();
            var amount = 0;
            using (BinaryReader binReader = new BinaryReader(s))
            {
                while (binReader.PeekChar() != -1)
                {
                    String data = binReader.ReadString();
                    int randomId = binReader.ReadInt32();
                    nodesDataDictionary.Add(amount, new Tuple<String, int>(data, randomId));
                    amount++;
                }
            }
            Count = amount;
            var maxIndex = amount - 1;
            Head = new ListNode();
            var current = Head;
            for (var i = 0; i < Count; i++)
            {
                current.Data = nodesDataDictionary.ElementAt(i).Value.Item1;
                if (i != maxIndex)
                {
                    current.Next = new ListNode();
                    current.Next.Previous = current;
                    current = current.Next;
                }
                else
                {
                    Tail = current;
                }
            }
            var counter = 0;
            for (ListNode currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                currentNode.Random = GetNodeAt(nodesDataDictionary.ElementAt(counter).Value.Item2);
                counter++;
            }
        }

        /// <summary>
        /// Возвращает элемент коллекции с индексом.
        /// </summary>
        /// <param name="index">индекс искомого элемента.</param>
        /// <returns>Найденный элемент или null.</returns>
        public ListNode GetNodeAt(int index)
        {
            var counter = 0;
            for (ListNode currentNode = Head; currentNode != null; currentNode = currentNode.Next)
            {
                if (counter == index)
                    return currentNode;
                counter++;
            }
            return null;
        }
    }
}