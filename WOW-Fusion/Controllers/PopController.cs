﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Tulpep.NotificationWindow;

namespace WOW_Fusion
{
    internal class PopController
    {
        frmLoading FormLoading;
        Thread threadLoading;
        PopupNotifier pop = new PopupNotifier();

        public void Show()
        {
            threadLoading = new Thread(new ThreadStart(LoadingProcess));
            threadLoading.Start();
        }
        public void Show(Form parent)
        {
            threadLoading = new Thread(new ParameterizedThreadStart(LoadingProcess));
            threadLoading.Start(parent);
        }

        public void Close()
        {
            if(FormLoading != null)
            {
                FormLoading.BeginInvoke(new ThreadStart(FormLoading.LoadingClose));
                FormLoading = null;
                threadLoading = null;
            }
        }

        private void LoadingProcess()
        {
            FormLoading = new frmLoading();
            FormLoading.ShowDialog();
        }

        private void LoadingProcess(object parent)
        {
            Form parent1 = parent as Form;
            FormLoading = new frmLoading(parent1);
            FormLoading.ShowDialog();
        }

        public void Notifier(string content, Image icon)
        {
            pop.ContentText = content;
            pop.ContentColor = Color.Black;
            pop.ContentFont = new Font("Arial", 14);
            pop.ContentPadding = new Padding(10, 15, 10, 5);
            pop.ShowGrip = false;
            pop.HeaderHeight = 1;
            pop.BorderColor = Color.DarkGray; //Color.FromArgb(35, 35, 35);
            pop.Image = icon;
            pop.ImageSize = new Size(70, 70);
            pop.ImagePadding = new Padding(10);
            pop.Size = new Size(350, 90);
            pop.IsRightToLeft = false;
            pop.Popup();
        }

        public static void Notify(string content, Image icon)
        {
            PopupNotifier pop2 = new PopupNotifier();
            pop2.ContentText = content;
            pop2.ContentColor = Color.Black;
            pop2.ContentFont = new Font("Arial", 14);
            pop2.ContentPadding = new Padding(10, 15, 10, 5);
            pop2.ShowGrip = false;
            pop2.HeaderHeight = 1;
            pop2.BorderColor = Color.DarkGray; //Color.FromArgb(35, 35, 35);
            pop2.Image = icon;
            pop2.ImageSize = new Size(70, 70);
            pop2.ImagePadding = new Padding(10);
            pop2.Size = new Size(350, 90);
            pop2.IsRightToLeft = false;
            pop2.Popup();
        }
    }
}
