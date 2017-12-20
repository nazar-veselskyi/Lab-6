namespace NRVI_LABS_4_6 {
    public class Mobile {
        private readonly CallsProvider _callsProvider;
        public CallsStorage CallsStorage { get; set; }

        public Mobile() {
            _callsProvider = new CallsProvider();
            _callsProvider.CallReceived += OnCallReceived;

            CallsStorage = new CallsStorage();
        }

        public void StartGeneratingMessages() {
            _callsProvider.StartTimer();
        }

        public void StopGeneratingMessages() {
            _callsProvider.StopTimer();
        }

        private void OnCallReceived(Call call) {
            CallsStorage.AddCall(call);
        }
    }
}
