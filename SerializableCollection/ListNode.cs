namespace SerializableCollection
{
    /// <summary>
    /// Узел коллекции.
    /// </summary>
    public class ListNode
    {
        /// <summary>
        /// Хранит предыдущий элемент списка.
        /// </summary>
        public ListNode Previous;

        /// <summary>
        /// Хранит следующий элемент списка.
        /// </summary>
        public ListNode Next;

        /// <summary>
        /// Хранит случайный элемент списка.
        /// </summary>
        public ListNode Random;

        /// <summary>
        /// Хранит данные узла.
        /// </summary>
        public string Data;
    }
}
