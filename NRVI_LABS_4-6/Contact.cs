namespace NRVI_LABS_4_6 {
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
            Contact other = obj as Contact;
            if (other == null)
                return false;

            return other.Phone1 == Phone1 && other.Phone2 == Phone2 && other.Phone3 == Phone3;
        }

        public override int GetHashCode() {
            return Phone1.GetHashCode() ^ Phone2.GetHashCode() ^ Phone3.GetHashCode();
        }
    }
}
