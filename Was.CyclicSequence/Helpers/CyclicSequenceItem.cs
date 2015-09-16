namespace Was.CyclicSequence.Helpers
{
    internal class CyclicSequenceItem<T>
    {
        public CyclicSequenceItem(CyclicSequenceItem<T> elem)
        {
            this.Item = elem.Item;
            this.Occurences = elem.Occurences;
            this.Previous = elem.Previous;
        }

        public CyclicSequenceItem(CyclicSequenceItem<T> previous, T item, int occurences)
        {
            Previous = previous;
            Item = item;
            Occurences = occurences;
        }

        public T Item { get; private set; }

        public CyclicSequenceItem<T> Previous { get; set; }

        public int Occurences { get; set; }
    }
}
