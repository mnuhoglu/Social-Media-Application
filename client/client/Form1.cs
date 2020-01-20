using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace client
{
    public partial class Form1 : Form
    {

        bool terminating = false;
        bool connected = false;
        Socket clientSocket;

        struct Request
        {
            public string sender;
            public string receiver;

            public static bool operator ==(Request c1, Request c2)
            {
                if (c1.sender == c2.sender && c1.receiver == c2.receiver)
                {
                    return true;
                }
                return false;
            }

            public static bool operator !=(Request c1, Request c2)
            {
                if (c1.sender == c2.sender && c1.receiver == c2.receiver)
                {
                    return false;
                }
                return true;
            }
        }

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();

            dis_button.Enabled = false;

            text_send_rq.Enabled = false;
            button_send_rq.Enabled = false;
            button_delete_friend.Enabled = false;
            button1.Enabled = false;

            text_send_ans.Enabled = false;
            button_send_ans.Enabled = false;
            radio_accept.Enabled = false;
            radio_decline.Enabled = false;
            button_show_friends.Enabled = false;
        }

        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        private void button_connect_Click(object sender, EventArgs e)
        {
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            string my_IP = textBox_ip.Text;
            string name = name_text.Text;
            string my_port = textBox_port.Text;
            if (my_IP == "" || my_port == "" || name == "")
            {
                logs.AppendText("You have an empty space.");
            }
            else
            {
                int portNum;
                if (Int32.TryParse(my_port, out portNum))
                {
                    try
                    {
                        clientSocket.Connect(my_IP, portNum);
                        if (name != "" && name.Length <= 64)    //Checking the name if it satisfies the conditions or not.
                        {
                            // Start Encoding Name.
                            Byte[] buffer = new Byte[64];
                            buffer = Encoding.Default.GetBytes(name);
                            clientSocket.Send(buffer);
                            // If we receive confirmation, we are good to go.

                            Byte[] buff = new Byte[64];
                            clientSocket.Receive(buff);
                            string our_name = Encoding.Default.GetString(buff);
                            our_name = our_name.Substring(0, our_name.IndexOf("\0"));


                            if (our_name == "not_approved")     //Checking if our name is not confirmed.
                            {
                                logs.AppendText("Invalid Name\n");
                            }
                            else if (our_name == "same_name")
                            {
                                logs.AppendText("Name already taken\n");

                            }
                            else     //Checking if our name is confirmed.
                            {
                                button_connect.Enabled = false;
                                textBox_message.Enabled = true;
                                button_send.Enabled = true;
                                connected = true;
                                dis_button.Enabled = true;
                                logs.AppendText("Connected to the server!\n");
                                Thread receiveThread = new Thread(Receive);
                                receiveThread.Start();

                                text_send_rq.Enabled = true;
                                button_send_rq.Enabled = true;
                                text_send_ans.Enabled = true;
                                button_send_ans.Enabled = true;
                                radio_decline.Enabled = true;
                                radio_accept.Enabled = true;
                                button1.Enabled = true;
                                button_show_friends.Enabled = true;
                                button_delete_friend.Enabled = true;

                                textBox_ip.Enabled = false;
                                textBox_port.Enabled = false;
                                name_text.Enabled = false;
                            }
                        }

                    }
                    catch
                    {
                        logs.AppendText("Could not connect to the server!\n");
                    }
                }
                else
                {
                    logs.AppendText("Check the port\n");
                }
            }
        }

        private void Receive()
        {
            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    clientSocket.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));

                    if (incomingMessage.Contains("/") == true && incomingMessage.Contains("*") == true && incomingMessage.Contains("-") == true && incomingMessage.Contains("+") == true && incomingMessage.Contains(".") == true)
                    {
                        string my_name = Between(incomingMessage, "+", "-");
                        string friend_name = Between(incomingMessage, "-", "/");
                        string message_offline = Between(incomingMessage, "/", "*");

                        if (incomingMessage.Contains("req_offline") == true)
                        {
                            logs.AppendText( message_offline + "\n");
                        }
                    }
                    else if (incomingMessage.Contains("/") == true && incomingMessage.Contains("*") == true && incomingMessage.Contains("-") == true && incomingMessage.Contains("+") == true)
                    {
                        string my_name = Between(incomingMessage, "+", "-");
                        string friend_name = Between(incomingMessage, "*", "/");
                        if (my_name == name_text.Text)
                        {
                            if (incomingMessage.Contains("accept") == true)
                            {
                                logs.AppendText(friend_name + " has accepted your request. You are now friends.\n");
                            }
                            else
                            {
                                logs.AppendText(friend_name + " declined your request. You are not friends.\n");
                            }
                        }
                        else if (friend_name == name_text.Text)
                        {
                            if (incomingMessage.Contains("accept") == true)
                            {
                                logs.AppendText(my_name + " has accepted your request. You are now friends.\n");
                            }
                            else
                            {
                                logs.AppendText(my_name + " declined your request. You are not friends.\n");
                            }
                        }
                    }
                    else if (incomingMessage.Contains("My Requests are:") == true)
                    {
                        logs_Request.Clear();
                        logs_Request.AppendText(incomingMessage);
                    }
                    else if (incomingMessage.Contains("My Friends are: ") == true)
                    {
                        MessageBox.Show(incomingMessage);
                    }
                    else
                    {
                        if (dis_button.Enabled == true)
                        {
                            if (incomingMessage.Contains("This is an message ->"))
                            {
                                incomingMessage = incomingMessage.Substring(incomingMessage.IndexOf('>') + 1);

                                int index_of = incomingMessage.IndexOf(":");
                                string name_about_to = incomingMessage.Substring(0, index_of);
                                string message = incomingMessage.Substring(index_of);

                                logs.AppendText(name_about_to + message + "\n");
                            }
                            else
                            {
                                logs.AppendText(incomingMessage);
                            }
                        }
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        logs.AppendText("The server has disconnected. \n");
                        button_connect.Enabled = true;
                        textBox_message.Enabled = false;
                        button_send.Enabled = false;
                    }

                    clientSocket.Close();
                    connected = false;
                }

            }


        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            connected = false;
            terminating = true;
            Environment.Exit(0);
        }

        private void button_send_Click(object sender, EventArgs e)
        {
            string message = textBox_message.Text;
            string nmae = name_text.Text;
            if (message == "")
            {
                logs.AppendText("Message can't be empty. \n");
            }
            else
            {
                if (message.Length <= 64)
                {
                    Byte[] buffer = new Byte[64];
                    buffer = Encoding.Default.GetBytes("This is an message ->"+ nmae + ": " + message);
                    clientSocket.Send(buffer);
                }
            }
        }

        private void dis_button_Click(object sender, EventArgs e)
        {
            
            try
            {
                string my_name = name_text.Text;

                connected = false;
                terminating = true;
                button_connect.Enabled = true;

                textBox_message.Enabled = false;
                button_send.Enabled = false;
                dis_button.Enabled = false;

                text_send_rq.Enabled = false;
                button_send_rq.Enabled = false;
                button_delete_friend.Enabled = false;
                button1.Enabled = false;

                text_send_ans.Enabled = false;
                button_send_ans.Enabled = false;
                radio_accept.Enabled = false;
                radio_decline.Enabled = false;
                button_show_friends.Enabled = false;

                textBox_ip.Enabled = true;
                textBox_port.Enabled = true;
                name_text.Enabled = true;

                logs.AppendText("Disconnected to the server!\n");

                Byte[] buffer = new Byte[64];
                buffer = Encoding.Default.GetBytes(my_name + ": disconnected to the server!. \n");
                clientSocket.Send(buffer);

                Byte[] buffer_disc = new Byte[64];
                buffer_disc = Encoding.Default.GetBytes(my_name + ": remove my name. \n");
                clientSocket.Send(buffer_disc);
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void button_send_rq_Click(object sender, EventArgs e)
        {
            String friend_name = text_send_rq.Text;
            String my_name = name_text.Text;


            if (friend_name == "" || friend_name.Length > 64)
            {
                logs.AppendText("Invalid friend name \n");
            }
            else
            {
                //logs.AppendText("You sent a friend request to " + friend_name + "\n");
                Byte[] buffer = new Byte[64];
                string request_str = "req+" + my_name + "-*" + friend_name + "/";
                buffer = Encoding.Default.GetBytes(request_str);
                clientSocket.Send(buffer);
            }

        }

        private void button_send_ans_Click(object sender, EventArgs e)
        {
            String friend_name = text_send_ans.Text;
            String my_name = name_text.Text;

            if (friend_name != "")
            {
                if (radio_accept.Checked == true && radio_decline.Checked == false)
                {
                    Byte[] buffer = new Byte[64];
                    string request_str = "accept+" + my_name + "-*" + friend_name + "/";
                    buffer = Encoding.Default.GetBytes(request_str);
                    clientSocket.Send(buffer);
                }
                else if (radio_decline.Checked == true && radio_accept.Checked == false)
                {
                    Byte[] buffer = new Byte[64];
                    string request_str = "decline+" + my_name + "-*" + friend_name + "/";
                    buffer = Encoding.Default.GetBytes(request_str);
                    clientSocket.Send(buffer);
                }
                else
                {
                    logs.AppendText("You have to accept or decline the request. \n");
                }
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            Byte[] buffer = new Byte[64];
            string request_str = "list_pends:" + name_text.Text + "-";
            buffer = Encoding.Default.GetBytes(request_str);
            clientSocket.Send(buffer);
        }

        private void button_show_friends_Click(object sender, EventArgs e)
        {
            Byte[] buffer = new Byte[64];
            string request_str = "list_friends:" + name_text.Text + "-";
            buffer = Encoding.Default.GetBytes(request_str);
            clientSocket.Send(buffer);
        }

        private void radio_accept_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_decline.Checked == true)
            {
                radio_decline.Checked = false;
                radio_accept.Checked = true;
            }
        }

        private void radio_decline_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_accept.Checked == true)
            {
                radio_accept.Checked = false;
                radio_decline.Checked = true;
            }
        }

        private void button_delete_friend_Click(object sender, EventArgs e)
        {
            String friend_name = text_send_rq.Text;
            String my_name = name_text.Text;

            if (friend_name != "")
            {
                Byte[] buffer = new Byte[64];
                string request_str = "delete+" + my_name + "-*" + friend_name + "/";
                buffer = Encoding.Default.GetBytes(request_str);
                clientSocket.Send(buffer);
            }
            else
            {
                logs.AppendText("This place cannot be empty. \n");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Byte[] buffer = new Byte[64];
            string request_str = "show_me_everbody";
            buffer = Encoding.Default.GetBytes(request_str);
            clientSocket.Send(buffer);
        }
    }
}
