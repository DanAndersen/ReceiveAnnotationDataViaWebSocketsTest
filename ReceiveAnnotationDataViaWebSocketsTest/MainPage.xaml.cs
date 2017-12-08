using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

using WSAUnity;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;
using Windows.UI.Core;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ReceiveAnnotationDataViaWebSocketsTest
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        StarWebrtcContext starWebrtcContext;

        public MainPage()
        {
            this.InitializeComponent();

            starWebrtcContext = StarWebrtcContext.CreateAnnotationReceiverContext();

            // comment these out if not needed
            //Messenger.AddListener<string>(SympleLog.LogTrace, OnLog);
            Messenger.AddListener<string>(SympleLog.LogDebug, OnLog);
            Messenger.AddListener<string>(SympleLog.LogInfo, OnLog);
            Messenger.AddListener<string>(SympleLog.LogError, OnLog);

            Messenger.AddListener<string>(SympleLog.IncomingMessage, OnIncomingMessage);
        }

        private void connectButton_Click(object sender, RoutedEventArgs e)
        {
            connectButton.IsEnabled = false;

            starWebrtcContext.initAndStartWebRTC();
        }

        private void OnLog(string msg)
        {
            Debug.WriteLine(msg);

            // http://stackoverflow.com/questions/19341591/the-application-called-an-interface-that-was-marshalled-for-a-different-thread
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                // Your UI update code goes here!
                logTextBox.Text += msg + "\n";
            }
            );

        }

        private void OnIncomingMessage(string msg)
        {
            Debug.WriteLine(msg);

            // http://stackoverflow.com/questions/19341591/the-application-called-an-interface-that-was-marshalled-for-a-different-thread
            Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,
            () =>
            {
                // Your UI update code goes here!
                incomingMessagesTextBox.Text += msg + "\n";
            }
            );

        }
    }
}
