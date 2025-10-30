using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BLL.Facade;
using DomainModel;

namespace WinUI.WinForms.Gestiones.Clientes
{
    public partial class FrmClientes : Form
    {
        private List<Cliente> _listaCompletaClientes;

        public FrmClientes()
        {
            InitializeComponent();
            _listaCompletaClientes = new List<Cliente>();
        }

        private void FrmClientes_Load(object sender, EventArgs e)
        {
            CargarGrid();
        }

        private void CargarGrid()
        {
            try
            {
                
                _listaCompletaClientes = BLLFacade.Current.ClienteService.GetAll().ToList();
                RefrescarGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar clientes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                _listaCompletaClientes = new List<Cliente>();
                RefrescarGrid();
            }
        }

        private void RefrescarGrid()
        {
            string filtro = txtBuscar.Text.ToLower().Trim();

            List<Cliente> listaFiltrada;

            if (string.IsNullOrEmpty(filtro))
            {
                listaFiltrada = _listaCompletaClientes;
            }
            else
            {
                listaFiltrada = _listaCompletaClientes
                    .Where(c => c.Nombre.ToLower().Contains(filtro) ||
                                (c.Telefono != null && c.Telefono.ToLower().Contains(filtro)) ||
                                (c.Email != null && c.Email.ToLower().Contains(filtro)))
                    .ToList();
            }

            dgvClientes.DataSource = null;
            dgvClientes.DataSource = listaFiltrada;

            if (dgvClientes.Columns.Contains("IdCliente"))
            {
                dgvClientes.Columns["IdCliente"].Visible = false;
            }
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            RefrescarGrid();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            using (var formDetalle = new FrmClientesDetalle())
            {
                var result = formDetalle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CargarGrid();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un cliente para editar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var clienteSeleccionado = (Cliente)dgvClientes.CurrentRow.DataBoundItem;

            using (var formDetalle = new FrmClientesDetalle(clienteSeleccionado))
            {
                var result = formDetalle.ShowDialog();
                if (result == DialogResult.OK)
                {
                    CargarGrid();
                }
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("El borrado de clientes debe implementarse en la BLL.\n\nAsegúrese de que el borrado controle que el cliente no sea capitán de un equipo activo.", "Función No Implementada", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            /*
            // --- EJEMPLO DE CÓDIGO CUANDO TENGAS EL DELETE ---
            
            if (dgvClientes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un cliente para borrar.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var clienteSeleccionado = (Cliente)dgvClientes.CurrentRow.DataBoundItem;
            
            var confirmacion = MessageBox.Show($"¿Está seguro de borrar al cliente '{clienteSeleccionado.Nombre}'?\n\n¡ADVERTENCIA: Si el cliente es capitán, podría causar errores!", "Confirmar Borrado", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            
            if (confirmacion == DialogResult.Yes)
            {
                try
                {
                    // Necesitarás implementar ClienteService.Delete(Guid id) en BLL y DAL
                    BLLFacade.Current.ClienteService.Delete(clienteSeleccionado.IdCliente);
                    CargarGrid();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al borrar cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            */
        }
    }
}