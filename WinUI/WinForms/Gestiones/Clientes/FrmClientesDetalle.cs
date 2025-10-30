using System;
using System.Windows.Forms;
using BLL.Facade;
using DomainModel;

namespace WinUI.WinForms.Gestiones.Clientes
{
    public partial class FrmClientesDetalle : Form
    {
        private Cliente _clienteActual;

        // Constructor para "Nuevo"
        public FrmClientesDetalle()
        {
            InitializeComponent();
            _clienteActual = new Cliente();
        }

        // Constructor para "Editar"
        public FrmClientesDetalle(Cliente clienteAEditar)
        {
            InitializeComponent();
            _clienteActual = clienteAEditar;
        }

        private void FrmClientesDetalle_Load(object sender, EventArgs e)
        {
            if (_clienteActual.IdCliente != Guid.Empty) // Modo Editar
            {
                this.Text = "Editar Cliente";
                txtNombre.Text = _clienteActual.Nombre;
                txtTelefono.Text = _clienteActual.Telefono;
                txtEmail.Text = _clienteActual.Email;
            }
            else // Modo Nuevo
            {
                this.Text = "Nuevo Cliente";
            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El Nombre es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtNombre.Focus();
                return;
            }

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                MessageBox.Show("El Teléfono es obligatorio.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefono.Focus();
                return;
            }

            // (Opcional: validación simple de email)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) && !txtEmail.Text.Contains("@"))
            {
                MessageBox.Show("El formato del Email no es válido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtEmail.Focus();
                return;
            }

            try
            {
                // 1. Actualizar el objeto en memoria
                _clienteActual.Nombre = txtNombre.Text.Trim();
                _clienteActual.Telefono = txtTelefono.Text.Trim();
                _clienteActual.Email = txtEmail.Text.Trim();

                // 2. Decidir si crear o actualizar
                if (_clienteActual.IdCliente == Guid.Empty) // Es NUEVO
                {
                    _clienteActual.IdCliente = Guid.NewGuid();
                    BLLFacade.Current.ClienteService.Add(_clienteActual);
                    MessageBox.Show("Cliente creado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else // Es EDICIÓN
                {
                    BLLFacade.Current.ClienteService.Update(_clienteActual);
                    MessageBox.Show("Cliente actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al guardar el cliente: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}