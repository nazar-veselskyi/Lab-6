using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace NRVI_LABS_4_6 {
    public partial class CallsForm : Form {
        private readonly Mobile _mobile;

        public CallsForm() {
            InitializeComponent();

            StartButton.Click += OnStartButton;
            StopButton.Click += OnStopButton;

            _mobile = new Mobile();
            _mobile.CallsStorage.CallAdded += OnCallAdded;
            
        }

        private void OnCallAdded(Call call) {
            if (InvokeRequired) {
                Invoke(new CallsStorage.CallAddedDelegate(OnCallAdded), call);
                return;
            }

            ShowCalls(_mobile.CallsStorage.Calls);
        }

        private void ShowCalls(List<Call> calls) {
            CallsListView.Items.Clear();
            foreach (Call call in calls) {
                CallsListView.Items.Add(new ListViewItem(new[] {call.GetContactPhone(), call.GetCallInfo()}));
            }
        }

        private void OnStartButton(object obj, EventArgs eventArgs) {
            _mobile.StartGeneratingMessages();
        }

        private void OnStopButton(object obj, EventArgs eventArgs) {
            _mobile.StopGeneratingMessages();
        }
    }
}
