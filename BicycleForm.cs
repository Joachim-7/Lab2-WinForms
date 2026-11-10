using System.Drawing;
using System.Windows.Forms;

namespace Lab2;

public class BicycleForm : Form
{
    private readonly TextBox txtStart = new() { Left = 140, Top = 30, Width = 100 };
    private readonly TextBox txtEnd = new() { Left = 140, Top = 70, Width = 100 };
    private readonly Button btnCalculate = new() { Text = "Calculate", Left = 140, Top = 110, Width = 100 };
    private readonly TextBox txtTotal = new() { Left = 140, Top = 150, Width = 100, ReadOnly = true };
    private readonly Button btnBack = new() { Text = "Back", Left = 40, Top = 200, Width = 90 };
    private readonly Button btnLogout = new() { Text = "Logout", Left = 150, Top = 200, Width = 90 };

    public BicycleForm()
    {
        Text = "Bicycle Renting";
        ClientSize = new Size(290, 250);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "Start hour (0-23):", Left = 20, Top = 33, AutoSize = true });
        Controls.Add(new Label { Text = "End hour (1-24):", Left = 20, Top = 73, AutoSize = true });
        Controls.Add(new Label { Text = "Total (RWF):", Left = 20, Top = 153, AutoSize = true });
        Controls.Add(txtStart);
        Controls.Add(txtEnd);
        Controls.Add(btnCalculate);
        Controls.Add(txtTotal);
        Controls.Add(btnBack);
        Controls.Add(btnLogout);

        AcceptButton = btnCalculate;
        btnCalculate.Click += BtnCalculate_Click;
        btnBack.Click += (s, e) => Nav.Go(this, new ChooseForm());
        btnLogout.Click += (s, e) => Nav.Go(this, new LoginForm());
        FormClosed += (s, e) => Nav.OnClosed();
    }

    // Rate (RWF) for the hour that starts at 'hour'
    private static int RateForHour(int hour)
    {
        if (hour >= 14 && hour < 19) return 1500;
        if ((hour >= 7 && hour < 14) || (hour >= 19 && hour < 21)) return 1000;
        return 500; // 0-7 and 21-24
    }

    private void BtnCalculate_Click(object sender, EventArgs e)
    {
        txtTotal.Clear();

        // int.TryParse rejects empty text, letters and decimals
        if (!int.TryParse(txtStart.Text.Trim(), out int start) ||
            !int.TryParse(txtEnd.Text.Trim(), out int end))
        {
            MessageBox.Show("Enter whole numbers only (no decimals or letters).", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (start < 0 || start > 23)
        {
            MessageBox.Show("Start hour must be between 0 and 23.", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (end < 1 || end > 24)
        {
            MessageBox.Show("End hour must be between 1 and 24.", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (start >= end)
        {
            MessageBox.Show("Start time must be less than end time.", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        int total = 0;
        for (int h = start; h < end; h++)
            total += RateForHour(h);

        txtTotal.Text = total.ToString();
        MessageBox.Show($"Total rental cost: {total} RWF", "Result",
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
