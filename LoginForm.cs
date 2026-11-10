using System.Drawing;
using System.Windows.Forms;

namespace Lab2;

public class LoginForm : Form
{
    private readonly TextBox txtUsername = new() { Left = 120, Top = 30, Width = 180 };
    private readonly TextBox txtPassword = new() { Left = 120, Top = 70, Width = 180, UseSystemPasswordChar = true };
    private readonly Button btnLogin = new() { Text = "Login", Left = 120, Top = 110, Width = 100 };

    public LoginForm()
    {
        Text = "Login";
        ClientSize = new Size(340, 160);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "Username:", Left = 30, Top = 33, AutoSize = true });
        Controls.Add(new Label { Text = "Password:", Left = 30, Top = 73, AutoSize = true });
        Controls.Add(txtUsername);
        Controls.Add(txtPassword);
        Controls.Add(btnLogin);

        AcceptButton = btnLogin;
        btnLogin.Click += BtnLogin_Click;
        FormClosed += (s, e) => Nav.OnClosed();
    }

    private void BtnLogin_Click(object sender, EventArgs e)
    {
        // Only validation needed: username and password cannot be empty
        if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrWhiteSpace(txtPassword.Text))
        {
            MessageBox.Show("Username and Password cannot be empty.", "Login",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        Nav.Go(this, new ChooseForm());
    }
}
