using System.Drawing;
using System.Windows.Forms;

namespace Lab2;

public class MushroomForm : Form
{
    private readonly Label lblQuestion = new() { Left = 20, Top = 60, Width = 340, Height = 40, Text = "Click Start and think of a mushroom." };
    private readonly Button btnStart = new() { Text = "Start", Left = 20, Top = 20, Width = 100 };
    private readonly Button btnYes = new() { Text = "Yes", Left = 20, Top = 110, Width = 90, Enabled = false };
    private readonly Button btnNo = new() { Text = "No", Left = 130, Top = 110, Width = 90, Enabled = false };
    private readonly Button btnBack = new() { Text = "Back", Left = 20, Top = 170, Width = 90 };
    private readonly Button btnLogout = new() { Text = "Logout", Left = 130, Top = 170, Width = 90 };

    private int step;      // 0 = not started, 1..3 = question number
    private bool convex;
    private bool ring;

    public MushroomForm()
    {
        Text = "Mushroom";
        ClientSize = new Size(380, 220);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(btnStart);
        Controls.Add(lblQuestion);
        Controls.Add(btnYes);
        Controls.Add(btnNo);
        Controls.Add(btnBack);
        Controls.Add(btnLogout);

        btnStart.Click += BtnStart_Click;
        btnYes.Click += (s, e) => Answer(true);
        btnNo.Click += (s, e) => Answer(false);
        btnBack.Click += (s, e) => Nav.Go(this, new ChooseForm());
        btnLogout.Click += (s, e) => Nav.Go(this, new LoginForm());
        FormClosed += (s, e) => Nav.OnClosed();
    }

    private void BtnStart_Click(object sender, EventArgs e)
    {
        step = 1;
        btnYes.Enabled = true;
        btnNo.Enabled = true;
        lblQuestion.Text = "Does your mushroom have a convex cup?";
    }

    private void Answer(bool yes)
    {
        switch (step)
        {
            case 1:
                convex = yes;
                step = 2;
                lblQuestion.Text = "Does your mushroom have a ring?";
                break;

            case 2:
                ring = yes;
                if (convex && !ring) { ShowResult("Pied bleu"); return; }
                if (!convex && ring) { ShowResult("Coprin chevelu"); return; }
                step = 3;
                lblQuestion.Text = convex
                    ? "Does your mushroom grow in a forest?"
                    : "Does your mushroom have gills?";
                break;

            case 3:
                if (convex) // convex + ring: forest -> Amanite, meadow -> Agaric
                    ShowResult(yes ? "Amanite tue-mouche" : "Agaric Jaunissant");
                else        // no convex + no ring: gills -> Girolle, pores -> Cepe
                    ShowResult(yes ? "Girolle" : "Cepe de bordeau");
                break;
        }
    }

    private void ShowResult(string name)
    {
        MessageBox.Show($"Your mushroom is: {name}", "Result",
            MessageBoxButtons.OK, MessageBoxIcon.Information);

        step = 0;
        btnYes.Enabled = false;
        btnNo.Enabled = false;
        lblQuestion.Text = "Click Start and think of a mushroom.";
    }
}
