using System.Drawing;
using System.Windows.Forms;

namespace IkiminaManager;

public class LoanForm : Form
{
    private const int MonthlyRatePercent = 5; // 5% per month

    private readonly TextBox txtAmount = new() { Left = 170, Top = 30, Width = 100 };
    private readonly TextBox txtMonths = new() { Left = 170, Top = 70, Width = 100 };
    private readonly TextBox txtPot = new() { Left = 170, Top = 110, Width = 100 };
    private readonly Button btnCalculate = new() { Text = "Calculate", Left = 170, Top = 150, Width = 100 };
    private readonly TextBox txtTotal = new() { Left = 170, Top = 190, Width = 100, ReadOnly = true };
    private readonly Button btnBack = new() { Text = "Back", Left = 40, Top = 240, Width = 90 };
    private readonly Button btnLogout = new() { Text = "Logout", Left = 180, Top = 240, Width = 90 };

    public LoanForm()
    {
        Text = "Loan";
        ClientSize = new Size(310, 290);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "Loan amount (RWF):", Left = 20, Top = 33, AutoSize = true });
        Controls.Add(new Label { Text = "Months (1-12):", Left = 20, Top = 73, AutoSize = true });
        Controls.Add(new Label { Text = "Cash in pot (RWF):", Left = 20, Top = 113, AutoSize = true });
        Controls.Add(new Label { Text = "Total to repay:", Left = 20, Top = 193, AutoSize = true });
        Controls.Add(txtAmount);
        Controls.Add(txtMonths);
        Controls.Add(txtPot);
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

    private void BtnCalculate_Click(object sender, EventArgs e)
    {
        txtTotal.Clear();

        // int.TryParse rejects empty text, letters and decimals
        if (!int.TryParse(txtAmount.Text.Trim(), out int amount) ||
            !int.TryParse(txtMonths.Text.Trim(), out int months) ||
            !int.TryParse(txtPot.Text.Trim(), out int pot))
        {
            MessageBox.Show("Enter whole numbers only (no decimals or letters).", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (amount <= 0)
        {
            MessageBox.Show("Loan amount must be greater than 0.", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (months < 1 || months > 12)
        {
            MessageBox.Show("Months must be between 1 and 12.", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (pot < 0)
        {
            MessageBox.Show("Cash in pot cannot be negative.", "Invalid input",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (amount > pot)
        {
            MessageBox.Show($"Loan rejected: {amount} RWF is more than the cash available ({pot} RWF).",
                "Loan", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        // Interest = amount x monthly rate x months (long avoids overflow)
        long interest = (long)amount * MonthlyRatePercent * months / 100;
        long total = amount + interest;

        txtTotal.Text = total.ToString();
        MessageBox.Show($"Loan approved.\nInterest: {interest} RWF\nTotal to repay: {total} RWF",
            "Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
