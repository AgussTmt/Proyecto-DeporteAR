using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Services.DomainModel;
using WinUI.WinForms.Main;
using WinUI.WinForms.Gestiones;

namespace WinUI.WinForms
{
    public partial class FrmMain : Form
    {
        //Fields
        private Button currentButton;
        private Random random;
        private int tempIndex;
        private Form activeForm;
        private Usuario _user;
        public FrmMain(Usuario user)
        {
            
            InitializeComponent();
            random = new Random();
            _user = user;
        }

        private Color SelectThemeColor()
        {
            int index = random.Next(ThemeColor.ColorList.Count);
            while (tempIndex == index)
            {
                index = random.Next(ThemeColor.ColorList.Count);
            }
            tempIndex = index;
            string color = ThemeColor.ColorList[index];
            return ColorTranslator.FromHtml(color);
        }

        private void ActivateButton(object btnSender)
        {
            if (btnSender != null)
            {
                if (currentButton != (Button)btnSender)
                {
                    DisableButton();
                    Color color = SelectThemeColor();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    panelTitleBar.BackColor = color;
                    panelLogo.BackColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    ThemeColor.PrimaryColor = color;
                    ThemeColor.SecondaryColor = ThemeColor.ChangeColorBrightness(color, -0.3);
                    //btnCloseChildForm.Visible = true; 
                }
            }
        }

        private void DisableButton()
        {
            foreach (Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                    previousBtn.BackColor = Color.FromArgb(23, 24, 29);
                    previousBtn.ForeColor = Color.Gainsboro;
                    previousBtn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
            }
        }


        private void OpenChildForm(Form childForm, object btnSender)
        {
            if (!TienePermiso(childForm))
            {
                MessageBox.Show($"No tienes permisos para acceder a {childForm.Text}, acceso denegado");
                return;
            }
            else
            {
                if (activeForm != null)
                    activeForm.Close();
                ActivateButton(btnSender);
                activeForm = childForm;
                childForm.TopLevel = false;
                childForm.FormBorderStyle = FormBorderStyle.None;
                childForm.Dock = DockStyle.Fill;
                this.panelDesktopPane.Controls.Add(childForm);
                this.panelDesktopPane.Tag = childForm;
                childForm.BringToFront();
                childForm.Show();
                LblTitle.Text = childForm.Text.Substring(3);
            }

           
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        private void BtnCalendario_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCalendario(), sender);
        }

        private void BtnCancha_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCanchas(), sender);
        }

        private void BtnCompeticion_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmCompeticion(), sender);
        }

        private void BtnReportes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmReportes(), sender);
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmSettings(), sender);
        }

        private void BtnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private bool TienePermiso(Form childForm)
        {
            return _user.Patentes.Any(p => p.DataKey == childForm.GetType().Name);
        }
    }
}
