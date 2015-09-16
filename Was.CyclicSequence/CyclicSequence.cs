namespace Was.CyclicSequence
{
    using Helpers;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public interface ICyclicSequence<T> : IEnumerable<T>
    {
        T Next();
    }

    public class CyclicSequence<T> : ICyclicSequence<T>
    {
        private const int BeginingPos = 0;

        private CyclicSequenceItem<T> _head;
        private CyclicSequenceItem<T> _tail;

        private int _version;

        private readonly IDictionary<CyclicSequenceItem<T>, int> _occurences;

        public CyclicSequence(IEnumerable<T> sequence)
        {
            if (sequence == null) throw new ArgumentNullException("sequence");

            const int defaultOccurence = 1;

            this._occurences = new Dictionary<CyclicSequenceItem<T>, int>();
            this._version = 1;

            var sequenceAsList = sequence as ICollection<T> ?? sequence.ToList();

            foreach (var element in sequenceAsList.Reverse())
            {
                this.Insert(BeginingPos, element, defaultOccurence);
            }
        }

        public T Next()
        {
            if (this._head == null) throw new InvalidOperationException("There is no elements in sequence.");

            var result = this._head.Item;
            this._head.Occurences--;

            if (this._head.Occurences == 0)
            {
                this.SlideSequence();
            }

            return result;
        }

        public IEnumerator<T> GetEnumerator()
        {
            CyclicSequenceItem<T> copiedSequence;
            Dictionary<CyclicSequenceItem<T>, int> newOccurences;

            this.CopySequence(out copiedSequence, out newOccurences);

            return new CyclicSequenceEnumerator<T>(copiedSequence, newOccurences, () => this._version);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        private void SlideSequence()
        {
            if (this._head == null) throw new InvalidOperationException("No sequenece.");
            if (this._head.Occurences != 0)
                throw new InvalidOperationException("Cannot move sequence because all occurences are fetched.");

            if (this._head.Previous == null)
            {
                this._head.Occurences = this._occurences[this._head];
            }
            else
            {
                var oldHead = this._head;
                this._head = this._head.Previous;
                this._tail.Previous = oldHead;
                this._tail = oldHead;
                this._tail.Occurences = this._occurences[this._head];
            }
        }

        private void CopySequence(out CyclicSequenceItem<T> newHead, out Dictionary<CyclicSequenceItem<T>, int> newOccurences)
        {
            newOccurences = new Dictionary<CyclicSequenceItem<T>, int>();

            var traveler = this._head;
            if (traveler == null)
            {
                newHead = null;
                return;
            }

            newHead = new CyclicSequenceItem<T>(traveler);
            newOccurences.Add(newHead, this._occurences[traveler]);

            var newHeadTraveler = newHead;
            traveler = traveler.Previous;

            while (traveler != null)
            {
                newHeadTraveler.Previous = new CyclicSequenceItem<T>(traveler);
                newOccurences.Add(newHeadTraveler.Previous, this._occurences[traveler]);

                traveler = traveler.Previous;
                newHeadTraveler = newHead.Previous;
            }                       
        }

        private void Insert(int position, T element, int occurences)
        {
            try
            {
                if (this._head == null)
                {
                    if (position != 0) throw new ArgumentOutOfRangeException("position");

                    this._head = new CyclicSequenceItem<T>(null, element, occurences);
                    this._tail = this._head;
                    this._occurences.Add(this._head, occurences);

                    return;
                }

                if (position == 0)
                {
                    this._head = new CyclicSequenceItem<T>(this._head, element, occurences);
                    this._occurences.Add(this._head, occurences);
                    return;
                }

                var travel = this._head;
                var counter = 1;

                while (travel.Previous != null && counter < position)
                {
                    travel = travel.Previous;
                }

                if (travel.Previous == null)
                {
                    if (counter + 1 == position)
                    {
                        var newTail = new CyclicSequenceItem<T>(null, element, occurences);
                        this._occurences.Add(newTail, occurences);
                        this._tail.Previous = newTail;
                        this._tail = newTail;
                    }
                    else
                    {
                        throw new ArgumentOutOfRangeException("position");
                    }
                }
                else
                {
                    var newElement = new CyclicSequenceItem<T>(travel.Previous, element, occurences);
                    this._occurences.Add(newElement, occurences);
                    travel.Previous = newElement;
                }
            }
            finally
            {
                unchecked
                {
                    this._version++;
                }
            }
        }
    }
}
