using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLL.Facade; 
using DomainModel;

namespace WinUI.WinForms.Gestiones.Clientes
{
    public partial class FrmSeleccionarCliente : Form
    {
        public Cliente ClienteSeleccionado { get; private set; }
        private List<Cliente> _listaCompletaClientes;
        public FrmSeleccionarCliente()
        {
            InitializeComponent();
            this.Text = "Seleccionar Capitán";
        }

        private void FrmSeleccionarCliente_Load(object sender, EventArgs e)
        {
            CargarGrilla();
            ConfigurarGrilla();
        }

        private void CargarGrilla()
        {
            try
            {
                _listaCompletaClientes = BLLFacade.Current.ClienteService.GetAll().ToList();
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = _listaCompletaClientes;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrilla()
        {
            if (dgvClientes.Columns.Contains("IdCliente")) dgvClientes.Columns["IdCliente"].Visible = false;
            if (dgvClientes.Columns.Contains("Telefono")) dgvClientes.Columns["Telefono"].Visible = false;
            //if (dgvClientes.Columns.Contains("Mail")) dgvClientes.Columns["Mail"].Visible = false; quiza le añado email dsp para avisar de partido de competiciones
            if (dgvClientes.Columns.Contains("Nombre")) dgvClientes.Columns["Nombre"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            string textoBusqueda = txtBuscar.Text.Trim().ToLower();

            if (string.IsNullOrEmpty(textoBusqueda))
            {
                dgvClientes.DataSource = null;
                dgvClientes.DataSource = _listaCompletaClientes;
            }
            else
            {

                var listaFiltrada = _listaCompletaClientes.Where(c =>
                    c.Nombre != null && 
                    c.Nombre.ToLower().Contains(textoBusqueda)
                ).ToList();

                dgvClientes.DataSource = null;
                dgvClientes.DataSource = listaFiltrada;
            }
        }

        private void btnSeleccionar_Click(object sender, EventArgs e)
        {
            SeleccionarCliente();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void dgvClientes_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                SeleccionarCliente();
            }
        }

        private void SeleccionarCliente()
        {
            if (dgvClientes.SelectedRows.Count > 0)
            {
                var cliente = (Cliente)dgvClientes.SelectedRows[0].DataBoundItem;
                this.ClienteSeleccionado = cliente;
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                MessageBox.Show("Por favor, seleccione un cliente de la lista.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
