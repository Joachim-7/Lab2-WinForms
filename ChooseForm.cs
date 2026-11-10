using System.Drawing;
using System.Windows.Forms;

namespace Lab2;

public class ChooseForm : Form
{
    private readonly Button btnBicycle = new() { Text = "Bicycle", Left = 40, Top = 60, Width = 110, Height = 40 };
    private readonly Button btnMushroom = new() { Text = "Mushroom", Left = 170, Top = 60, Width = 110, Height = 40 };

    public ChooseForm()
    {
        Text = "Choose";
        ClientSize = new Size(320, 140);
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        Controls.Add(new Label { Text = "Choose an option:", Left = 40, Top = 25, AutoSize = true });
        Controls.Add(btnBicycle);
        Controls.Add(btnMushroom);

        btnBicycle.Click += (s, e) => Nav.Go(this, new BicycleForm());
        btnMushroom.Click += (s, e) => Nav.Go(this, new MushroomForm());
        FormClosed += (s, e) => Nav.OnClosed();
    }
}
