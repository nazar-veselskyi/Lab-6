﻿using System.Collections.Generic;

namespace NazarVeselskyi.Equality {
    public class CallsStorage {
        public delegate void CallAddedDelegate(Call call);
        public event CallAddedDelegate CallAdded;

        public delegate void CallRemovedDelegate(Call call);
        public event CallRemovedDelegate CallRemoved;

        public List<Call> Calls;

        public CallsStorage() {
            Calls = new List<Call>();
        }

        private void RaiseCallAddedEvent(Call call) {
            var handler = CallAdded;
            handler?.Invoke(call);
        }

        private void RaiseCallRemovedEvent(Call call) {
            var handler = CallRemoved;
            handler?.Invoke(call);
        }

        public void AddCall(Call call) {
            if (Calls.Count > 0 && Calls[0].Equals(call))
                Calls[0].AddToAssosiated(call);
            else {
                Calls.Add(call);
                Calls.Sort();
            }

            RaiseCallAddedEvent(call);
        }

        public void RemoveCall(Call call) {
            if (Calls.Contains(call)) {
                Calls.Remove(call);
                RaiseCallRemovedEvent(call);
            }
        }
    }
}
