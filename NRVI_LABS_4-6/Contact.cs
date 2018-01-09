namespace NazarVeselskyi.Equality {
    public class Contact {
        public readonly string Phone1;
        public readonly string Phone2;
        public readonly string Phone3;

        public Contact(string phone1, string phone2 = "", string phone3 = "") {
            Phone1 = phone1;
            Phone2 = phone2;
            Phone3 = phone3;
        }

        public override bool Equals(object obj) {
            if (!(obj is Contact other))
                return false;

            return other.Phone1 == Phone1 && other.Phone2 == Phone2 && other.Phone3 == Phone3;
        }

        public override int GetHashCode() {
            return Phone1.GetHashCode() ^ Phone2.GetHashCode() ^ Phone3.GetHashCode();
        }
    }
}
