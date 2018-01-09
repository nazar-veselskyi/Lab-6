using System;

namespace NazarVeselskyi.Equality {
    public class Message {
        public string User { get; }
        public string Text { get; }
        public DateTime ReceivingTime { get; }

        public bool Equals(Message message) {
            return message.User == User && message.Text == Text && message.ReceivingTime == ReceivingTime;
        }

        public override bool Equals(object obj) {
            if (!(obj is Message))
                return false;

            return Equals((Message)obj);
        }

        public override int GetHashCode() {
            return User.GetHashCode() ^ Text.GetHashCode() ^ ReceivingTime.GetHashCode();
        }
    }
}
