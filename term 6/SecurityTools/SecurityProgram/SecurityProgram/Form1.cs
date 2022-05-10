using System.Collections;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SecurityProgram
{
    public partial class Form1 : Form
    {
        private readonly MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider();

        private readonly List<TextBox> loginTextBoxes = new List<TextBox>();
        private readonly List<TextBox> passwordTextBoxes = new List<TextBox>();
        private readonly List<TextBox> inputs = new List<TextBox>();
        private readonly List<TextBox> outputs = new List<TextBox>();
        private int authorizedUsersCount = 0;
        private const int MaxUsers = 3;

        private List<User> users = new List<User>()
        {
            new User("user1", "password1", false, Roles.DiscordAdmin),
            new User("user2", "password2", false, Roles.Moderator),
            new User("user3", "password3", false, Roles.Redditor),
            new User("user4", "password4", false, Roles.Redditor),
        };

        public Form1()
        {
            InitializeComponent();
            List<TextBox> controlList = new List<TextBox>();
            controlList.AddRange(this.Controls.OfType<TextBox>());
            loginTextBoxes.AddRange(controlList.Where(cnt => cnt.Name.StartsWith("login")));
            passwordTextBoxes.AddRange(controlList.Where(cnt => cnt.Name.StartsWith("password")));
            inputs.AddRange(controlList.Where(cnt => cnt.Name.StartsWith("input")));
            outputs.AddRange(controlList.Where(cnt => cnt.Name.StartsWith("output")));

            loginTextBoxes.Reverse();
            passwordTextBoxes.Reverse();
            inputs.Reverse();
            outputs.Reverse();
        }

        private void Authorize_Click(object sender, EventArgs e)
        {
            int userIndex = int.Parse((sender as Button).Name[^1..]) - 1;

            if (users[userIndex].Authorized)
            {
                MessageBox.Show("User is already logined");
                return;
            }

            if (authorizedUsersCount >= MaxUsers || this.attackCheckedListBox1.CheckedItems.Contains("DOS"))
            {
                MessageBox.Show("Service denied due to DoS attack");
                return;
            }

            string login = loginTextBoxes[userIndex].Text;
            string password = passwordTextBoxes[userIndex].Text;
            string authSafety;
            if (this.attackCheckedListBox1.CheckedItems.Contains("Canonization Error"))
            {
                if (login == users[userIndex].Login && password == users[userIndex].Password)
                {
                    authSafety = "Unsafe";
                }
                else
                {
                    MessageBox.Show("Authorization failed");
                    return;
                }
            }
            else
            {
                if (CompareHash(mD5.ComputeHash(Encoding.ASCII.GetBytes(login)), mD5.ComputeHash(Encoding.ASCII.GetBytes(users[userIndex].Login))) &&
                    CompareHash(mD5.ComputeHash(Encoding.ASCII.GetBytes(password)), mD5.ComputeHash(Encoding.ASCII.GetBytes(users[userIndex].Password))))
                {
                    authSafety = "Unsafe";
                }
                else
                {
                    MessageBox.Show("Authorization failed");
                    return;
                }
            }

            User user = users[userIndex];
            user.Authorized = true;
            MessageBox.Show($"{authSafety} authorization completed successfully.");
            authorizedUsersCount++;

        }

        private void Leave_Clicked(object sender, EventArgs e)
        {
            int userIndex = int.Parse((sender as Button).Name[^1..]) - 5;
            var user = users[userIndex];
            if (user.Authorized)
            {
                user.Authorized = false;
                authorizedUsersCount--;
                MessageBox.Show("User left");
            }
            else
            {
                MessageBox.Show("User is not loginned yet");
            }
        }

        private bool CompareHash(byte[] hash1, byte[] hash2)
        {
            if (hash1.Length == hash2.Length)
            {
                int i = 0;
                for (i = 0; i < hash1.Length; i++)
                {
                    if (hash1[i] != hash2[i])
                    {
                        break;
                    }
                }

                if (i == hash1.Length)
                {
                    return true;
                }
            }

            return false;

        }

        private void Message_Click(object sender, EventArgs e)
        {
            int index = int.Parse((sender as Button).Name[^1..]) - 1;
            string message = inputs[index].Text;
            var output = outputs[index];
            var privilege = this.attackCheckedListBox1.CheckedItems.Contains("Minimize privileges") ?
                users[index].DowngratedRole : users[index].Role;
            if (this.attackCheckedListBox1.CheckedItems.Contains("XSS"))
            {
                if (Regex.IsMatch(message, @"^[(http(s)?):\/\/(www\.)?a-zA-Z0-9@:%._\+~#=]{2,256}\.[a-z]{2,6}\b([-a-zA-Z0-9@:%_\+.~#?&//=]*)$"))
                {
                    MessageBox.Show("Incorrect input");
                    return;
                }
            }

            switch (message[0])
            {
                case '1':
                    output.Text = $"Hi {message}";
                    break;
                case '2':
                    if (privilege == Roles.Redditor)
                        MessageBox.Show("You don't have the right privilege");
                    else
                        output.Text = $"Hello {message}";
                    break;
                case '3':
                    if (privilege == Roles.Redditor || privilege == Roles.Moderator)
                        MessageBox.Show("You don't have the right privilege");
                    else
                        output.Text = $"Greetings {message}";
                    break;
            }

        }
    }
}