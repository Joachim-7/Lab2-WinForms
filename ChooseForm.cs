using System.Drawing;
using System.Windows.Forms;

namespace IkiminaManager;

public class ChooseForm : Form
{
    private readonly Button btnContribution = new() { Text = "Contribution", Left = 40, Top = 60, Width = 110, Height = 40 };
    private readonly Button btnLoan = new() { Text = "Loan", Left = 170, Top = 60, Width = 110, Height = 40 };

    public ChooseForm()
    {
        Text = "Choose";
        ClientSize = new Size(320, 140);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "Choose an option:", Left = 40, Top = 25, AutoSize = true });
        Controls.Add(btnContribution);
        Controls.Add(btnLoan);

        btnContribution.Click += (s, e) => Nav.Go(this, new ContributionForm());
        btnLoan.Click += (s, e) => Nav.Go(this, new LoanForm());
        FormClosed += (s, e) => Nav.OnClosed();
    }
}
