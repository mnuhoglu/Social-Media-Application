namespace client
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.textBox_port = new System.Windows.Forms.TextBox();
            this.button_connect = new System.Windows.Forms.Button();
            this.logs = new System.Windows.Forms.RichTextBox();
            this.textBox_message = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button_send = new System.Windows.Forms.Button();
            this.name_text = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.dis_button = new System.Windows.Forms.Button();
            this.button_show_friends = new System.Windows.Forms.Button();
            this.text_send_rq = new System.Windows.Forms.TextBox();
            this.button_send_rq = new System.Windows.Forms.Button();
            this.text_send_ans = new System.Windows.Forms.TextBox();
            this.button_send_ans = new System.Windows.Forms.Button();
            this.logs_Request = new System.Windows.Forms.RichTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button_delete_friend = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.radio_accept = new System.Windows.Forms.RadioButton();
            this.radio_decline = new System.Windows.Forms.RadioButton();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "IP:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 81);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Port:";
            // 
            // textBox_ip
            // 
            this.textBox_ip.Location = new System.Drawing.Point(65, 47);
            this.textBox_ip.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(199, 22);
            this.textBox_ip.TabIndex = 2;
            this.textBox_ip.Text = "127.0.0.1";
            // 
            // textBox_port
            // 
            this.textBox_port.Location = new System.Drawing.Point(65, 78);
            this.textBox_port.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_port.Name = "textBox_port";
            this.textBox_port.Size = new System.Drawing.Size(199, 22);
            this.textBox_port.TabIndex = 3;
            this.textBox_port.Text = "5";
            // 
            // button_connect
            // 
            this.button_connect.BackColor = System.Drawing.Color.Lime;
            this.button_connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_connect.Location = new System.Drawing.Point(15, 143);
            this.button_connect.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_connect.Name = "button_connect";
            this.button_connect.Size = new System.Drawing.Size(251, 44);
            this.button_connect.TabIndex = 4;
            this.button_connect.Text = "CONNECT";
            this.button_connect.UseVisualStyleBackColor = false;
            this.button_connect.Click += new System.EventHandler(this.button_connect_Click);
            // 
            // logs
            // 
            this.logs.Location = new System.Drawing.Point(297, 43);
            this.logs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs.Name = "logs";
            this.logs.Size = new System.Drawing.Size(313, 330);
            this.logs.TabIndex = 5;
            this.logs.Text = "";
            // 
            // textBox_message
            // 
            this.textBox_message.Enabled = false;
            this.textBox_message.Location = new System.Drawing.Point(95, 270);
            this.textBox_message.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.textBox_message.Name = "textBox_message";
            this.textBox_message.Size = new System.Drawing.Size(169, 22);
            this.textBox_message.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 272);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(69, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Message:";
            // 
            // button_send
            // 
            this.button_send.BackColor = System.Drawing.Color.Aqua;
            this.button_send.Enabled = false;
            this.button_send.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_send.Location = new System.Drawing.Point(15, 300);
            this.button_send.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_send.Name = "button_send";
            this.button_send.Size = new System.Drawing.Size(251, 49);
            this.button_send.TabIndex = 8;
            this.button_send.Text = "SEND";
            this.button_send.UseVisualStyleBackColor = false;
            this.button_send.Click += new System.EventHandler(this.button_send_Click);
            // 
            // name_text
            // 
            this.name_text.Location = new System.Drawing.Point(65, 107);
            this.name_text.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.name_text.Name = "name_text";
            this.name_text.Size = new System.Drawing.Size(199, 22);
            this.name_text.TabIndex = 9;
            this.name_text.Text = "Audrey Castillo";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 111);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 17);
            this.label4.TabIndex = 10;
            this.label4.Text = "Name:";
            // 
            // dis_button
            // 
            this.dis_button.BackColor = System.Drawing.Color.Black;
            this.dis_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dis_button.ForeColor = System.Drawing.Color.White;
            this.dis_button.Location = new System.Drawing.Point(15, 192);
            this.dis_button.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dis_button.Name = "dis_button";
            this.dis_button.Size = new System.Drawing.Size(251, 44);
            this.dis_button.TabIndex = 11;
            this.dis_button.Text = "DISCONNECT";
            this.dis_button.UseVisualStyleBackColor = false;
            this.dis_button.Click += new System.EventHandler(this.dis_button_Click);
            // 
            // button_show_friends
            // 
            this.button_show_friends.BackColor = System.Drawing.Color.Fuchsia;
            this.button_show_friends.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_show_friends.Location = new System.Drawing.Point(641, 306);
            this.button_show_friends.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_show_friends.Name = "button_show_friends";
            this.button_show_friends.Size = new System.Drawing.Size(200, 68);
            this.button_show_friends.TabIndex = 12;
            this.button_show_friends.Text = "SHOW FRIENDS";
            this.button_show_friends.UseVisualStyleBackColor = false;
            this.button_show_friends.Click += new System.EventHandler(this.button_show_friends_Click);
            // 
            // text_send_rq
            // 
            this.text_send_rq.Location = new System.Drawing.Point(640, 70);
            this.text_send_rq.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.text_send_rq.Name = "text_send_rq";
            this.text_send_rq.Size = new System.Drawing.Size(200, 22);
            this.text_send_rq.TabIndex = 13;
            // 
            // button_send_rq
            // 
            this.button_send_rq.BackColor = System.Drawing.Color.Yellow;
            this.button_send_rq.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_send_rq.Location = new System.Drawing.Point(640, 96);
            this.button_send_rq.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_send_rq.Name = "button_send_rq";
            this.button_send_rq.Size = new System.Drawing.Size(200, 42);
            this.button_send_rq.TabIndex = 14;
            this.button_send_rq.Text = "Send Friend Request";
            this.button_send_rq.UseVisualStyleBackColor = false;
            this.button_send_rq.Click += new System.EventHandler(this.button_send_rq_Click);
            // 
            // text_send_ans
            // 
            this.text_send_ans.Location = new System.Drawing.Point(644, 203);
            this.text_send_ans.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.text_send_ans.Name = "text_send_ans";
            this.text_send_ans.Size = new System.Drawing.Size(195, 22);
            this.text_send_ans.TabIndex = 15;
            // 
            // button_send_ans
            // 
            this.button_send_ans.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.button_send_ans.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_send_ans.Location = new System.Drawing.Point(644, 258);
            this.button_send_ans.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_send_ans.Name = "button_send_ans";
            this.button_send_ans.Size = new System.Drawing.Size(195, 44);
            this.button_send_ans.TabIndex = 16;
            this.button_send_ans.Text = "Send Answer";
            this.button_send_ans.UseVisualStyleBackColor = false;
            this.button_send_ans.Click += new System.EventHandler(this.button_send_ans_Click);
            // 
            // logs_Request
            // 
            this.logs_Request.Location = new System.Drawing.Point(868, 43);
            this.logs_Request.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.logs_Request.Name = "logs_Request";
            this.logs_Request.Size = new System.Drawing.Size(313, 290);
            this.logs_Request.TabIndex = 19;
            this.logs_Request.Text = "";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(383, 16);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 24);
            this.label5.TabIndex = 20;
            this.label5.Text = "Message Box";
            // 
            // button_delete_friend
            // 
            this.button_delete_friend.BackColor = System.Drawing.Color.Red;
            this.button_delete_friend.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button_delete_friend.Location = new System.Drawing.Point(640, 145);
            this.button_delete_friend.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button_delete_friend.Name = "button_delete_friend";
            this.button_delete_friend.Size = new System.Drawing.Size(200, 42);
            this.button_delete_friend.TabIndex = 21;
            this.button_delete_friend.Text = "Delete Friend";
            this.button_delete_friend.UseVisualStyleBackColor = false;
            this.button_delete_friend.Click += new System.EventHandler(this.button_delete_friend_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(963, 16);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(89, 24);
            this.label6.TabIndex = 22;
            this.label6.Text = "Requests";
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Blue;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.White;
            this.button1.Location = new System.Drawing.Point(868, 338);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(315, 36);
            this.button1.TabIndex = 23;
            this.button1.Text = "SHOW REQUESTS";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // radio_accept
            // 
            this.radio_accept.AutoSize = true;
            this.radio_accept.Location = new System.Drawing.Point(644, 231);
            this.radio_accept.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radio_accept.Name = "radio_accept";
            this.radio_accept.Size = new System.Drawing.Size(72, 21);
            this.radio_accept.TabIndex = 24;
            this.radio_accept.TabStop = true;
            this.radio_accept.Text = "Accept";
            this.radio_accept.UseVisualStyleBackColor = true;
            this.radio_accept.CheckedChanged += new System.EventHandler(this.radio_accept_CheckedChanged);
            // 
            // radio_decline
            // 
            this.radio_decline.AutoSize = true;
            this.radio_decline.Location = new System.Drawing.Point(724, 231);
            this.radio_decline.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.radio_decline.Name = "radio_decline";
            this.radio_decline.Size = new System.Drawing.Size(76, 21);
            this.radio_decline.TabIndex = 25;
            this.radio_decline.TabStop = true;
            this.radio_decline.Text = "Decline";
            this.radio_decline.UseVisualStyleBackColor = true;
            this.radio_decline.CheckedChanged += new System.EventHandler(this.radio_decline_CheckedChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(128)))));
            this.button2.Location = new System.Drawing.Point(640, 13);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(199, 52);
            this.button2.TabIndex = 26;
            this.button2.Text = "SHOW EVERYBODY";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 389);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.radio_decline);
            this.Controls.Add(this.radio_accept);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.button_delete_friend);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.logs_Request);
            this.Controls.Add(this.button_send_ans);
            this.Controls.Add(this.text_send_ans);
            this.Controls.Add(this.button_send_rq);
            this.Controls.Add(this.text_send_rq);
            this.Controls.Add(this.button_show_friends);
            this.Controls.Add(this.dis_button);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.name_text);
            this.Controls.Add(this.button_send);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_message);
            this.Controls.Add(this.logs);
            this.Controls.Add(this.button_connect);
            this.Controls.Add(this.textBox_port);
            this.Controls.Add(this.textBox_ip);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.TextBox textBox_port;
        private System.Windows.Forms.Button button_connect;
        private System.Windows.Forms.RichTextBox logs;
        private System.Windows.Forms.TextBox textBox_message;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_send;
        private System.Windows.Forms.TextBox name_text;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button dis_button;
        private System.Windows.Forms.Button button_show_friends;
        private System.Windows.Forms.TextBox text_send_rq;
        private System.Windows.Forms.Button button_send_rq;
        private System.Windows.Forms.TextBox text_send_ans;
        private System.Windows.Forms.Button button_send_ans;
        private System.Windows.Forms.RichTextBox logs_Request;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button_delete_friend;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RadioButton radio_accept;
        private System.Windows.Forms.RadioButton radio_decline;
        private System.Windows.Forms.Button button2;
    }
}

