using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DomainModel;

namespace WinUI.WinForms.Gestiones.Equipos
{
    public partial class FrmEquipoDetalle : Form
    {
        private Equipo equipoSeleccionado;

        public FrmEquipoDetalle()
        {
            InitializeComponent();
        }

        public FrmEquipoDetalle(Equipo equipoSeleccionado)
        {
            this.equipoSeleccionado = equipoSeleccionado;
        }
    }
}
