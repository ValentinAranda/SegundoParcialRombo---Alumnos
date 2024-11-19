using SegundoParcialRombo.Datos;
using SegundoParcialRombo.Entidades;

namespace SegundoParcialRombo.Windows
{
    public partial class frmRombos : Form
    {
        private RepositorioRombo? repositorio;
        private int cantidadRegistros;
        private List<Rombo>? rombos;
        public frmRombos()
        {
            InitializeComponent();
            repositorio = new RepositorioRombo();
        }
        private void tsbNuevo_Click(object sender, EventArgs e)
        {
            frmRomboAE frm = new frmRomboAE(repositorio) { Text = "Agregar Rombos" };
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) return;
            Rombo? rombo = frm.GetRombo();
            try
            {
                if (!repositorio!.Existe(rombo!))
                {
                    repositorio.AgregarRombo(rombo!);
                    DataGridViewRow r = ConstruirFila(dgvDatos);
                    SetearFila(r, rombo!);
                    AgregarFila(r, dgvDatos);
                    MessageBox.Show("Registro agregado", "...",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {

                    MessageBox.Show("¡¡¡Registro existente!!!", "¡Error!",
        MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            catch (Exception)
            {

                MessageBox.Show("¡¡¡Hubo un error!!!", "¡Error!",
    MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        public void SetearFila(DataGridViewRow r, Rombo obj)
        {
            r.Cells[0].Value = obj.DiagonalMayor;
            r.Cells[1].Value = obj.DiagonalMenor;
            r.Cells[2].Value = obj.Tipocontorno.ToString();
            r.Cells[3].Value = obj.CalcularArea().ToString("N2");
            r.Cells[4].Value = obj.CalcularPerimetro().ToString("N2");

            r.Tag = obj;
        }
        private void AgregarFila(DataGridViewRow r, DataGridView dgv)
        {
            dgv.Rows.Add(r);
        }

        public void LimpiarGrilla(DataGridView grid)
        {
            grid.Rows.Clear();
        }
        public DataGridViewRow ConstruirFila(DataGridView grid)
        {
            var r = new DataGridViewRow();
            r.CreateCells(grid);
            return r;
        }
        private void tsbBorrar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow r = dgvDatos.SelectedRows[0];
            Rombo rombo = (Rombo)r.Tag!;
            DialogResult dr = MessageBox.Show("¿Desea borrar el rombo?", "Confirme",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.No) { return; }
            try
            {
                repositorio!.EliminarRombo(rombo);
                EliminarFila(r, dgvDatos);
                MessageBox.Show("¡Registro agregado!", "...",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (Exception)
            {

                MessageBox.Show("¡¡¡Hubo un error!!!", "¡Error!",
                MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void EliminarFila(DataGridViewRow r, DataGridView grid)
        {
            grid.Rows.Remove(r);
        }
        private void tsbEditar_Click(object sender, EventArgs e)
        {
            if (dgvDatos.SelectedRows.Count == 0)
            {
                return;
            }
            DataGridViewRow r = dgvDatos.SelectedRows[0];
            Rombo? rombo = (Rombo)r.Tag!;
            frmRomboAE frm = new frmRomboAE(repositorio) { Text = "Editar Rombo" };
            frm.SetRombo(rombo);
            DialogResult dr = frm.ShowDialog(this);
            if (dr == DialogResult.Cancel) { return; }
            try
            {
                rombo = frm.GetRombo();
                SetearFila(r, rombo!);
                MessageBox.Show("Registro editado", "Mensaje",
                MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception)
            {

                MessageBox.Show("¡¡¡Hubo un error!!!", "¡Error!",
MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }


        private void CargarComboContornos(ref ToolStripComboBox tsCboBordes)
        {
            var listaContornos = Enum.GetValues(typeof(Contorno));
            foreach (var item in listaContornos)
            {
                tsCboBordes.Items.Add(item);
            }
            tsCboBordes.DropDownStyle = ComboBoxStyle.DropDownList;
            tsCboBordes.SelectedIndex = 0;

        }
        private void MostrarDatosGrilla()
        {
            LimpiarGrilla(dgvDatos);
            foreach (var item in rombos!)
            {
                var r = ConstruirFila(dgvDatos);
                SetearFila(r, item);
                AgregarFila(r, dgvDatos);
            }
        }

        private void lado09ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            rombos = repositorio!.OrdenarArriba();
            MostrarDatosGrilla();
        }

        private void lado90ToolStripMenuItem_Click(object sender, EventArgs e)
        {
          rombos = repositorio!.OrdenarAbajo();
          MostrarDatosGrilla();
        }

        private void tsbActualizar_Click(object sender, EventArgs e)
        {
            rombos = repositorio!.ObtenerRombo();
            MostrarDatosGrilla();
        }

        private void tsbSalir_Click(object sender, EventArgs e)
        {
            repositorio!.AlmacenarDatos();
            MessageBox.Show("Fin del Programa", "...",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            Application.Exit();
        }

        private void frmRombos_Load(object sender, EventArgs e)
        {
            CargarComboContornos(ref tsCboContornos);
            cantidadRegistros = repositorio!.GetCantidad();
            if (cantidadRegistros > 0)
            {
                rombos = repositorio.ObtenerRombo();
                MostrarDatosGrilla();
                MostrarCantidadRegistros();
            }


        }
        private void MostrarCantidadRegistros()
        {
            txtCantidad.Text = cantidadRegistros.ToString();
        }

    }
}
