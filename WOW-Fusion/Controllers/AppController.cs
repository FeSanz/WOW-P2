﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WOW_Fusion.Controllers
{
    internal class AppController
    {
        public static void Exit(string message)
        {
            MessageBox.Show(message, "Sin datos suficientes", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Application.Exit();
        }
    }
}
