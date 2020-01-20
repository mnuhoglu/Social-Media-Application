using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace server
{
    public partial class Form1 : Form
    {
        Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> clientSockets = new List<Socket>();

        bool terminating = false;
        bool listening = false;

        String[] names = new String[300];
        List<String> names_include = new List<string>();

        List<Request> friendships = new List<Request>();
        List<Request> pending = new List<Request>();

        List<String> offline_friendship_nots = new List<string>();
        List<String> offline_message_nots = new List<string>();

        string give_my_friend_list(List<Request> requ_list, String str)
        {
            string my_list = "";
            foreach (Request item in requ_list)
            {
                if (item.receiver == str)
                {
                    my_list += item.sender + " \n";
                }
                else if (item.sender == str)
                {
                    my_list += item.receiver + " \n";
                }
            }
            return my_list;
        }

        string give_my_list(List<Request> requ_list, String str)
        {
            string my_list = "";
            foreach (Request item in requ_list)
            {
                if (item.receiver == str)
                {
                    my_list += item.sender + " \n";
                }
            }
            return my_list;
        }

        bool contains_request(List<Request> requ_list, Request requ)
        {
            Request requ_2 = new Request();
            requ_2.receiver = requ.sender;
            requ_2.sender = requ.receiver;
            foreach (Request item in requ_list)
            {
                if (item == requ || item == requ_2)
                {
                    return true;
                }
            }
            return false;
        }

        bool remove_friendship(List<Request> requ_list, Request requ)
        {
            Request requ_2 = new Request();
            requ_2.receiver = requ.sender;
            requ_2.sender = requ.receiver;
            foreach (Request item in requ_list)
            {
                if (item == requ || item == requ_2)
                {
                    requ_list.Remove(item);
                    return true;
                }
            }
            return false;
        }

        bool remove_request(List<Request> requ_list, Request requ)
        {
            foreach (Request item in requ_list)
            {
                if (item == requ)
                {
                    requ_list.Remove(item);
                    return true;
                }
            }
            return false;
        }

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

        public string Between(string STR, string FirstString, string LastString)
        {
            string FinalString;
            int Pos1 = STR.IndexOf(FirstString) + FirstString.Length;
            int Pos2 = STR.IndexOf(LastString);
            FinalString = STR.Substring(Pos1, Pos2 - Pos1);
            return FinalString;
        }

        public Form1()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
            InitializeComponent();


            //  Reading People Name to Check
            int counter = 0;
            string line;

            System.IO.StreamReader file = new System.IO.StreamReader("user_db.txt");
            while ((line = file.ReadLine()) != null)
            {
                names[counter] = line;
                counter++;
            }
            file.Close();

            //Thread pending_th = new Thread(Pending);
            //pending_th.Start();

        }

        private void button_listen_Click(object sender, EventArgs e)
        {
            int serverPort;

            if (Int32.TryParse(textBox_port.Text, out serverPort))
            {
                IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, serverPort);
                serverSocket.Bind(endPoint);

                serverSocket.Listen(300);       //300 since we are going to listen 300 items.

                listening = true;
                button_listen.Enabled = false;
                //textBox_message.Enabled = true;
                //button_send.Enabled = true;

                Thread acceptThread = new Thread(Accept);
                acceptThread.Start();

                logs.AppendText("Started listening on port: " + serverPort + "\n");

            }
            else
            {
                logs.AppendText("Please check port number \n");
            }
        }

        private void Accept()
        {
            while (listening)
            {
                try
                {
                    Socket newClient = serverSocket.Accept();

                    Byte[] our_name = new Byte[64];     //Names length is 64 Bytes.
                    newClient.Receive(our_name);        //Name is received.
                    string new_name = Encoding.Default.GetString(our_name);
                    new_name = new_name.Substring(0, new_name.IndexOf("\0"));


                    if (names.Contains(new_name) == true)    //Checking if the name is in the database or not.
                    {
                        names_include.Add(new_name);

                        clientSockets.Add(newClient);       //Adding the client to socket.
                        logs.AppendText("Client: " + new_name + " is connected.");
                        logs.AppendText(" \n");
                        Thread our_thread = new Thread(Receive);
                        our_thread.Start();
                        string special_approval = "approved";
                        Byte[] our_buffer = Encoding.Default.GetBytes(special_approval);
                        newClient.Send(our_buffer);

                        Byte[] confirm_client_buffer;
                        string pendings_str = "My Requests are: \n" + give_my_list(pending, new_name);
                        confirm_client_buffer = Encoding.Default.GetBytes(pendings_str);
                        newClient.Send(confirm_client_buffer);

                        bool have_message = false;
                        if (offline_message_nots.Count() != 0)
                        {
                            //int num_of_iterate =
                            //for (int i = 0; i < offline_message_nots.Count; i++)
                            //{
                            //    string item = offline_message_nots[i];
                            //    string friend_name = Between(item, "-", "/");

                            //    if (new_name.Equals(friend_name))
                            //    {
                            //        if (have_message == false)
                            //        {
                            //            have_message = true;
                            //            confirm_client_buffer = Encoding.Default.GetBytes("While you were offline these messages sent by your friends. \n");
                            //            newClient.Send(confirm_client_buffer);
                            //        }
                            //        if (item.Contains("req_offline") == true)
                            //        {
                            //            confirm_client_buffer = Encoding.Default.GetBytes(item);
                            //            newClient.Send(confirm_client_buffer);
                            //            offline_message_nots.Remove(item);
                            //        }


                            //    }
                            //}
                            foreach (String item in offline_message_nots.ToList())
                            {
                                string friend_name = Between(item, "-", "/");

                                if (new_name.Equals(friend_name))
                                {
                                    if (have_message == false)
                                    {
                                        have_message = true;
                                        confirm_client_buffer = Encoding.Default.GetBytes("While you were offline these messages sent by your friends. \n");
                                        newClient.Send(confirm_client_buffer);
                                    }
                                    if (item.Contains("req_offline") == true)
                                    {
                                        confirm_client_buffer = Encoding.Default.GetBytes(item);
                                        newClient.Send(confirm_client_buffer);
                                        offline_message_nots.Remove(item);
                                    }


                                }
                            }
                        }
                        if (offline_friendship_nots.Count() != 0)
                        {
                            foreach (String item in offline_friendship_nots.ToList())
                            {
                                string my_name = Between(item, "+", "-");
                                string friend_name = Between(item, "*", "/");

                                if (new_name == my_name || new_name == friend_name)
                                {
                                    if (item.Contains("accept") == true)
                                    {
                                        string request_str = "req_accept+" + my_name + "-*" + friend_name + "/";
                                        confirm_client_buffer = Encoding.Default.GetBytes(request_str);
                                    }
                                    else
                                    {
                                        string request_str = "req_decline+" + my_name + "-*" + friend_name + "/";
                                        confirm_client_buffer = Encoding.Default.GetBytes(request_str);
                                    }

                                    clientSockets.Remove(newClient);       //We are removing this client so, he/she does not send message to himslef/herself.
                                    if (clientSockets.Count() > 0)      //If we have more than 1 client we can send a message.
                                    {
                                        foreach (Socket client in clientSockets)      //We are sending to message to each client one by one except this sender.
                                        {
                                            try
                                            {
                                                client.Send(confirm_client_buffer);
                                            }
                                            catch
                                            {
                                                logs.AppendText("Problem Occured with the connection. \n");
                                                terminating = true;
                                                //textBox_message.Enabled = false;
                                                //button_send.Enabled = false;
                                                textBox_port.Enabled = true;
                                                button_listen.Enabled = true;
                                                serverSocket.Close();

                                            }

                                        }

                                    }
                                    clientSockets.Add(newClient);
                                    offline_friendship_nots.Remove(item);
                                }
                            }
                        }
                    }
                    else    //Name is not in the database.
                    {

                        string special_approval = "not_approved";
                        if (names_include.Contains(new_name) == true)
                        {
                            special_approval = "same_name";
                        }
                        Byte[] our_buffer = Encoding.Default.GetBytes(special_approval);
                        newClient.Send(our_buffer);
                        newClient.Close();
                        logs.AppendText("A client tried to connect with invalid name.\n");
                    }
                }
                catch
                {
                    if (terminating)
                    {
                        listening = false;
                    }
                    else
                    {
                        logs.AppendText("The socket stopped working.\n");
                    }

                }
            }
        }

        private void Receive()
        {
            Socket thisClient = clientSockets[clientSockets.Count() - 1];
            bool connected = true;

            string name_about_to = "";

            while (connected && !terminating)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    thisClient.Receive(buffer);

                    string incomingMessage = Encoding.Default.GetString(buffer);
                    incomingMessage = incomingMessage.Substring(0, incomingMessage.IndexOf("\0"));


                    Byte[] confirm_client_buffer;
                    if (incomingMessage.Contains("/") == true && incomingMessage.Contains("*") == true && incomingMessage.Contains("-") == true && incomingMessage.Contains("+") == true)
                    {
                        string my_name = Between(incomingMessage, "+", "-");
                        string friend_name = Between(incomingMessage, "*", "/");

                        Request pr = new Request();

                        if (incomingMessage.Contains("req"))
                        {

                            pr.sender = my_name;
                            pr.receiver = friend_name;

                            if (names.Contains(my_name) == true && my_name != friend_name && names.Contains(friend_name))
                            {
                                if (contains_request(friendships, pr) == true)
                                {
                                    logs.AppendText("Client: " + my_name + " is already friends with: " + friend_name + ".\n");

                                    confirm_client_buffer = Encoding.Default.GetBytes("Your already friends. \n");
                                    thisClient.Send(confirm_client_buffer);
                                }
                                else if (contains_request(pending, pr) == true)
                                {
                                    logs.AppendText("Client: This Request is been made before. It is between: " + my_name + " and " + friend_name + ".\n");

                                    confirm_client_buffer = Encoding.Default.GetBytes("This request been made before. \n");
                                    thisClient.Send(confirm_client_buffer);
                                }
                                else
                                {
                                    if (contains_request(pending, pr) != true)
                                    {
                                        pending.Add(pr);

                                        logs.AppendText("Client: " + my_name + " has sent a friend request to: " + friend_name + ". \n");

                                        confirm_client_buffer = Encoding.Default.GetBytes("Your friend request sent successfully. \n");
                                        thisClient.Send(confirm_client_buffer);
                                    }
                                    else
                                    {
                                        logs.AppendText("Client: " + my_name + " has already sent a friend request to: " + friend_name + ".\n");

                                        confirm_client_buffer = Encoding.Default.GetBytes("You already sent this request. \n");
                                        thisClient.Send(confirm_client_buffer);
                                    }
                                }
                            }
                            else
                            {
                                logs.AppendText("Client: " + my_name + " tried to send invalid request. \n");
                                confirm_client_buffer = Encoding.Default.GetBytes("You try to send an invalid friend request. \n");
                                thisClient.Send(confirm_client_buffer);
                            }
                        }
                        else if (incomingMessage.Contains("accept"))
                        {
                            pr.sender = friend_name;
                            pr.receiver = my_name;

                            if (contains_request(pending, pr) == true)
                            {

                                bool removing = remove_request(pending, pr);
                                if (removing == true)
                                {
                                    friendships.Add(pr);
                                    logs.AppendText("Client: " + my_name + " and " + friend_name + " are now friends. \n");
                                    confirm_client_buffer = Encoding.Default.GetBytes("You are now friends with " + friend_name + "  \n");
                                    thisClient.Send(confirm_client_buffer);



                                    if (names_include.Contains(friend_name))        //Send to Online person.
                                    {
                                        clientSockets.Remove(thisClient);       //We are removing this client so, he/she does not send message to himslef/herself.
                                        if (clientSockets.Count() > 0)      //If we have more than 1 client we can send a message.
                                        {
                                            string request_str = "req_accept+" + my_name + "-*" + friend_name + "/";
                                            confirm_client_buffer = Encoding.Default.GetBytes(request_str);
                                            foreach (Socket client in clientSockets)      //We are sending to message to each client one by one except this sender.
                                            {
                                                try
                                                {
                                                    client.Send(confirm_client_buffer);
                                                }
                                                catch
                                                {
                                                    logs.AppendText("Problem Occured with the connection. \n");
                                                    terminating = true;
                                                    //textBox_message.Enabled = false;
                                                    //button_send.Enabled = false;
                                                    textBox_port.Enabled = true;
                                                    button_listen.Enabled = true;
                                                    serverSocket.Close();

                                                }

                                            }

                                        }
                                        clientSockets.Add(thisClient);
                                    }
                                    else        //Offline olanlar için queue ayarla
                                    {
                                        string request_str = "req_accept+" + my_name + "-*" + friend_name + "/";
                                        offline_friendship_nots.Add(request_str);
                                    }


                                }
                                else
                                {
                                    confirm_client_buffer = Encoding.Default.GetBytes("Server could not accept the request.  \n");
                                    thisClient.Send(confirm_client_buffer);
                                }
                            }
                            else
                            {
                                confirm_client_buffer = Encoding.Default.GetBytes("You do not have this request.  \n");
                                thisClient.Send(confirm_client_buffer);
                            }

                        }
                        else if (incomingMessage.Contains("decline"))
                        {
                            pr.sender = friend_name;
                            pr.receiver = my_name;

                            if (contains_request(pending, pr) == true)
                            {

                                bool removing = remove_request(pending, pr);
                                if (removing == true)
                                {
                                    logs.AppendText("Client: " + my_name + " and " + friend_name + " are not friends. \n");
                                    confirm_client_buffer = Encoding.Default.GetBytes("You are not friends with " + friend_name + "  \n");
                                    thisClient.Send(confirm_client_buffer);

                                    if (names_include.Contains(friend_name))        //Send to Online person.
                                    {
                                        clientSockets.Remove(thisClient);       //We are removing this client so, he/she does not send message to himslef/herself.
                                        if (clientSockets.Count() > 0)      //If we have more than 1 client we can send a message.
                                        {
                                            string request_str = "req_decline+" + my_name + "-*" + friend_name + "/";
                                            confirm_client_buffer = Encoding.Default.GetBytes(request_str);
                                            foreach (Socket client in clientSockets)      //We are sending to message to each client one by one except this sender.
                                            {
                                                try
                                                {
                                                    client.Send(confirm_client_buffer);
                                                }
                                                catch
                                                {
                                                    logs.AppendText("Problem Occured with the connection. \n");
                                                    terminating = true;
                                                    //textBox_message.Enabled = false;
                                                    //button_send.Enabled = false;
                                                    textBox_port.Enabled = true;
                                                    button_listen.Enabled = true;
                                                    serverSocket.Close();

                                                }

                                            }

                                        }
                                        clientSockets.Add(thisClient);
                                    }
                                    else        //Offline olanlar için queue ayarla
                                    {
                                        string request_str = "req_decline+" + my_name + "-*" + friend_name + "/";
                                        offline_friendship_nots.Add(request_str);
                                    }


                                }
                                else
                                {
                                    confirm_client_buffer = Encoding.Default.GetBytes("Server could not accept the request.  \n");
                                    thisClient.Send(confirm_client_buffer);
                                }
                            }
                            else
                            {
                                confirm_client_buffer = Encoding.Default.GetBytes("You do not have this request.  \n");
                                thisClient.Send(confirm_client_buffer);
                            }
                        }

                        else if (incomingMessage.Contains("delete"))
                        {
                            pr.sender = friend_name;
                            pr.receiver = my_name;

                            if (contains_request(friendships, pr) == true)
                            {

                                bool removing = remove_friendship(friendships, pr);
                                if (removing == true)
                                {
                                    logs.AppendText("Client: " + my_name + " deleted " + friend_name + " from friends. \n");
                                    confirm_client_buffer = Encoding.Default.GetBytes("You are deleted your friendship with " + friend_name + "  \n");
                                    thisClient.Send(confirm_client_buffer);

                                    if (offline_message_nots.Count() != 0)
                                    {
                                        foreach (String item in offline_message_nots.ToList())
                                        {
                                            string name_off_message = Between(item, "+", "-");
                                            string friend_name_off_message = Between(item, "-", "/");
                                            string message_offline = Between(item, "/", ".");

                                            if ( (name_off_message == friend_name && my_name == friend_name_off_message) || (name_off_message == my_name && friend_name == friend_name_off_message) )
                                            {
                                                confirm_client_buffer = Encoding.Default.GetBytes(item);
                                                offline_message_nots.Remove(item);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    confirm_client_buffer = Encoding.Default.GetBytes("Server could not accept the request.  \n");
                                    thisClient.Send(confirm_client_buffer);
                                }
                            }
                            else
                            {
                                confirm_client_buffer = Encoding.Default.GetBytes("You are not friends.  \n");
                                thisClient.Send(confirm_client_buffer);
                            }

                        }
                    }
                    else if (incomingMessage.Contains("list_pend"))
                    {
                        string my_name = Between(incomingMessage, ":", "-");

                        string pendings_str = "My Requests are: \n" + give_my_list(pending, my_name);
                        confirm_client_buffer = Encoding.Default.GetBytes(pendings_str);
                        thisClient.Send(confirm_client_buffer);

                    }
                    else if (incomingMessage.Contains("list_friends"))
                    {
                        string my_name = Between(incomingMessage, ":", "-");

                        string pendings_str = "My Friends are: \n" + give_my_friend_list(friendships, my_name);
                        confirm_client_buffer = Encoding.Default.GetBytes(pendings_str);
                        thisClient.Send(confirm_client_buffer);

                    }
                    else if (incomingMessage.Contains("show_me_everbody"))
                    {

                        string everbody = get_me_everbody();
                        confirm_client_buffer = Encoding.Default.GetBytes(everbody);
                        thisClient.Send(confirm_client_buffer);

                    }
                    else if (incomingMessage.Contains("remove my name"))
                    {
                        if (incomingMessage.Contains(":"))
                        {
                            int index_of = incomingMessage.IndexOf(":");
                            name_about_to = incomingMessage.Substring(0, index_of);
                        }
                        if (!terminating)
                        {
                            logs.AppendText("Client: " + name_about_to + " has disconnected. \n");
                        }

                        if (name_about_to != "")
                        {
                            if (names_include.Contains(name_about_to) == true)
                            {
                                names_include.Remove(name_about_to);
                            }
                        }
                        if (clientSockets.Contains(thisClient))
                        {
                            clientSockets.Remove(thisClient);
                        }
                        thisClient.Close();
                        connected = false;

                    }
                    else
                    {

                        if (incomingMessage.Contains("This is an message ->"))
                        {
                            incomingMessage = incomingMessage.Substring(incomingMessage.IndexOf('>') + 1);

                            int index_of = incomingMessage.IndexOf(":");
                            name_about_to = incomingMessage.Substring(0, index_of);

                            clientSockets.Remove(thisClient);       //We are removing this client so, he/she does not send message to himslef/herself.
                            if (clientSockets.Count() > 0)      //If we have more than 1 client we can send a message.
                            {
                                foreach (Socket client in clientSockets)      //We are sending to message to each client one by one except this sender.
                                {
                                    try
                                    {
                                        logs.AppendText("Client: " + incomingMessage + "\n");
                                        client.Send(buffer);
                                    }
                                    catch
                                    {
                                        logs.AppendText("Problem Occured with the connection. \n");
                                        terminating = true;
                                        //textBox_message.Enabled = false;
                                        //button_send.Enabled = false;
                                        textBox_port.Enabled = true;
                                        button_listen.Enabled = true;
                                        serverSocket.Close();

                                    }

                                }

                            }
                            else
                            {
                                logs.AppendText("Clients message has been brodcasted. \n");
                            }
                            clientSockets.Add(thisClient);

                            string off_friends = give_my_friend_list(friendships, name_about_to);
                            if (off_friends != "")
                            {
                                using (StringReader reader = new StringReader(off_friends))
                                {
                                    string line;
                                    while ((line = reader.ReadLine()) != null)
                                    {
                                        if (names_include.Contains(line) == false)
                                        {
                                            line = line.Substring(0, line.Length - 1);
                                            string message_offline = "req_offline+" + name_about_to + "-" + line + "/" + incomingMessage + "*.";
                                            offline_message_nots.Add(message_offline);
                                        }
                                    }
                                }
                            }
                        }
                    }

                }
                catch
                {
                    if (!terminating)
                    {

                        logs.AppendText("A client has disconnected\n");
                    }

                    if (name_about_to != "")
                    {
                        if (names_include.Contains(name_about_to) == true)
                        {
                            names_include.Remove(name_about_to);
                        }
                    }
                    if (clientSockets.Contains(thisClient))
                    {
                        clientSockets.Remove(thisClient);
                    }
                    thisClient.Close();
                    connected = false;
                }
            }
        }

        private void Form1_FormClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            listening = false;
            terminating = true;
            Environment.Exit(0);
        }
    }
}
