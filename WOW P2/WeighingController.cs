﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace WOW_P2
{
    internal class WeighingController
    {
        private string ip = "172.16.100.207";
        private int port = 4001;
        public string SocketWeighing(string command)
        {
            string response = "";
            TcpClient client = new TcpClient();
            var result = client.BeginConnect(ip, port, null, null);
            result.AsyncWaitHandle.WaitOne(TimeSpan.FromSeconds(3));

            if (client.Connected)
            {
                StreamWriter writer = new StreamWriter(client.GetStream());
                StreamReader reader = new StreamReader(client.GetStream());
                reader.BaseStream.ReadTimeout = 10000;

                try
                {
                    writer.Write(command + "\r\n");
                    writer.Flush();

                    string readLine = reader.ReadLine();

                    if (command.Equals("T") || command.Equals("S"))
                    {
                        switch (readLine.Substring(2, 1))
                        {
                            case "A":
                                response = SecondLineResponse(reader.ReadLine());
                                break;
                            default:
                                response = "(1) " + readLine;
                                break;
                        }
                    }
                    else if(command.Equals("OT"))
                    {
                        response = readLine.Substring(4, 9).Trim();
                    }
                    else
                    {
                        MessageBox.Show("Comando de báscula no encontrado", "Comando", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    writer.Close();
                    reader.Close();

                }
                catch (Exception ex)
                {
                    response = "EX";
                    MessageBox.Show("Error. " + ex.Message, "Socket Báscula", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    writer.Close();
                    reader.Close();
                    client.Close();
                }
            }
            else
            {
                MessageBox.Show("Báscula no encontrada", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return response;
        }

        /// <summary>
        /// Aplica a comando T y S
        /// </summary>
        /// <param name="secondLineResponse"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        private string SecondLineResponse(string secondLineResponse)
        {
            string response = "";
            MessageBox.Show(secondLineResponse);
            switch (secondLineResponse.Substring(2, 1))
            {
                case "D":
                    response = "OK";
                    break;
                case "v":
                    response = "Comando entendido, pero se ha superado el rango de tara";
                    break;
                case "E":
                    response = "Límite de tiempo superado en espera del resultado estable";
                    break;
                case "I":
                    response = "Comando entendido, pero en el momento no está disponible";
                    break;
                case " ":
                    //Peso bascula
                    response = secondLineResponse.Substring(7,9).Trim();
                    break;
                default:
                    response = "(2) " + secondLineResponse;
                    break;
            }
            return response;
        }

        public static string RemoveLines(string s, int linesToRemove)
        {
            return s.Split(Environment.NewLine.ToCharArray(), linesToRemove + 1).Skip(linesToRemove).FirstOrDefault();
        }

    }
}
