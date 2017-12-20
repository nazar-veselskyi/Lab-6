using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NRVI_LABS_4_6;

namespace Tests {
    [TestClass]
    public class Lab6Tests {
        [TestMethod]
        public void TestSortingCalls() {
            Contact contact = new Contact("+380985763454");
            Call call1 = new Call(contact, ContactPhone.Phone1, new DateTime(1971, 1, 1), CallDirection.Incoming);
            Call call2 = new Call(contact, ContactPhone.Phone1, new DateTime(1975, 1, 1), CallDirection.Incoming);
            Call call3 = new Call(contact, ContactPhone.Phone1, new DateTime(1960, 1, 1), CallDirection.Incoming);
            Call call4 = new Call(contact, ContactPhone.Phone1, new DateTime(1959, 1, 1), CallDirection.Incoming);

            List<Call> calls = new List<Call> {call1, call2, call3, call4};

            calls.Sort();
            Call prevCall = calls[0];

            foreach (Call call in calls) {
                Assert.IsTrue(prevCall.CompareTo(call) <= 0);
                prevCall = call;
            }

            Call call5 = new Call(contact, ContactPhone.Phone1, new DateTime(1963, 1, 1), CallDirection.Incoming);
            calls.Add(call5);
            calls.Sort();

            prevCall = calls[0];
            foreach (Call call in calls) {
                Assert.IsTrue(prevCall.CompareTo(call) <= 0);
                prevCall = call;
            }

            calls.Remove(call2);
            calls.Sort();

            prevCall = calls[0];
            foreach (Call call in calls) {
                Assert.IsTrue(prevCall.CompareTo(call) <= 0);
                prevCall = call;
            }
        }

        [TestMethod]
        public void TestCallsEquality() {
            TestEquality(new Contact("+380985763454", "+38097753619"), new Contact("+380630987667", "+390984058003"), 
                new DateTime(1971, 1, 1), new DateTime(1975, 1, 1));

            TestEquality(new Contact("", "+38097753619"), new Contact("+380630987667"),
                new DateTime(1971, 1, 1), new DateTime(1975, 1, 1));

            TestEquality(new Contact("+38098953619"), new Contact("+380630007667"),
                new DateTime(1971, 2, 3), new DateTime(1971, 3, 2));
        }

        private void TestEquality(Contact contact1, Contact contact2, DateTime date1, DateTime date2) {
            Call call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//different contacts
            Call call2 = new Call(contact2, ContactPhone.Phone1, date1, CallDirection.Incoming);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//different contact phone
            call2 = new Call(contact1, ContactPhone.Phone2, date1, CallDirection.Incoming);

            Assert.IsTrue(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//different dates
            call2 = new Call(contact1, ContactPhone.Phone1, date2, CallDirection.Incoming);

            Assert.IsTrue(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//different call direction
            call2 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//same contacts
            call2 = new Call(contact1, ContactPhone.Phone2, date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//same contact phone
            call2 = new Call(contact2, ContactPhone.Phone1, date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//same date
            call2 = new Call(contact2, ContactPhone.Phone2, date1, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//same call direction
            call2 = new Call(contact2, ContactPhone.Phone2, date2, CallDirection.Incoming);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//same contact and contact phone
            call2 = new Call(contact1, ContactPhone.Phone1, date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//same contact and date
            call2 = new Call(contact1, ContactPhone.Phone2, date1, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//same contact and call direction
            call2 = new Call(contact1, ContactPhone.Phone2, date2, CallDirection.Incoming);

            Assert.IsTrue(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Incoming);//same contact phone and date
            call2 = new Call(contact2, ContactPhone.Phone1, date1, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date1, CallDirection.Outgoing);//same contact phone and call direction
            call2 = new Call(contact2, ContactPhone.Phone1, date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date2, CallDirection.Outgoing);//same date and call direction
            call2 = new Call(contact2, ContactPhone.Phone2, date2, CallDirection.Outgoing);

            Assert.IsFalse(call1.Equals(call2));

            call1 = new Call(contact1, ContactPhone.Phone1, date2, CallDirection.Outgoing);//all the same
            call2 = new Call(contact1, ContactPhone.Phone1, date2, CallDirection.Outgoing);

            Assert.IsTrue(call1.Equals(call2));

            Assert.IsTrue(call1.Equals(call1));//equal to itself

            call1 = new Call(contact1, ContactPhone.Phone1, date2, CallDirection.Outgoing);//everything different
            call2 = new Call(contact2, ContactPhone.Phone2, date1, CallDirection.Incoming);

            Assert.IsFalse(call1.Equals(call2));
        }

        [TestMethod]
        public void TestContactsEquality() {
            string phoneNumber = "+380984058003";

            Contact contact1 = new Contact(phoneNumber);
            Contact contact2 = new Contact(phoneNumber, "+380677536190");
            Contact contact3 = new Contact(phoneNumber, "+380677536190", "+380636104903");
            Contact contact4 = new Contact("", phoneNumber);
            Contact contact5 = new Contact("", "", phoneNumber);
            Contact contact6 = new Contact(phoneNumber, "", phoneNumber);
            Contact contact7 = new Contact(phoneNumber, phoneNumber, phoneNumber);

            Assert.IsTrue(contact1.Equals(contact1));
            Assert.IsFalse(contact1.Equals(contact2));
            Assert.IsFalse(contact1.Equals(contact3));
            Assert.IsFalse(contact1.Equals(contact4));
            Assert.IsFalse(contact1.Equals(contact5));
            Assert.IsFalse(contact1.Equals(contact6));
            Assert.IsFalse(contact1.Equals(contact7));
            Assert.IsFalse(contact2.Equals(contact3));
            Assert.IsFalse(contact2.Equals(contact4));
            Assert.IsFalse(contact2.Equals(contact5));
            Assert.IsFalse(contact2.Equals(contact6));
            Assert.IsFalse(contact2.Equals(contact7));
            Assert.IsFalse(contact3.Equals(contact4));
            Assert.IsFalse(contact3.Equals(contact5));
            Assert.IsFalse(contact3.Equals(contact6));
            Assert.IsFalse(contact3.Equals(contact7));
            Assert.IsFalse(contact4.Equals(contact5));
            Assert.IsFalse(contact4.Equals(contact6));
            Assert.IsFalse(contact4.Equals(contact7));
            Assert.IsFalse(contact5.Equals(contact6));
            Assert.IsFalse(contact5.Equals(contact7));
            Assert.IsFalse(contact6.Equals(contact7));
        }
    }
}
