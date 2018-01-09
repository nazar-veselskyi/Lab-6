using System;
using System.Collections.Generic;

namespace NazarVeselskyi.Equality {
    public enum CallDirection {
        Incoming,
        Outgoing
    }

    public class Call: IComparable {
        public readonly Contact Contact;
        public readonly ContactPhone ContactPhone;
        public DateTime CallTime;
        public readonly CallDirection CallDirection;
        public List<Call> AssosiatedCalls = new List<Call>();

        public Call(Contact contact, ContactPhone contactPhone, DateTime callTime, CallDirection callDirection) {
            Contact = contact;
            ContactPhone = contactPhone;
            CallTime = callTime;
            CallDirection = callDirection;
        }

        public int CompareTo(object obj) {
            if (!(obj is Call other))
                return 1;

            return other.CallTime.CompareTo(CallTime);
        }

        public void AddToAssosiated(Call call) {
            AssosiatedCalls.Add(call);
        }

        public override bool Equals(object obj) {
            if (!(obj is Call other))
                return false;

            return CallDirection == other.CallDirection && Contact.Equals(other.Contact);
        }

        public override int GetHashCode() {
            return Contact.GetHashCode() ^ CallDirection.GetHashCode();
        }

        public string GetCallInfo() {
            string res = CallDirection + ", time: " + CallTime.Hour + ":" + CallTime.Minute + ":" + CallTime.Second;

            foreach (Call assosiatedCall in AssosiatedCalls) {
                res += ", " + assosiatedCall.CallTime.Hour + ":" + assosiatedCall.CallTime.Minute + ":" + assosiatedCall.CallTime.Second;
            }

            return res;
        }

        public string GetContactPhone() {
            switch (ContactPhone) {
                case ContactPhone.Phone1:
                    return Contact.Phone1;

                case ContactPhone.Phone2:
                    return Contact.Phone2;

                case ContactPhone.Phone3:
                    return Contact.Phone3;
            }

            return Contact.Phone1;
        }
    }
}
