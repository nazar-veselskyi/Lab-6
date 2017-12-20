using System;
using System.Threading;
using System.Timers;

namespace NRVI_LABS_4_6 {
    public enum ContactPhone {
        Phone1,
        Phone2,
        Phone3
    }

    public class CallsProvider {
        public delegate void CallReceivedDelegate(Call call);
        public event CallReceivedDelegate CallReceived;
        
        protected System.Timers.Timer Timer;
        private Thread _messagesThread;

        private void RaiseCallReceivedEvent(Call call) {
            var handler = CallReceived;
            if (handler != null)
                handler(call);
        }

        public void SetUpTimer() {
            Timer = new System.Timers.Timer(1000);
            Timer.Elapsed += OnTimerEvent;
            Timer.AutoReset = true;
            Timer.Enabled = true;
        }

        public void StartTimer() {
            if (_messagesThread == null)
            {
                _messagesThread = new Thread(SetUpTimer);
                _messagesThread.Start();
            }
            else
                Timer.Enabled = true;
        }

        public void StopTimer() {
            Timer.Enabled = false;
        }

        private void OnTimerEvent(Object source, ElapsedEventArgs e) {
            Random rand = new Random();
            CallDirection callDirection = rand.NextDouble() > 0.2 ? CallDirection.Incoming : CallDirection.Outgoing;
            DateTime callTime = DateTime.Now;
            Contact contact;
            Call call;

            double nextDouble = rand.NextDouble();
            
            if (nextDouble < 0.33) {
                contact = new Contact("+380675432523");
                call = new Call(contact, ContactPhone.Phone1, callTime, callDirection);
            }
            else if (nextDouble < 0.66) {
                contact = new Contact("+380631890789", "044556699");
                ContactPhone contactPhone = rand.NextDouble() > 0.4 ? ContactPhone.Phone1 : ContactPhone.Phone2;
                call = new Call(contact, contactPhone, callTime, callDirection);
            }
            else {
                contact = new Contact("+380983335768");
                call = new Call(contact, ContactPhone.Phone1, callTime, callDirection);
            }

            Console.WriteLine(call.GetContactPhone() + " " + callDirection + " " + call.CallTime);

            RaiseCallReceivedEvent(call);
        }
    }
}
