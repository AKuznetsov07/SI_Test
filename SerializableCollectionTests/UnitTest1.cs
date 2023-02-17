namespace SerializableCollectionTests
{
    using SerializableCollection;
    public class Tests
    {
        /// <summary>
        /// Проверяет в паре сериализацию\десериализацию списка.
        /// </summary>
        [Test]
        public void SeraliztionDeseraliztionPairTest()
        {
            var originalCollection = PrepareRandomList();
            var processedCollection = ProcessCollection(PrepareRandomList());
            Assert.True(IsCollectionsEqual(originalCollection, processedCollection));
        }

        /// <summary>
        /// Проверяет что две коллекции эквивалентны.
        /// </summary>
        /// <param name="firstCollection">Первая коллекция.</param>
        /// <param name="secondCollection">Вторая коллекция.</param>
        /// <returns>true если коллекции эквивалентны, иначе false.</returns>
        private bool IsCollectionsEqual(ListRandom firstCollection, ListRandom secondCollection)
        {
            // TODO: Да, имеет смысл, вынести в ListRandom и определить Equals, но тогда нужно ещё что-то думать с хэшем, а я уже сделал тут. Надеюсь, не критично.
            if (firstCollection.Count != secondCollection.Count)
                return false;
            var Count = firstCollection.Count;

            var firstDictionary = new Dictionary<ListNode, int>();
            var id = 0;
            for (ListNode currentNode = firstCollection.Head; currentNode != null; currentNode = currentNode.Next)
            {
                firstDictionary.Add(currentNode, id);
                id++;
            }

            var secondDictionary = new Dictionary<ListNode, int>();
            id = 0;
            for (ListNode currentNode = secondCollection.Head; currentNode != null; currentNode = currentNode.Next)
            {
                secondDictionary.Add(currentNode, id);
                id++;
            }

            for(int i = 0; i < Count; i++)
            {
                if (i == 9)
                    Console.Write("");

                var firstCollectionNode = firstCollection.GetNodeAt(i);
                var secondCollectionNode = secondCollection.GetNodeAt(i);
                if (!firstCollectionNode.Data.Equals(secondCollectionNode.Data))
                    return false;

                var firstCollectionNodeRandomId = firstDictionary[firstCollectionNode.Random];
                var secondCollectionNodeRandomId = secondDictionary[secondCollectionNode.Random];
                if(firstCollectionNodeRandomId!=secondCollectionNodeRandomId)
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Обрабатывает коллекцию. Проводит сериализацию\десериализацию списка.
        /// </summary>
        /// <param name="collection">Обрабатываемая коллекция.</param>
        /// <returns>Обработанная коллекция.</returns>
        private ListRandom ProcessCollection(ListRandom collection)
        {
            var tempFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "1.txt");

            using (var testStream = File.Create(tempFilePath))
            {
                testStream.Seek(0, SeekOrigin.Begin);
                collection.Serialize(testStream);
            }
            using (var testStream = File.OpenRead(tempFilePath))
            {
                testStream.Seek(0, SeekOrigin.Begin);
                collection.Deserialize(testStream);
            }
            File.Delete(tempFilePath);

            return collection;
        }

        /// <summary>
        /// Подготавливает тестовую коллекцию.
        /// </summary>
        /// <returns>тестовая коллекция.</returns>
        private ListRandom PrepareRandomList()
        {
            var result = new ListRandom();
            var Node1 = new ListNode()
            {
                Data = "Node1"
            };
            var Node2 = new ListNode()
            {
                Previous = Node1,
                Data = "Node2"
            };
            var Node3 = new ListNode()
            {
                Previous = Node2,
                Data = "Node3"
            };
            var Node4 = new ListNode()
            {
                Previous = Node3,
                Data = "Node4"
            };
            var Node5 = new ListNode()
            {
                Previous = Node4,
                Data = "Node5"
            };
            var Node6 = new ListNode()
            {
                Previous = Node5,
                Data = "Node6"
            };
            var Node7 = new ListNode()
            {
                Previous = Node6,
                Data = "Node7"
            };
            var Node8 = new ListNode()
            {
                Previous = Node7,
                Data = "Node8"
            };
            var Node9 = new ListNode()
            {
                Previous = Node8,
                Data = "Node9"
            };
            var Node10 = new ListNode()
            {
                Previous = Node9,
                Data = "Node10"
            };

            Node1.Next = Node2;
            Node2.Next = Node3;
            Node3.Next = Node4;
            Node4.Next = Node5;
            Node5.Next = Node6;
            Node6.Next = Node7;
            Node7.Next = Node8;
            Node8.Next = Node9;
            Node9.Next = Node10;


            Node1.Random = Node9;
            Node2.Random = Node8;
            Node3.Random = Node10;
            Node4.Random = Node6;
            Node5.Random = Node2;
            Node6.Random = Node1;
            Node7.Random = Node5;
            Node8.Random = Node3;
            Node9.Random = Node7;
            Node10.Random = Node4;

            result.Head = Node1;
            result.Tail = Node10;
            result.Count = 10;

            return result;
        }
    }
}