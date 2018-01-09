using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NazarVeselskyi.Equality;

namespace Tests {
    [TestClass]
    public class EqualityTests {
        private readonly Contact _contact1 = new Contact("+380985763454", "+38097753619");
        private readonly Contact _contact2 = new Contact("+380630987667", "+390984058003");
        private readonly DateTime _date1 = new DateTime(1971, 1, 1);
        private readonly DateTime _date2 = new DateTime(1971, 3, 2);
        private const string PhoneNumber = "+380984058003";

        [TestMethod]
        public void TestCallsSorting() {
            List<Call> calls = GenerateList();
            calls.Sort();
            Assert.IsTrue(IsListSorted(calls));
        }

        [TestMethod]
        public void TestCallsSortingAfterAddingNewCall() {
            List<Call> calls = GenerateList();
            calls.Sort();
            Call newCall = new Call(new Contact("+380985763454"), ContactPhone.Phone1, new DateTime(1963, 1, 1), CallDirection.Incoming);
            calls.Add(newCall);
            calls.Sort();
            Assert.IsTrue(IsListSorted(calls));
        }

        [TestMethod]
        public void TestCallsSortingAfterRemovingCall() {
            List<Call> calls = GenerateList();
            calls.Sort();
            calls.Remove(calls[1]);
            calls.Sort();
            Assert.IsTrue(IsListSorted(calls));
        }

        private List<Call> GenerateList() {
            Contact contact = new Contact("+380985763454");
            Call call1 = new Call(contact, ContactPhone.Phone1, new DateTime(1971, 1, 1), CallDirection.Incoming);
            Call call2 = new Call(contact, ContactPhone.Phone1, new DateTime(1975, 1, 1), CallDirection.Incoming);
            Call call3 = new Call(contact, ContactPhone.Phone1, new DateTime(1960, 1, 1), CallDirection.Incoming);
            Call call4 = new Call(contact, ContactPhone.Phone1, new DateTime(1959, 1, 1), CallDirection.Incoming);

            return new List<Call> { call1, call2, call3, call4 };
        }

        private bool IsListSorted(List<Call> calls) {
            Call prevCall = calls[0];
            foreach (Call call in calls) {
                if (prevCall.CompareTo(call) > 0) { return false; }
                prevCall = call;
            }

            return true;
        }

        [TestMethod]
        private void TestCallsEqualityDifferentContacts() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact2, ContactPhone.Phone1, _date1, CallDirection.Incoming);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualityDifferentContactPhone() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact1, ContactPhone.Phone2, _date1, CallDirection.Incoming);

            Assert.IsTrue(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualityDifferentDates() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact1, ContactPhone.Phone1, _date2, CallDirection.Incoming);

            Assert.IsTrue(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualityDifferentCallDirection() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameContacts() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact1, ContactPhone.Phone2, _date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameContactPhone() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact2, ContactPhone.Phone1, _date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameDate() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact2, ContactPhone.Phone2, _date1, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameCallDirection() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact2, ContactPhone.Phone2, _date1, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameContactAndContactPhone() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact1, ContactPhone.Phone1, _date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameContactAndDate() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact1, ContactPhone.Phone2, _date1, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameContactAndCallDirection() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact1, ContactPhone.Phone2, _date2, CallDirection.Incoming);

            Assert.IsTrue(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameContactPhoneAndDate() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Incoming);
            Call call2 = new Call(_contact2, ContactPhone.Phone1, _date1, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameContactPhoneAndCallDirection() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date1, CallDirection.Outgoing);
            Call call2 = new Call(_contact2, ContactPhone.Phone1, _date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameDateAndCallDirection() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date2, CallDirection.Outgoing);
            Call call2 = new Call(_contact2, ContactPhone.Phone2, _date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameCalls() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date2, CallDirection.Outgoing);
            Call call2 = new Call(_contact1, ContactPhone.Phone1, _date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestCallsEqualitySameCall() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call1));
        }

        [TestMethod]
        private void TestCallsEqualityDifferentCalls() {
            Call call1 = new Call(_contact1, ContactPhone.Phone1, _date2, CallDirection.Outgoing);
            Call call2 = new Call(_contact2, ContactPhone.Phone2, _date1, CallDirection.Incoming);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        private void TestContactsEqualitySameContact() {
            Contact contact = new Contact(PhoneNumber);
            Assert.IsTrue(contact.Equals(contact));
        }

        [TestMethod]
        private void TestContactsEqualitySameFirstNumber() {
            Contact contact1 = new Contact(PhoneNumber);
            Contact contact2 = new Contact(PhoneNumber, "+380677536190");

            Assert.IsFalse(contact1.Equals(contact2));
        }

        [TestMethod]
        private void TestContactsEqualitySameFirstNumber2() {
            Contact contact1 = new Contact(PhoneNumber);
            Contact contact2 = new Contact(PhoneNumber, "+380677536190", "+380636104903");

            Assert.IsFalse(contact1.Equals(contact2));
        }

        [TestMethod]
        private void TestContactsEqualitySameNumberAsDifferentPhones() {
            Contact contact1 = new Contact(PhoneNumber);
            Contact contact2 = new Contact("", PhoneNumber);

            Assert.IsFalse(contact1.Equals(contact2));
        }

        [TestMethod]
        private void TestContactsEqualitySameNumberAsDifferentPhones2() {
            Contact contact1 = new Contact(PhoneNumber);
            Contact contact2 = new Contact("", "", PhoneNumber);

            Assert.IsFalse(contact1.Equals(contact2));
        }

        [TestMethod]
        private void TestContactsEqualitySameNumberAsDifferentPhones3() {
            Contact contact1 = new Contact(PhoneNumber);
            Contact contact2 = new Contact(PhoneNumber, "", PhoneNumber);

            Assert.IsFalse(contact1.Equals(contact2));
        }

        [TestMethod]
        private void TestContactsEqualitySameNumberAsDifferentPhones4() {
            Contact contact1 = new Contact(PhoneNumber);
            Contact contact2 = new Contact(PhoneNumber, PhoneNumber, PhoneNumber);

            Assert.IsFalse(contact1.Equals(contact2));
        }
    }
}
