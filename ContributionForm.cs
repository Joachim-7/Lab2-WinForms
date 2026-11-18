using System.Drawing;
using System.Windows.Forms;

namespace IkiminaManager;

public class ContributionForm : Form
{
    private const int ContributionAmount = 5000; // RWF per weekly cycle
    private const int LateFee = 500;             // RWF
    private const int MissedAfterDays = 7;       // cycle closes after 7 days late

    private readonly TextBox txtDaysLate = new() { Left = 170, Top = 30, Width = 100 };
    private readonly TextBox txtPaid = new() { Left = 170, Top = 70, Width = 100 };
    private readonly Button btnCalculate = new() { Text = "Calculate", Left = 170, Top = 110, Width = 100 };
    private readonly TextBox txtTotal = new() { Left = 170, Top = 150, Width = 100, ReadOnly = true };
    private readonly TextBox txtStatus = new() { Left = 170, Top = 190, Width = 100, ReadOnly = true };
    private readonly Button btnBack = new() { Text = "Back", Left = 40, Top = 240, Width = 90 };
    private readonly Button btnLogout = new() { Text = "Logout", Left = 180, Top = 240, Width = 90 };

    public ContributionForm()
    {
        Text = "Contribution";
        ClientSize = new Size(310, 290);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "Days late (0-30):", Left = 20, Top = 33, AutoSize = true });
        Controls.Add(new Label { Text = "Amount paid (RWF):", Left = 20, Top = 73, AutoSize = true });
        Controls.Add(new Label { Text = "Total due (RWF):", Left = 20, Top = 153, AutoSize = true });
        Controls.Add(new Label { Text = "Status:", Left = 20, Top = 193, AutoSize = true });
        Controls.Add(txtDaysLate);
        Controls.Add(txtPaid);
        Controls.Add(btnCalculate);
        Controls.Add(txtTotal);
        Controls.Add(txtStatus);
        Controls.Add(btnBack);
        Controls.Add(btnLogout);

        AcceptButton = btnCalculate;
        btnCalculate.Click += BtnCalculate_Click;
        btnBack.Click += (s, e) => Nav.Go(this, new ChooseForm());
        btnLogout.Click += (s, e) => Nav.Go(this, new LoginForm());
        FormClosed += (s, e) => Nav.OnClosed();
    }

    private void BtnCalculate_Click(object sender, EventArgs e)
    {
        txtTotal.Clear();
        txtStatus.Clear();

        // int.TryParse rejects empty text, letters and decimals
        if (!int.TryParse(txtDaysLate.Text.Trim(), out int daysLate) ||
            !int.TryParse(txtPaid.Text.Trim(), out int paid))
        {
            MessageBox.Show("Enter whole numbers only (no decimals or letters).", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (daysLate < 0 || daysLate > 30)
        {
            MessageBox.Show("Days late must be between 0 and 30.", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (paid < 0)
        {
            MessageBox.Show("Amount paid cannot be negative.", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Status: Paid (on time), Late (1-6 days), Missed (7 days or more)
        string status = daysLate == 0 ? "Paid" : daysLate < MissedAfterDays ? "Late" : "Missed";
        int total = ContributionAmount + (daysLate > 0 ? LateFee : 0);
        int balance = total - paid;

        txtTotal.Text = total.ToString();
        txtStatus.Text = status;

        string message = $"Status: {status}\nTotal due: {total} RWF\nAmount paid: {paid} RWF\n";
        message += balance > 0 ? $"Remaining: {balance} RWF"
                 : balance < 0 ? $"Overpaid by: {-balance} RWF"
                 : "Fully paid.";
        MessageBox.Show(message, "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
